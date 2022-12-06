using Microsoft.AspNetCore.Components;

namespace Ddc2022Blazor.Rcl.Components
{
    public partial class Expander
    {
        bool isExpanded;
        [Parameter]
        public string Header { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }
    }
}
