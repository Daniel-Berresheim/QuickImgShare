
namespace QuickImgShare
{
    internal static class FileHandler
    {
        // TODO: error handling, etc
        public static byte[] ReadImageFile(string path) 
        {
            return File.ReadAllBytes(path);
        }

        public static void WriteImageFile(string path, byte[] image) 
        {
            File.WriteAllBytes(path, image);
        }

        // Potential addition to prevent errors from sending too large files? Are there any libraries for this available?
        // public static byte[] ResizeImage(byte[] imageData, int maxWidth, int maxHeight) { }
    }
}
