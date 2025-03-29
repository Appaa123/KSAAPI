using System.Net;
using KSAApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;
// Add services to the container.
builder.Services.AddSingleton<IKSAService, KSAServcie>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<FarmStockRepository>();


var app = builder.Build();
app.UseForwardedHeaders(); // Ensures proper HTTPS handling
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin() // Allows requests from any domain
          .AllowAnyMethod() // Allows GET, POST, PUT, DELETE, etc.
          .AllowAnyHeader()); // Allows all headers
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
