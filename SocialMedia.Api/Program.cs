using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Infrastructure.Extensions;
using SocialMedia.Infrastructure.Filters;
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
//IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(options =>   
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            })
            .ConfigureApiBehaviorOptions(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            }
            );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Extension methods
builder.Services.AddOptions(configuration)
                .AddDbContexts(configuration)
                .AddServices()
                .AddSwagger($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Authentication:Issuer"],
                ValidAudience = configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]))
            };
        });

builder.Services.AddMvc(options => 
{
    options.Filters.Add<ValidationFilter>();
})
.AddFluentValidation(option => 
{
    option.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();
//IConfiguration configuration = app.Configuration;
//IWebHostEnvironment environment = app.Environment;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
