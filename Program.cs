// See https://aka.ms/new-console-template for more information

// TODO: outsource token to config file?
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

const string POST_URL = "https://api.imgur.com/3/image";
const string ACCESS_TOKEN = "24855467c3079c954ffd4e568a375775494084e2";

// print a message and close application if no image is provided
if (args.Length == 0)
{
    Console.WriteLine("Drag an image (jpg) on the exe to upload it to Imgur.");
    Console.ReadLine();
    Environment.Exit(0);
}

string imagePath = args[0];

Console.WriteLine(imagePath);

PostToImgur(POST_URL, imagePath, ACCESS_TOKEN);

Console.ReadLine();


static void PostToImgur(string postUrl, string localImagePath, string accessToken)
{
    // load image as base64, TODO: outsource later
    byte[] imageData = File.ReadAllBytes(localImagePath);
    File.WriteAllBytes("copy.jpg", imageData); // Todo: remove, test only
    string base64 = Convert.ToBase64String(imageData);

    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    var responseTask = client.PostAsync(postUrl, new StringContent(base64));
    responseTask.Wait();

    var response = responseTask.Result;
    client.Dispose();

    if (response.IsSuccessStatusCode)
    {
        var task = response.Content.ReadAsByteArrayAsync();
        task.Wait();

        var decodedString = Encoding.Default.GetString(task.Result);

        ImgurGetModel? imgurGet = JsonSerializer.Deserialize<ImgurGetModel>(decodedString);

        string link = imgurGet.data.link;
        Console.WriteLine("Image uploaded to: " + link);
    }
    else
    {
        Console.WriteLine("Response Error: " + response.StatusCode);
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