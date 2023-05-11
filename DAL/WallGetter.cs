using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using VkWallReader.DAL.Interfaces;

namespace VkWallReader.DAL;
public class WallGetter : IWallGetter
{
    public async Task<JToken> GetWallAsync(string domain)
    {
        var client = new HttpClient { BaseAddress = new Uri("https://api.vk.com/method/") };
        Dictionary<string, string> query = new()
        {
            ["access_token"] = "token",
            ["domain"] = domain,
            ["count"] = "5",
            ["v"] = "5.131"
        };
        var method = "wall.get";
        var jsonString = await client.GetStringAsync(QueryHelpers.AddQueryString(method, query));
        return await Task.FromResult(JToken.Parse(jsonString));
    }
}
