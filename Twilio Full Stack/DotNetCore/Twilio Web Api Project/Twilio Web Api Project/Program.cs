using Twilio_Web_Api_Project.Model;
using Twilio_Web_Api_Project.Services;

using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container


builder.Services.AddControllers();

// Register TwilioService with configuration
var twilioConfig = builder.Configuration.GetSection("Twilio").Get<TwilioConfig>();
builder.Services.AddScoped<TwilioService>(sp => new TwilioService(twilioConfig.AccountSid,
                                                                  twilioConfig.AuthToken,
                                                                  sp.GetRequiredService<ILogger<TwilioService>>() // Inject the logger
                                                                  ));

// Add Hangfire
//var connectionString = builder.Configuration.GetConnectionString("HangfireConnection"); // Define your connection string
//builder.Services.AddHangfire(configuration => configuration
//    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
//    {
//        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//        QueuePollInterval = TimeSpan.Zero,
//        UseRecommendedIsolationLevel = true,
//        DisableGlobalLocks = true
//    }));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsapp");

//app.UseHangfireDashboard(); // Add Hangfire Dashboard

//app.UseHangfireServer(); // Add Hangfire Server

app.MapControllers();

app.Run();