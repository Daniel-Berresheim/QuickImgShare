using System.Text.Json;
using System.Text.Json.Serialization;

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

        
    }
}
