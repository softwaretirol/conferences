using System;
using System.Windows;
using System.Windows.Controls;
using DdcWorkshop.DependencyInjection;

namespace DdcWorkshop.Controls;

public class LookupType : ContentPresenter
{
    //propdp

    //dependencyProperty
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        "Type", typeof(Type), typeof(LookupType), new PropertyMetadata(default(Type), OnTypeChanged));

    public Type Type
    {
        get => (Type) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LookupType lookupType)
        {
            lookupType.Content = ServiceProviderLocator.ServiceProvider?.GetService(lookupType.Type);
        }
    }

    //public Type Type { get; set; }
}