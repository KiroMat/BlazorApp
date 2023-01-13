using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;


namespace WebAssemblyApp.Components
{
    public partial class CreateToDoItemForm
    {
        [Inject]
        public IToDoItemService ToDoItemService { get; set; }

        [Parameter]
        public string PlanId { get; set; }

        private bool _isBusy = false;
        private string _description { get; set; }
        private string _errorMessage = string.Empty;

        [Parameter]
        public EventCallback<ToDoItem> OnToDoItemAdded { get; set; }

        private async Task AddToDoItemAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(_description))
                {
                    _errorMessage = "Description is requierd";
                    _isBusy = false;
                    return;
                }

                var result = await ToDoItemService.CreateAsync(new ToDoItem { Description = _description, PlanId = PlanId });
                _description = string.Empty;
                await OnToDoItemAdded.InvokeAsync(result);
            }
            catch (Exception ex)
            {
                throw;
            }
            _isBusy= false;
        }
    }
}
