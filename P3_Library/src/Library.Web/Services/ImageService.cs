namespace Library.Web.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment) => _webHostEnvironment = webHostEnvironment;

        public string ConvertImageBytesToBase64ImageString(byte[] imageBytes)
        {
            const string prefix = "data:image/jpeg;base64,";
            var imageData = Convert.ToBase64String(imageBytes);
            return $"{prefix}{imageData}";
        }

        public async Task<string> GetBase64ImageStringAsync(byte[]? imageBytes)
        {
            if (imageBytes is not null && imageBytes.Length != 0)
            {
                return ConvertImageBytesToBase64ImageString(imageBytes);
            }

            var placeholderImageBytes = await GetPlaceholderImageBytesAsync().ConfigureAwait(false);
            return ConvertImageBytesToBase64ImageString(placeholderImageBytes);
        }

        public async Task<byte[]> GetPlaceholderImageBytesAsync()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", "place_holder.png");
            var placeholderImageBytes = await File.ReadAllBytesAsync(path).ConfigureAwait(false);
            return placeholderImageBytes;
        }
    }
}
