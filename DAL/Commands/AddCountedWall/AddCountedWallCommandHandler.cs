using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VkWallReader.DAL.EF;
using VkWallReader.DAL.Entities;
using VkWallReader.DAL.Infrastructure;

namespace VkWallReader.DAL.Commands.AddCountedWall;

public class AddCountedWallCommandHandler : IAddCountedWallCommandHandler
{
    private readonly DataContext _context;
    private readonly ILogger<AddCountedWallCommandHandler> _logger;

    public AddCountedWallCommandHandler(DataContext context,
        ILogger<AddCountedWallCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(AddCountedWallModel model)
    {
        await CheckForCopyAsync(model);

        var countedWall = new CountedWall
        {
            Id = Guid.NewGuid(),
            CountedLetters = model.CountedLetters,
            Domain = model.Domain,
            Hash = model.Hash
        };

        await _context.CountedWalls.AddAsync(countedWall);
        await _context.SaveChangesAsync();

        return countedWall.Id;
    }

    private async Task CheckForCopyAsync(AddCountedWallModel model)
    {
        var sameHashes = await _context.CountedWalls
            .Where(e => e.Hash == model.Hash)
            .ToListAsync();
        if (sameHashes.Count > 0)
        {
            foreach (var element in sameHashes) 
                if (element.Domain == model.Domain) 
                {
                    var error = "Record with these letters already exists, " +
                        $"Guid: {element.Id}";
                    
                    _logger.LogError(error);

                    var countedLetters = JValue
                        .Parse(element.CountedLetters.RootElement.ToString());

                    throw new DataConflictException(error + "\n" + countedLetters);
                }
        }
    }
}
