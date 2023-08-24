
using QuickImgShare;

const string AccessToken = "24855467c3079c954ffd4e568a375775494084e2";

// print a message and close application if no image is provided
if (args.Length == 0)
{
    Console.WriteLine("Drag and drop one or multiple images (jpg or png) on the exe to upload it to Imgur privately.");
    Console.ReadLine();
    Environment.Exit(0);
}

using (ImgurApiClient ImgurClient = new(AccessToken))
{
    List<Task> tasks = new List<Task>();

    foreach (string argument in args) tasks.Add(PostToImgurFromPath(argument, ImgurClient));

    await Task.WhenAll(tasks);
}

Console.ReadLine();

static async Task PostToImgurFromPath(string path, ImgurApiClient client) 
{
    if (FileHandler.PathGetImageType(path) != null)
    {
        string imageData = Convert.ToBase64String(FileHandler.ReadImageFile(path));

        string response = await client.PostToImgurAsync(imageData);
        string? imageLink = JsonHandler.GetImageLinkFromResponse(response);

        if (imageLink != null) Console.WriteLine("Image uploaded to: " + imageLink);
        else Console.WriteLine("Error: Failed to upload Image at path: " + path);
    }
}

static async Task GetFromImgurFromUrl(string url, string fileName) 
{
    using (ImgurApiClient ImgurClient = new(AccessToken))
    {
        byte[]? image = await ImgurClient.GetFromImgur(url);

        if (image != null) _ = File.WriteAllBytesAsync(fileName, image);
    }
}
