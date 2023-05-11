namespace VkWallReader.BLL.Infrastructure;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
