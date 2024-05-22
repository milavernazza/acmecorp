public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<Middleware.ApiKeyMiddleware>();

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}