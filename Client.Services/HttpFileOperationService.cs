using Client.Services.Interfaces;
using DataApi.Shared.Models;

namespace Client.Services
{
    public class HttpFileOperationService : IFileOperationService
    {
        private readonly HttpClient _client;

        public HttpFileOperationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> SendFileAsync(FileWithDataForm model, FormFile formFile)
        {
            var content = PrepareFileForm(model, formFile);

            var response = await _client.PostAsync("api/fileoperation", content);

            if (!response.IsSuccessStatusCode)
            {
                return "Error from API";
            }

            return String.Empty;
        }

        private HttpContent PrepareFileForm(FileWithDataForm model, FormFile formFile)
        {
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(model.Description), nameof(FileWithDataForm.Description));

            if (formFile != null)
                form.Add(new StreamContent(formFile.FileStream), nameof(model.File), formFile.FileName);

            return form; 
        }
    }
}
