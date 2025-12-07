using Autorent.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvironment();
builder.AddCorsConfiguration();
builder.AddDatabase();
builder.AddApplicationServices();
builder.AddJwtAuthentication();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

await app.UseSeeders();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
