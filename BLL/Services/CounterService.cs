using VkWallReader.BLL.Interfaces;

namespace VkWallReader.BLL.Services;

public class CounterService : ICounterService
{
    public async Task<SortedDictionary<char, int>> CountLetters(string wallText)
    {
        var countedLetters = new SortedDictionary<char, int>();
        var charSet = wallText
            .Distinct()
            .Where(char.IsLetter)
            .OrderBy(x => x);
        foreach (var letter in charSet)
            countedLetters.Add(letter, wallText.Count(c => c == letter));

        return await Task.FromResult(countedLetters);
    }
}