using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
