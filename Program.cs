using Api.Config;
using Api.Middleware;
using Api.Providers;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// JWT configuration
builder.Services.AddJwtProvider(builder.Configuration);

// DB connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Controllers
builder.Services.AddAppServiceProvider();
builder.Services.AddControllers();

// Validators
builder.Services.AddValidatorProvider();
builder.Services.AddFluentValidationAutoValidation();

// Swagger
builder.Services.AddSwaggerProvider();

// Authenticate and authorize
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Debug
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Global handler exception
app.UseMiddleware<HandlerException>();

app.Run();
