using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlaceAgregator.API.Services;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.EntityFramework;
using System.Text;

IConfiguration Configuration;

var builder = WebApplication.CreateBuilder(args);
Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,

            ValidAudience = Configuration.GetValue<string>("JWT:Audience"),
            ValidIssuer = Configuration.GetValue<string>("JWT:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddSingleton<IAuthService>(
    new AuthService(
            jwtIssuer: Configuration.GetValue<string>("JWT:Issuer"),
            jwtAudience: Configuration.GetValue<string>("JWT:Audience"),
            jwtSecret: Configuration.GetValue<string>("JWT:Secret"),
            jwtLifespan: Configuration.GetValue<int>("Jwt:Lifespan")
        )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();