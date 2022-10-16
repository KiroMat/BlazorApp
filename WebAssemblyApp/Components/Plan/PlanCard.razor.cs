using Microsoft.AspNetCore.Components;
using DataApi.Shared.Models;

namespace WebAssemblyApp.Components
{
    public partial class PlanCard
    {
        [Parameter]
        public Plan Plan { get; set; }

        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public EventCallback<Plan> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnEditClicked { get; set; }
    }
}