using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using RecipeBook.API.Data;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add CORS before building
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// FastEndpoints and Swagger
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Use CORS before endpoints
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();
app.UseFastEndpoints();

app.Run();



// 👇 This part is only for EF Core tools (Add-Migration, Update-Database)
public partial class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
                });
            });
}
