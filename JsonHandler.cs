using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickImgShare
{
    internal static class JsonHandler
    {
        /// <summary>
        /// This function takes a string encoding an Imgur json image representation and returns its corresponding image link.
        /// </summary>
        /// <param name="response">A json image representation as a string as it is received by the Imgur API.</param>
        /// <returns>A string containing the web link to the image described by the input. If the fields are missing, null is returned.</returns>
        public static string? GetImageLinkFromResponse(string response)
        {
            try
            {
                ImgurGetModel? ImgurGet = JsonSerializer.Deserialize<ImgurGetModel>(response);

                return ImgurGet?.Data?.Link;
            }
            catch (JsonException exception)
            {
                Console.WriteLine("JSON Deserialization Error: " + exception.Message);
                return null;
            }
        }
    }

    /// <summary>
    /// This class encodes the first layer of a json representation of an image send by Imgur. 
    /// </summary>
    public class ImgurGetModel
    {
        [JsonPropertyName("data")]
        public ImgurGetDataModel? Data { get; set; }
        public bool success;
        public int status;
    }

    /// <summary>
    /// This class encodes the data field of a json representation of an image send by Imgur. 
    /// </summary>
    public class ImgurGetDataModel
    {
        [JsonPropertyName("link")]
        public string? Link { get; set; }
    }
}
