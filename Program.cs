
using QuickImgShare;

const string AccessToken = "24855467c3079c954ffd4e568a375775494084e2";

// print a message and close application if no image is provided
if (args.Length == 0)
{
    Console.WriteLine("Drag and drop one or multiple images (jpg or png) on the exe to upload it to Imgur privately.");
    Console.ReadLine();
    Environment.Exit(0);
}

ImgurApiClient ImgurClient = new(AccessToken);

// iterate over all images in args
foreach (string argument in args) PostToImgurFromPath(argument, ImgurClient);

Console.ReadLine();

static async void PostToImgurFromPath(string path, ImgurApiClient client) 
{
    if (FileHandler.PathGetImageType(path) != null)
    {
        // load image as base64
        string imageData = Convert.ToBase64String(FileHandler.ReadImageFile(path));

        string response = await client.PostToImgurAsync(imageData);
        string? imageLink = JsonHandler.GetImageLinkFromResponse(response);

        if (imageLink != null) Console.WriteLine("Image uploaded to: " + imageLink);
        else Console.WriteLine("Error: Could not get link from response.");
    }
}

