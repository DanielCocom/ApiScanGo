using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;



public class CloudinaryServices 
{

    private readonly Cloudinary _cloudinary;

    public CloudinaryServices(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    public async Task<string> UploadImageOfProduct(IFormFile file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream())
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl.AbsoluteUri;
    }


}