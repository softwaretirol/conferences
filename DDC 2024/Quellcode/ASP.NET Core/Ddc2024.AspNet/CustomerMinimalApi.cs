namespace Ddc2024.AspNet;

public static class DdcMinimalApi
{
    public static void MapDdcApi(this WebApplication app)
    {
        app.MapCustomerApi();
    }
}

public class Person
{
    public DateTime CreateTime { get; set; } = DateTime.Now;
}
public static class CustomerMinimalApi
{
    public static void MapCustomerApi(this WebApplication app)
    {
        app.MapGet("/customers3/{customerId}/", GetCustomer);

    }
    
    public static Person GetCustomer(int customerId, IConfiguration configuration)
    {
        return new Person();
    }
}