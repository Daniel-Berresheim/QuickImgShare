
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

        public string PostToImgur(string imageBase64)
        {
            var responseTask = _httpClient.PostAsync(POST_URL, new StringContent(imageBase64));
            responseTask.Wait();

            var response = responseTask.Result;

            if (response.IsSuccessStatusCode)
            {
                var task = response.Content.ReadAsByteArrayAsync();
                task.Wait();

                return Encoding.Default.GetString(task.Result);
            }
            else 
            {
                throw new HttpRequestException("Response Error: " + response.StatusCode);
            }
        }
    }
}
