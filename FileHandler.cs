using System.Text.Json;

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
        public static string GetImgLinkFromResponse(string response) 
        {
            ImgurGetModel? imgurGet = JsonSerializer.Deserialize<ImgurGetModel>(response);

            return (imgurGet == null) ? "" : imgurGet.data.link;
        }
    }

    public class ImgurGetModel
    {
        public ImgurGetDataModel data { get; set; }
        public bool success;
        public int status;
    }

    public class ImgurGetDataModel
    {
        public string link { get; set; }
    }
}
