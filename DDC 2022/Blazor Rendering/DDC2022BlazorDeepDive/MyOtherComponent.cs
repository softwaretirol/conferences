using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DDC2022BlazorDeepDive
{
    public class MyOtherComponent : IComponent, IHandleEvent
    {
        RenderFragment fragment;
        private RenderHandle _renderHandle;

        public MyOtherComponent()
        {
            fragment = builder =>
            {
                builder.OpenElement(0, "h2");
                builder.AddAttribute(1, "onclick",
                    EventCallback.Factory.Create(this, OnClick));
                builder.AddMarkupContent(2, DateTime.Now.ToString());
                builder.CloseElement();
            };
        }

        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
            renderHandle.Render(fragment);
        }

        private async Task OnClick()
        {
            await Task.Delay(1000);
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            return Task.CompletedTask;
        }

        public async Task HandleEventAsync(EventCallbackWorkItem item, object arg)
        {
            var task = item.InvokeAsync(arg);
            _renderHandle.Render(fragment);
            if (!task.IsCompleted)
            {
                await task;
                _renderHandle.Render(fragment);
            }
        }
    }
}
