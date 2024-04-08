namespace Library.Web.Services
{
    public interface IImageService
    {
        string ConvertImageBytesToBase64ImageString(byte[] imageBytes);

        Task<string> GetBase64ImageStringAsync(byte[]? imageBytes);

        Task<byte[]> GetPlaceholderImageBytesAsync();
    }
}
