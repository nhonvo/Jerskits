using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ecommerce.Application.Common;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.Photo;

namespace ecommerce.Application.Services;

public class PhotoService(AppSettings config) : IPhotoService
{
    private readonly Cloudinary _cloudinary = new(
        new Account(
            config.Cloudinary.CloudName,
            config.Cloudinary.ApiKey,
            config.Cloudinary.ApiSecret
        ));
    public async Task<PhotoUploadResult> AddPhotoAsync(IFormFile file)
    {
        if (file == null || file.Length <= 0)
        {
            throw new ArgumentException("Invalid file", nameof(file));
        }

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new InvalidOperationException(uploadResult.Error.Message);
        }

        return new PhotoUploadResult
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.ToString()
        };
    }

    public async Task<string> DeletePhotoAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
        {
            throw new ArgumentException("Invalid public ID", nameof(publicId));
        }

        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Result != "ok")
        {
            throw new InvalidOperationException("Failed to delete photo");
        }

        return result.Result;
    }

    public string GetPhoto(string publicId) => _cloudinary.Api.UrlImgUp.BuildUrl(publicId);

}
