using System.Security.Cryptography;
using System.Text;

namespace VkWallReader.BLL.Dto;

public class CountedWallDto
{
    public class WallItem
    {
        public string Hash { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
    public SortedDictionary<char, int>? CountedLetters { get; set; }
    public string Domain { get; set; } = string.Empty;
    public List<WallItem> Items { get; set; } = null!;
    public int Count { get; set; }

    public override int GetHashCode()
    {
        var md5 = IncrementalHash.CreateHash(HashAlgorithmName.MD5);

        md5.AppendData(Encoding.ASCII.GetBytes(Domain));
        foreach (var item in Items)
            md5.AppendData(Encoding.ASCII.GetBytes(item.Hash));

        return BitConverter.ToInt32(md5.GetHashAndReset());
    }
    
    
}
