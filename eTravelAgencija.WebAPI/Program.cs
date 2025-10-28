using eCommerce.WebAPI.Filters;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("BasicAuthentication", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BasicAuthentication" } },
            new string[] { }
        }
    });
});

builder.Services.AddAutoMapper(typeof(OfferMappingProfile).Assembly);



builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOfferService, OfferService>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IOfferHotelService, OfferHotelService>();
builder.Services.AddTransient<IOfferImageService, OfferImageService>();
builder.Services.AddTransient<IHotelImageService, HotelImageService>();


builder.Services.AddDbContext<eTravelAgencijaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Lockout.AllowedForNewUsers = false;
})
    .AddEntityFrameworkStores<eTravelAgencijaDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    string[] roles = { "Radnik", "Direktor", "Korisnik" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new Role { Name = role, Description = $"{role} role" });
    }

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
