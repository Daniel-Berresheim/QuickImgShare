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

        // TODO: error handling
        public static string? GetImageLinkFromResponse(string response) 
        {
            ImgurGetModel? ImgurGet = JsonSerializer.Deserialize<ImgurGetModel>(response);

            return ImgurGet?.Data?.Link;
        }
    }

    public class ImgurGetModel
    {
        [JsonPropertyName("data")]
        public ImgurGetDataModel? Data { get; set; }
        public bool success;
        public int status;
    }

    public class ImgurGetDataModel
    {
        [JsonPropertyName("link")]
        public string? Link { get; set; }
    }
}
