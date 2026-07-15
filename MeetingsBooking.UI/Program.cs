using MeetingsBooking.UI;
using MeetingsBooking.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(
//    sp => new HttpClient
//    {
//        BaseAddress = new Uri("https://meeting-scheduler-api.azurewebsites.net/")
//    });
var hostEnvironment = builder.HostEnvironment;

var baseUrl = hostEnvironment.IsDevelopment()
    ? "https://localhost:7099/"
    : "https://meeting-scheduler-api.azurewebsites.net/";

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl)
});
builder.Services.AddScoped<IBookMeetings,BookMeetings>();
builder.Services.AddScoped<IFileUploader, FileUploader>();
await builder.Build().RunAsync();