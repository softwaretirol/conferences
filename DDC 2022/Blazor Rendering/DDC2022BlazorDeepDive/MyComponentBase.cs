using Microsoft.AspNetCore.Components;

namespace DDC2022BlazorDeepDive
{
    public class MyComponentBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}
