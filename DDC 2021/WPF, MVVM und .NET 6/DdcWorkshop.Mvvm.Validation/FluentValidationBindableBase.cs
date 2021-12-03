using System.Collections;
using System.ComponentModel;
using FluentValidation;

namespace DdcWorkshop.Mvvm.Validation;

public class FluentValidationBindableBase : BindableBase, INotifyDataErrorInfo
{
    readonly AssemblyScanner validators;
    private readonly IValidator validatorInstance;

    public FluentValidationBindableBase()
    {
        validators = AssemblyScanner.FindValidatorsInAssembly(GetType().Assembly);
        var assemblyScanResult = validators.FirstOrDefault();
        if (assemblyScanResult != null)
        {
            validatorInstance = (IValidator) Activator.CreateInstance(assemblyScanResult.ValidatorType);
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        var result = validatorInstance.Validate(new ValidationContext<object>(this));
        return result.Errors.Select(x => x.ErrorMessage);
    }

    public bool HasErrors => !validatorInstance.Validate(new ValidationContext<object>(this)).IsValid;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


}