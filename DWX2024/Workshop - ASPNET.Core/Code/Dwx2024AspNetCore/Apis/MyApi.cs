namespace Dwx2024AspNetCore.Apis;

public static class MyApi
{
    public static void MapMyApi(this IEndpointRouteBuilder app)
    {
        app.MapPersonApi()
            .MapLoginApi();
            // .MapPersonApi()
            // .MapPersonApi()
            // .MapPersonApi()
            // .MapPersonApi()
            // .MapPersonApi();
    }
}