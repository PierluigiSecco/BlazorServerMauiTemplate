using Blazored.SessionStorage;
using BlazorServerMaui.WebComponents.Data;
using BlazorServerMaui.WebComponents.Data.Auth;
using BlazorServerMaui.WebComponents.Data.Services.ProductService;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Serilog;
using webenology.blazor.components;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.File(@"C:\logs\log.txt")
        .CreateLogger();

        builder.Services.AddSingleton<HttpContextAccessor>();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        //builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        //builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
        //{
        //    opt.Password.RequireDigit = true;
        //    opt.Password.RequiredLength = 8;
        //    opt.Password.RequireLowercase = false;
        //    opt.Password.RequireUppercase = false;
        //    opt.Password.RequireNonAlphanumeric = false;
        //})
        //    .AddEntityFrameworkStores<DataContext>();

        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddWebenologyHelpers();

        builder.Services.AddBlazoredSessionStorage();

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<TooltipService>();
        builder.Services.AddScoped<ContextMenuService>();

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

        app.UseRouting();


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        //CreateHostBuilder(args).Build().Run();

        app.Run();
    }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>();
                });
    }