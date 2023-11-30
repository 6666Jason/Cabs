namespace Cabs.Areas.Website.Repositories.UploadFileRepo
{
    public interface IFileRepository
    {
        Task<string> UploadFile(IFormFile photo, string? folder);
        Task<bool> DeleteFile(string filePath);

    }
}
