using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorPerf.Components.Pages;

public class MyComponent : IComponent
{
    private RenderHandle _renderHandle;

    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
        RenderNow(renderHandle);
    }

    private void RenderNow(RenderHandle renderHandle)
    {
        RenderFragment fragment = builder =>
        {
            builder.OpenElement(1, "p");
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, OnClick));
            builder.AddMarkupContent(2, "Hello World ");
            builder.AddMarkupContent(4, DateTime.Now.ToShortDateString());
            // builder.OpenComponent<Counter>();
            builder.CloseElement();
            
        };
        // RenderTreeBuilder builder = new();
        // builder.AddContent(1, fragment);
        // ArrayRange<RenderTreeFrame> frames = builder.GetFrames();
        renderHandle.Render(fragment);
    }

    private void OnClick()
    {
        RenderNow(_renderHandle);
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        return Task.CompletedTask;
    }
}