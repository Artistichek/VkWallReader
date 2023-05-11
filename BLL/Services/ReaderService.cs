using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkWallReader.BLL.Dto;
using VkWallReader.BLL.Infrastructure;
using VkWallReader.BLL.Interfaces;
using VkWallReader.DAL.Interfaces;

namespace VkWallReader.BLL.Services;
public class ReaderService : IReaderService
{
    private readonly IWallGetter _wallGetter;
    private readonly ILogger<ReaderService> _logger;

    public ReaderService(IWallGetter wallGetter, ILogger<ReaderService> logger)
    {
        _wallGetter = wallGetter;
        _logger = logger;
    }

    public async Task<CountedWallDto?> ReadData(string domain)
    {
        var json = await _wallGetter.GetWallAsync(domain);
        var responseToken = json.SelectToken("response");
        if (responseToken != null)
        {
            var result = await Task.FromResult(JsonConvert.DeserializeObject<CountedWallDto>(responseToken.ToString()));
            result.Domain = domain;
            return result;
        }

        var errorCode = json.SelectToken("error").SelectToken("error_code");
        var errorMessage = json.SelectToken("error").SelectToken("error_msg");

        _logger.LogError($"Cannot get data from requested page. Error code: {errorCode}; Error message: {errorMessage}");
        throw new BadRequestException(json.ToString());
    }
}
