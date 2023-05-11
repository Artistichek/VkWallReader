namespace VkWallReader.BLL.Interfaces;

public interface ICounterService
{
    Task<SortedDictionary<char, int>> CountLetters(string wallText);
}