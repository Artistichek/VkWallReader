using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VkWallReader.BLL.Interfaces;
using VkWallReader.BLL.Services;
using VkWallReader.DAL;
using VkWallReader.DAL.Commands.AddCountedWall;
using VkWallReader.DAL.EF;
using VkWallReader.DAL.Interfaces;
using VkWallReader.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    options.Filters.Add<ExceptionFilter>());

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

// Injections
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IWallGetter, WallGetter>();
builder.Services.AddScoped<IAddCountedWallCommandHandler, AddCountedWallCommandHandler>();
builder.Services.AddScoped<ICounterService, CounterService>();

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .CreateLogger();
builder.Logging.AddSerilog(logger);

// Swagger
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "VkWallReader");
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
