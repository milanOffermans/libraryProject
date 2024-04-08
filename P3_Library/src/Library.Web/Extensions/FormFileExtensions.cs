using System.Text.RegularExpressions;

namespace Library.Web.Extensions
{
    public static class FormFileExtensions
    {
        public const int ImageMinimumBytes = 512;

        public static async Task<byte[]> GetFileBytesAsync(this IFormFile formFile)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms).ConfigureAwait(false);

            return ms.ToArray();
        }

        public static bool IsImage(this IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile != null &&
                !string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile?.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }

                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content,
                        @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool IsValid(this IFormFile? formFile) => formFile != null && formFile.Length != 0;

        public static async Task<byte[]?> TryGetFileBytesAsync(this IFormFile? formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return null;
            }

            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms).ConfigureAwait(false);

            return ms.ToArray();
        }
    }
}
