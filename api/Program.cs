using System.Text;
using api.Data.Implementation;
using api.Data.interfaces;
using api.Helpers;
using api.Interfaces;
using api.Mapping;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Add services to the container.
builder.Services.AddScoped<IOnboardingService, OnboardingService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOnboardingService, OnboardingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserServices>();
//builder.Services.AddScoped<IFavRepo,FavRepo>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));

builder.Services.AddScoped<IAddress, AddressService>();
builder.Services.AddScoped<INotification, NotificationService>();
builder.Services.AddScoped<ICategory, CategoryService>();

builder.Services.AddScoped<IfavServices, FavService>();
builder.Services.AddScoped<IOfferServce, OfferService>();

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<FoodtekDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key is null"))),
        };
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
