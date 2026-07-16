using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Infrastructure.Persistence;
using MeetingsBooking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingsBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =configuration.GetConnectionString("DefaultConnection");

        services.Configure<jwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<MeetingsBookingDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<IMeetingRepository,MeetingRepository>();
        services.AddScoped<IAzureBlobStorageRepository, AzureBlobStorageRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IJwtTokenGenerator,JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
        return services;
    }
}