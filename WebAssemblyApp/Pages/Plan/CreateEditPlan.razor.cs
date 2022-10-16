using MudBlazor;

namespace WebAssemblyApp.Pages.Plan
{
    public partial class CreateEditPlan
    {
        private List<BreadcrumbItem> _breadCrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Plans", "/plans"),
            new BreadcrumbItem("Form", "/plans/form", true)
        };
    }
}