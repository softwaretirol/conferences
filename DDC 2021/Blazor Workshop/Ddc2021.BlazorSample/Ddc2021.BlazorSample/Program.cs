Host
    .CreateDefaultBuilder()
    .ConfigureWebHostDefaults(webHost =>
    {
        webHost.UseStartup<Startup>();
    })
    .Build()
    .Run();