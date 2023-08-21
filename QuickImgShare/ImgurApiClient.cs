
using System.Net.Http.Headers;
using System.Text;

namespace QuickImgShare
{
    
    internal class ImgurApiClient : IDisposable
    {
        const string POST_URL = "https://api.imgur.com/3/image";

        private readonly HttpClient _httpClient;
    
        /// <summary>
        /// A class to handle calls to the imgur API. Builds an HttpClient on construction which is reused. 
        /// </summary>
        /// <param name="accessToken">The access token required in the Bearer authentication of the Imgur API.</param>
        public ImgurApiClient(string accessToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Makes a post request to the Imgur API to upload the specified image. The request happens asynchronously, the function returns a Task. 
        /// </summary>
        /// <param name="imageBase64">The image to post in base64 representation. Use Convert.ToBase64String(byteArray) to convert a byte array to this form.</param>
        /// <returns>A task handling the upload. Returns the API response as a string.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<string> PostToImgurAsync(string base64Image)
        { 
            var response = await _httpClient.PostAsync(POST_URL, new StringContent(base64Image));

            if (response.IsSuccessStatusCode)
            {
                var decodedResponse = await response.Content.ReadAsByteArrayAsync();

                return Encoding.Default.GetString(decodedResponse);
            }
            else 
            {
                throw new HttpRequestException("Response Error: " + response.StatusCode);
            }
        }

        /// <summary>
        /// A function to fetch an image file from a specified url and to save it to disk. Supports .png, .jpg and .jpeg.
        /// </summary>
        /// <param name="url">The address of the image.</param>
        /// <param name="fileName">The optional name the saved file will have without file extension. By default, the name will be "download".</param>
        public async void FetchImageFromUrl(string url, string fileName = "download")
        {
            string? type = FileHandler.PathGetImageType(url);
            if (type == null) return;

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsByteArrayAsync();

                    _ = File.WriteAllBytesAsync(fileName + "." + type, result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not fetch image: " + e.Message);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
