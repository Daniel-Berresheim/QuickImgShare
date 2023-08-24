
using QuickImgShare;

const string AccessTokenString = "D:/InputData/AccessTokens/ImgurAccessToken.txt";

// print a message and close application if no image is provided
if (args.Length == 0)
{
    Console.WriteLine("Drag and drop one or multiple images (jpg or png) on the exe to upload it to Imgur privately.");
    Console.ReadLine();
    Environment.Exit(0);
}

string? AccessToken = LoadAccessToken(AccessTokenString);
if (AccessToken == null) 
{
    Console.WriteLine("Error reading access token!");
    Console.ReadLine();
    Environment.Exit(0);
}

using (ImgurApiClient ImgurClient = new(AccessToken))
{
    var tasks = new List<Task>();

    foreach (string argument in args) tasks.Add(PostToImgurFromPath(argument, ImgurClient));

    await Task.WhenAll(tasks);
}

Console.ReadLine();

static async Task PostToImgurFromPath(string path, ImgurApiClient client) 
{
    if (FileHandler.PathGetImageType(path) != null)
    {
        string imageData = Convert.ToBase64String(FileHandler.ReadFile(path));

        string response = await client.PostToImgurAsync(imageData);
        string? imageLink = JsonHandler.GetImageLinkFromResponse(response);

        if (imageLink != null) Console.WriteLine("Image uploaded to: " + imageLink);
        else Console.WriteLine("Error: Failed to upload Image at path: " + path);
    }
}

static async Task GetFromImgurFromUrl(string url, string fileName, ImgurApiClient client) 
{
    byte[]? image = await client.GetFromImgur(url);

    if (image != null) _ = File.WriteAllBytesAsync(fileName, image);
}

static string? LoadAccessToken(string path) 
{
    try
    {
        if (File.Exists(path)) return File.ReadAllText(path);
        else return null;
    }
    catch
    {
        return null;
    }
}