using FluentValidation;

namespace DdcWorkshop.Main;

public class MainWindowViewModelValidator : AbstractValidator<MainWindowViewModel>
{
    public MainWindowViewModelValidator()
    {
        RuleFor(x => x.Name).MinimumLength(2).MaximumLength(10);
    }
}