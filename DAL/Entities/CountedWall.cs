using System.Text.Json;

namespace VkWallReader.DAL.Entities;

public class CountedWall : IDisposable
{
    public Guid Id { get; set; }
    public JsonDocument CountedLetters { get; set; } = null!;
    public string Domain { get; set; } = string.Empty;
    public int Hash { get; set; }

    public void Dispose() => CountedLetters?.Dispose();
}