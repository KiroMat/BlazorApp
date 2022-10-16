using MudBlazor;

namespace WebAssemblyApp.Pages.Plan
{
    public partial class Plans
    {
        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Plans", "/plans", true)
        };
    }
}