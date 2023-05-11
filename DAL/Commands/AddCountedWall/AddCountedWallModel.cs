using System.Text.Json;

namespace VkWallReader.DAL.Commands.AddCountedWall;

public class AddCountedWallModel
{
    public JsonDocument CountedLetters { get; set; } = null!;
    public string Domain { get; set; } = string.Empty;
    public int Hash { get; set; }
}
