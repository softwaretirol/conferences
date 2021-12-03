namespace DdcWorkshop.DependencyInjection;

public class Container
{
    private readonly Dictionary<Type, Type> mapping = new();

    public void Register<TSource>()
    {
        Register(typeof(TSource), typeof(TSource));
    }

    public void Register<TSource, TType>()
        where TType : TSource
    {
        Register(typeof(TSource), typeof(TType));
    }

    public void Register(Type sourceType, Type targetType)
    {
        mapping[sourceType] = targetType;
    }

    public T Resolve<T>()
    {
        return (T) Resolve(typeof(T));
    }

    public object? Resolve(Type lookupType)
    {
        var targetType = mapping[lookupType];
        var ctor = targetType.GetConstructors().First();
        var parameters = ctor.GetParameters();
        List<object> ctorArguments = new();
        foreach (var parameter in parameters)
        {
            var parameterInstance = Resolve(parameter.ParameterType);
            ctorArguments.Add(parameterInstance);
        }

        return Activator.CreateInstance(targetType, ctorArguments.ToArray());
    }
}