using CollegeApp.MyLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
#region SerelogSettings
/*
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval:RollingInterval.Minute)
    .CreateLogger();    //Loggers got added in our application
// Add services to the container.

//builder.Host.UseSerilog();
builder.Logging.AddSerilog();///serilog with built in loggers */
#endregion


builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();





builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable=true).AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IMyLogger, LogToFile>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
