using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
//IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            }
            );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SocialMediaContext>( options =>
options.UseSqlServer(configuration.GetConnectionString("SocialMedia"))
);

builder.Services.AddTransient<IPostRepository, PostRepository>();

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

var app = builder.Build();
//IConfiguration configuration = app.Configuration;
//IWebHostEnvironment environment = app.Environment;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
