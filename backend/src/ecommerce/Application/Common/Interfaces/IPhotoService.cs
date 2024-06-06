using ecommerce.Application.Common.Models.Photo;

namespace ecommerce.Application.Common.Interfaces;

public interface IPhotoService
{
    public Task<PhotoUploadResult> AddPhotoAsync(IFormFile file);
    public Task<string> DeletePhotoAsync(string publicId);
    public string GetPhoto(string publicId);
}
