using Hangfire;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Infrastructure.Authentication;
using MacsBusinessManagementAPI.Infrastructure.Jobs;
using MacsBusinessManagementAPI.Infrastructure.ServiceCollection;
using MacsBusinessManagementAPI.Infrastructure.Services.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using System.Reflection;
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection("JwtSettings"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter your JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
builder.Services.AddBusinessManagementServices();
builder.Services.AddDbContext<SQLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddHttpContextAccessor();
builder.Services.AddRateLimiting();
builder.Services.AddUseCaseInfrastructure(Assembly.GetExecutingAssembly());

var _JwtConfig = builder.Configuration
    .GetSection("JwtSettings").Get<JwtConfig>()!;

builder.Services.AddJwtAuth(_JwtConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<OverdueInvoiceReminderJob>(
    "overdue-invoice-reminders",
    job => job.ExecuteAsync(CancellationToken.None),
    Cron.Daily(7));

app.Run();
