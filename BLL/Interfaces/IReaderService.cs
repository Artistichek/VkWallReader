using VkWallReader.BLL.Dto;

namespace VkWallReader.BLL.Interfaces;
public interface IReaderService
{
    Task<CountedWallDto?> ReadData(string domain);
}
