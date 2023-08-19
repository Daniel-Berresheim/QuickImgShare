using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickImgShare
{
    internal static class JsonHandler
    {
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
