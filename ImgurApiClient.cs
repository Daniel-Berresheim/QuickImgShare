
using System.Net.Http.Headers;
using System.Text;

namespace QuickImgShare
{
    
    internal class ImgurApiClient
    {
        const string POST_URL = "https://api.imgur.com/3/image";

        private readonly HttpClient _httpClient;

        public ImgurApiClient(string accessToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<string> PostToImgurAsync(string imageBase64)
        {
            var response = await _httpClient.PostAsync(POST_URL, new StringContent(imageBase64));

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
    }
}
