using MeetingsBooking.UI;
using MeetingsBooking.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7099/")
    });

builder.Services.AddScoped<IBookMeetings,BookMeetings>();
builder.Services.AddScoped<IFileUploader, FileUploader>();
await builder.Build().RunAsync();