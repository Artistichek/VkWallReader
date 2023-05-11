namespace VkWallReader.DAL.Commands.AddCountedWall;

public interface IAddCountedWallCommandHandler
{
    Task<Guid> AddAsync(AddCountedWallModel model);
}
