using Autorent.Domain.Interfaces;
using Autorent.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Autorent.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
        }
    }
}
