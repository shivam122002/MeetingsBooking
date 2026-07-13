using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingsBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IBookMeetingService,BookMeetingService>();
        services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
        return services;
    }
}