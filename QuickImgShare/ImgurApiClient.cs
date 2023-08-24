
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

        public async Task<byte[]?> GetFromImgur(string imageUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    ImgurGetModel? imgurGet = JsonSerializer.Deserialize<ImgurGetModel>(result);

                    string? link = imgurGet?.Data?.Link;

                    if (link == null) return null;

                    return await FetchFromUrl(link);
                }
                else
                {
                    Console.WriteLine("Fetching failed with status code: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not upload image: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// A function to fetch data from a specified url. 
        /// </summary>
        /// <param name="url">The address of the data.</param>
        /// <returns>A byte array containing the data downloaded.</returns>
        public async Task<byte[]?> FetchFromUrl(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not fetch data: " + e.Message);
            }
            return null;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
