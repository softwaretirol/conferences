using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DdcWorkshop.Mvvm.Validation;

public class DataAnnotationBindableBase : BindableBase, INotifyDataErrorInfo
{
    public IEnumerable GetErrors(string? propertyName)
    {
        ICollection<ValidationResult> results = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), results, true);
        return results.Select(x => x.ErrorMessage);
    }

    public bool HasErrors => !Validator.TryValidateObject(this,
        new ValidationContext(this),
        new List<ValidationResult>(),
        true);

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}