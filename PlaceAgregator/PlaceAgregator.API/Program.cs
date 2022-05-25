using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PlaceAgregator.API.AppBuilders;
using PlaceAgregator.API.Services;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Types;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
        "Example: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference()
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<AppUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddDefaultTokenProviders();

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
builder.Services.AddSingleton<IConfiguration>((services) => Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<PlaceUpdateDTO, Place>()
        .ForMember(dest => dest.EventTypes,
            opt => opt.MapFrom(src => src.EventTypeIds.Select(item => new EventType() { Id = item })))
        .ForMember(dest => dest.Prohibitions,
            opt => opt.MapFrom(src => src.ProhibitionIds.Select(item => new Prohibition() { Id = item })));

    cfg.CreateMap<SheduleDTO, Shedule>();
    cfg.CreateMap<Place, GetPlaceDTO>()
        .ForMember(dest => dest.EventTypeIds,
            opt => opt.MapFrom(src => src.EventTypes.Select(item => item.Id)))
        .ForMember(dest => dest.ProhibitionIds,
            opt => opt.MapFrom(src => src.Prohibitions.Select(item => item.Id)));

    cfg.CreateMap<Shedule, SheduleDTO>();
    cfg.CreateMap<Place, PlaceCardInfo>();
    cfg.CreateMap<ServiceItem, ServiceItemGetDTO>();
    cfg.CreateMap<Charge, ChargeGetDTO>();
    cfg.CreateMap<Discount, DiscountGetDTO>();
    cfg.CreateMap<ServiceItemCreateDTO, ServiceItem>();
    cfg.CreateMap<ServiceItemUpdateDTO, ServiceItem>();
    cfg.CreateMap<ServiceItemDTO, ServiceItem>();

});

var app = builder.Build();

app.SeedDatabase(recreate: false)
    .GetAwaiter()
    .GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseExceptionHandler("/error");
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