using AdminClient.Authentication;
using AdminClient.Components;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Caching.Memory;
using MudBlazor.Services;
using Serilog.Events;
using Serilog;
using Application.Contracts.Services;
using Hangfire;
var builder = WebApplication.CreateBuilder(args);

// Create the logs directory if it doesn't exist
if (!Directory.Exists("telthemlogs"))
{
    Directory.CreateDirectory("telthemlogs");
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "/telthemlogs/log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Error
    )
    .CreateLogger();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthenticationCore();
builder.Services.AddApplicationDependency();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomeAuthenticationStateProvider>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
builder.Services.AddPersistanceServicesDependecy(builder.Configuration);
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseStatusCodePagesWithRedirects("/404");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseHangfireDashboard();
app.MapHangfireDashboard();


RecurringJob.AddOrUpdate<IEmailService>(
    x => x.BroadcastEmailAsync(),
    Cron.MinuteInterval(int.Parse(builder.Configuration.GetSection("EMAIL_INTERVAL").Value)));

app.Run();
