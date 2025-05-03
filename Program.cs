using System.Net;
using System.Text;
using AspNetCore.Identity.Mongo;
using KSAApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;
// Add services to the container.
builder.Services.AddSingleton<IKSAService, KSAServcie>();
builder.Services.AddControllers();
// builder.Services
//     .AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole, Guid>(identityOptions =>
//     {
//         identityOptions.Password.RequireDigit = false;
//         identityOptions.Password.RequiredLength = 6;
//     },
//     mongoIdentityOptions =>
//     {
//         mongoIdentityOptions.ConnectionString = "mongodb://localhost:27017";
//         mongoIdentityOptions.DatabaseName = "MyAuthDB";
//     });

// builder.Services.AddIdentityServer()
//     .AddAspNetIdentity<ApplicationUser>()
//     .AddDeveloperSigningCredential();
//     .AddDefaultTokenProviders();
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]??"")
                        )

                    };
                });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
