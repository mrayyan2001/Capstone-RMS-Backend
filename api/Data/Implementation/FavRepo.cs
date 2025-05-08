using api.Data.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Implementation
{
    //public class FavRepo : BaseRepo<Bookmark>, IFavRepo
    //{

    //    private readonly FoodtekDbContext _context;

    //    public FavRepo(FoodtekDbContext context):base(context) 
    //    {

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});


    //}
}
