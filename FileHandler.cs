
namespace QuickImgShare
{
    internal static class FileHandler
    {
        /// <summary>
        /// This function returns a byte array of the content of a specified file location. 
        /// </summary>
        /// <param name="path">The local location of the file to read out.</param>
        /// <returns>The content of the file as a byte array.</returns>
        public static byte[] ReadImageFile(string path) 
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception exception) 
            {
                Console.WriteLine("Error reading file: " + exception.Message);
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// This function saves a byte array to a specified file location.
        /// </summary>
        /// <param name="path">The local location of the file to store the data in.</param>
        /// <param name="image">The data to store as a byte array.</param>
        public static void WriteImageFile(string path, byte[] image) 
        {
            try
            {
                File.WriteAllBytes(path, image);
            }
            catch (Exception exception) 
            {
                Console.WriteLine("Error writing data to file: " + exception.Message);
            }
        }

        /// <summary>
        /// Returns for a given path whether it points to a file of type .png, .jpg or .jpeg.
        /// </summary>
        /// <param name="path">The path to evaluate, can be a local file location or a web link, as long as it ends with the file extension.</param>
        /// <returns>The function returns the strings "png", "jpg" or "jpeg" if the path points to the corresponding file type. Returns null otherwise.</returns>
        public static string? PathGetImageType(string path) 
        {
            if (path.EndsWith(".png")) return "png";
            else if (path.EndsWith(".jpg")) return "jpg";
            else if (path.EndsWith(".jpeg")) return "jpeg";
            else return null;
        }

        // Potential addition to prevent errors from sending too large files? Are there any libraries for this available?
        // public static byte[] ResizeImage(byte[] imageData, int maxWidth, int maxHeight) { }
    }
}
