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

        [Parameter]
        public string Id { get; set; }

        private bool _isEditMode => Id != null;

        private Plan _model = new();
        private bool _busy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if(_isEditMode)
                await FetchPlanByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _busy = true;
            try
            {
                FormFile formFile = null;
                if (_stream != null)
                    formFile = new FormFile(_stream, _fileName); 

                if(_isEditMode)
                {
                    await PlanService.EditAsync(_model);
                }
                else
                {
                    await PlanService.CreateAsync(_model);
                    await FileOperationService.SendFileAsync(new FileWithDataForm { Description = "lol" }, formFile);
                }

                Navigation.NavigateTo("/plans");
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _busy = false;
        }

        private async Task FetchPlanByIdAsync()
        {
            _busy = true;

            try
            {
                var result = await PlanService.GetByIdAsync(Id);
                _model = result;
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