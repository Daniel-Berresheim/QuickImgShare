
// TODO: outsource token to config file?
using QuickImgShare;

const string ACCESS_TOKEN = "24855467c3079c954ffd4e568a375775494084e2";

// print a message and close application if no image is provided
if (args.Length == 0)
{
    Console.WriteLine("Drag an image (jpg or png) on the exe to upload it to Imgur.");
    Console.ReadLine();
    Environment.Exit(0);
}

string imagePath = args[0];

// load image as base64, TODO: outsource later
string imageData = Convert.ToBase64String(FileHandler.ReadImageFile(imagePath));

ImgurApiClient client = new ImgurApiClient(ACCESS_TOKEN);
string response = client.PostToImgur(imageData);

Console.WriteLine("Image uploaded to: " + FileHandler.GetImgLinkFromResponse(response));

Console.ReadLine();


