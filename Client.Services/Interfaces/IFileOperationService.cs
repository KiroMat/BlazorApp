using DataApi.Shared.Models;

namespace Client.Services.Interfaces
{
    public interface IFileOperationService
    {
        Task<string> SendFileAsync(FileWithDataForm model, FormFile formFile);
    }
}
