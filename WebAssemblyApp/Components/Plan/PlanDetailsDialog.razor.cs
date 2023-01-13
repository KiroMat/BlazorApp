using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebAssemblyApp.Shared;

namespace WebAssemblyApp.Components
{
    public partial class PlanDetailsDialog
    {
        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IPlanService PlanService { get; set; }

        [Parameter]
        public string PlanId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private Plan _plan;
        private bool _isBusy;
        private string _errorMessage = string.Empty;
        private List<ToDoItem> _items = new();


        private void Close()
        {
            MudDialog.Cancel();
        }

        protected override void OnParametersSet()
        {
            if(PlanId == null)
                throw new ArgumentNullException(nameof(PlanId));

            base.OnParametersSet();
        }

        protected override async Task OnInitializedAsync()
        {
            await FetchPlanAsync();
        }

        private async Task FetchPlanAsync()
        {
            _isBusy= true;
            try
            {
                _plan = await PlanService.GetByIdAsync(PlanId);
                _items = _plan.ToDoItems.ToList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Error.HandlerError(ex);
                throw;
            }
            _isBusy= false;
        }

        private void OnToDoItemCallback(ToDoItem toDoItem)
        {
            _items.Add(toDoItem);
        }

        private void OnItemDeletedCallback(ToDoItem toDoItem)
        {
            _items.Remove(toDoItem);
        }

        private void OnItemEditedCallback(ToDoItem toDoItem)
        {
            var editedItem = _items.SingleOrDefault(x => x.Id == toDoItem.Id);
            editedItem.Description = toDoItem.Description;
            editedItem.IsDone= toDoItem.IsDone;
        }
    }
}
