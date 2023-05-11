using Newtonsoft.Json.Linq;

namespace VkWallReader.DAL.Interfaces;
public interface IWallGetter
{
    public Task<JToken> GetWallAsync(string domain);
}
