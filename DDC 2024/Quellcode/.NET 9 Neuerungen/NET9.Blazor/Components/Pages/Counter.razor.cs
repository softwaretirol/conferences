using Microsoft.AspNetCore.Components;

namespace NET9.Blazor.Components.Pages;

public partial class Counter(IConfiguration configuration)
{
    private int currentCount = 0;

    [Inject]
    public IConfiguration Configuration2 { get; set; }

    
    private void IncrementCount()
    {
        currentCount++;
    }
}