using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Autorent.Infrastructure.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new Exception("Jwt:Key missing");

            var key = Encoding.UTF8.GetBytes(jwtKey);

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }
    }
}
