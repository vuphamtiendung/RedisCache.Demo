using Microsoft.AspNetCore.Hosting.Builder;
using RedisCache.Demo02.Data;
using RedisCache.Demo02.Extension;
using RedisCache.Demo02.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.SessionConfiguration();
        builder.Services.AddRazorPages();
        builder.Services.RedisCacheConfiguration(builder.Configuration);
        builder.Services.AddScoped<RedisCachingService>();
        builder.Services.AddScoped<GamesDataService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        app.Run();
    }
}