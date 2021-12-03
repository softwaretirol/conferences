using System;
using System.Windows;
using DdcWorkshop.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DdcWorkshop.Main;

internal class DialogService : IDialogService
{
    public void ShowWindow<T>() where T : Window
    {
        var window = ServiceProviderLocator.ServiceProvider?.GetRequiredService<T>();
        if (window.DataContext is IWindowCallbackViewModel windowCallbackViewModel)
        {
            windowCallbackViewModel.CloseAction = window.Close;
        }
        window.Show();
    }
}

public interface IWindowCallbackViewModel
{
    Action CloseAction { get; set; }
}
public interface IDialogService
{
    void ShowWindow<T>() where T : Window;
}