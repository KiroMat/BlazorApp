using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebAssemblyApp.Pages.Plan
{
    public partial class CreateEditPlan
    {
        [Parameter]
        public string Id { get; set; }


        private List<BreadcrumbItem> _breadCrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Plans", "/plans"),
            new BreadcrumbItem("Form", "/plans/form", true)
        };
    }
}