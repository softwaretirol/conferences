

using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.AddTransient<Transient>();
services.AddScoped<Scoped>();
services.AddSingleton<Singleton>();

IServiceProvider serviceProvider = services.BuildServiceProvider();

Console.WriteLine("TRANSIENT");
var instanceOfTransient1 = serviceProvider.GetService<Transient>();
Console.WriteLine(instanceOfTransient1.Id);
var instanceOfTransient2 = serviceProvider.GetService<Transient>();
Console.WriteLine(instanceOfTransient2.Id);

Console.WriteLine("SINGLETON");
var instanceOfSingleton1 = serviceProvider.GetService<Singleton>();
Console.WriteLine(instanceOfSingleton1.Id);
var instanceOfSingleton2 = serviceProvider.GetService<Singleton>();
Console.WriteLine(instanceOfSingleton2.Id);

Console.WriteLine("SCOPED 1");
using (var scope = serviceProvider.CreateScope())
{
    var instanceOfScoped1 = scope.ServiceProvider.GetService<Scoped>();
    Console.WriteLine(instanceOfScoped1.Id);
    var instanceOfScoped2 = scope.ServiceProvider.GetService<Scoped>();
    Console.WriteLine(instanceOfScoped2.Id);
}
    
Console.WriteLine("SCOPED 2");
using (var scope = serviceProvider.CreateScope())
{
    var instanceOfScoped1 = scope.ServiceProvider.GetService<Scoped>();
    Console.WriteLine(instanceOfScoped1.Id);
    var instanceOfScoped2 = scope.ServiceProvider.GetService<Scoped>();
    Console.WriteLine(instanceOfScoped2.Id);
}
    

public class Transient
{
    public Transient()
    {
        
    }
    public Guid Id { get; } = Guid.NewGuid();
}
public class Scoped
{
    public Guid Id { get; } = Guid.NewGuid();
}
public class Singleton
{
    public Guid Id { get; } = Guid.NewGuid();
}