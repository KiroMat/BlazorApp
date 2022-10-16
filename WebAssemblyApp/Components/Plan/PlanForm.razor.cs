using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace WebAssemblyApp.Components
{
    public partial class PlanForm
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        [Inject]
        public IFileOperationService FileOperationService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private Plan _model = new();
        private bool _busy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        private async Task SubmitFormAsync()
        {
            _busy = true;
            try
            {
                FormFile formFile = null;
                if (_stream != null)
                    formFile = new FormFile(_stream, _fileName); 

                var result1 = await PlanService.CreateAsync(_model);
                var result2 = await FileOperationService.SendFileAsync(new FileWithDataForm { Description = "lol"}, formFile);


                Navigation.NavigateTo("/plans");
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _busy = false;
        }

        private async Task OnChooseFileAsync(InputFileChangeEventArgs e)
        {
            _errorMessage = String.Empty;
            var file = e.File;
            if(file != null)
            {
                if(file.Size > 2097152)
                {
                    _errorMessage = "File is must be equal or lest then 2MB";
                    return;
                }
                string[] allowedExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };
                string extension = Path.GetExtension(file.Name);
                if(!allowedExtensions.Contains(extension))
                {
                    _errorMessage = "Please choose a valid image file";
                    return ;
                }

                using var stream = file.OpenReadStream(2097152);
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                _stream = new MemoryStream(buffer);
                _stream.Position = 0;
                _fileName = file.Name;
            }
        }
    }
}