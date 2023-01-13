using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static MudBlazor.CategoryTypes;

namespace WebAssemblyApp.Components
{
    public partial class ToDoItemView
    {
        [Inject]
        public IToDoItemService ToDoItemService { get; set; }

        [Parameter]
        public ToDoItem Item { get; set; }

        [Parameter]
        public EventCallback<ToDoItem> OnToDoItemDeleted { get; set; }

        [Parameter]
        public EventCallback<ToDoItem> OnToDoItemEdited { get; set; }

        private bool _isChecked;

        private bool _isEdit = false;
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private string _description = string.Empty;
        private string _descriptionStyle => $"cursor:pointer; {(!_isChecked ? "" : "text-decoration:Line-through")}";

        protected override void OnInitialized()
        {
            _isChecked = Item.IsDone;
            base.OnInitialized();
        }

        private void ToogleEditMode(bool isCancel)
        {
            if(isCancel)
            {
                _isEdit = false;
                _description = isCancel ? Item.Description : _description;
            }
            else
            {
                _isEdit = true;
                _description = Item.Description;
            }
        }

        private async Task RemoveItemAsync()
        {
            _isBusy = true;

            try
            {
                await ToDoItemService.DeleteAsync(Item.Id);
                await OnToDoItemDeleted.InvokeAsync(Item);
            }
            catch (Exception ex)
            {
                throw;
            }
            _isBusy = false;
        }

        private async Task EditItemAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                if(string.IsNullOrEmpty(_description))
                {
                    _errorMessage = "Description is required";
                    return;
                }
                Item.Description = _description;
                var result = await ToDoItemService.EditAsync(Item);
                ToogleEditMode(false);

                await OnToDoItemEdited.InvokeAsync(result);
                
            }
            catch (Exception ex)
            {
                throw;
            }
            _isBusy = false;
        }

        private async Task ToogleItemAsync(bool value)
        {
            _isBusy = true;
            try
            {
                Item.IsDone = value;
                var result = await ToDoItemService.EditAsync(Item);
                _isChecked = value;
                await OnToDoItemEdited.InvokeAsync(result);

            }
            catch (Exception ex)
            {
                throw;
            }
            _isBusy = false;
        }
    }
}