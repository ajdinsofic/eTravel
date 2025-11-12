using eTravelAgencija.WebAPI.Authentication;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Mapping;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "eTravelAgencija API", Version = "v1" });

    // ✅ Dodaj JWT definiciju
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // ✅ Napravi zahtjev da sve rute traže JWT ako imaju [Authorize]
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOfferService, OfferService>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IOfferHotelService, OfferHotelService>();
builder.Services.AddTransient<IOfferImageService, OfferImageService>();
builder.Services.AddTransient<IHotelImageService, HotelImageService>();
builder.Services.AddTransient<IHotelRoomsService, HotelRoomsService>();
builder.Services.AddTransient<IOfferPlanDayService, OfferPlanDayService>();
builder.Services.AddTransient<IVoucherService, VoucherService>();
builder.Services.AddTransient<IResevationPreviewService, ReservationPreviewService>();
//builder.Services.AddTransient<IBookingPreviewService, BookingPreviewService>();


builder.Services.AddDbContext<eTravelAgencijaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Lockout.AllowedForNewUsers = false;
})
    .AddEntityFrameworkStores<eTravelAgencijaDbContext>()
    .AddDefaultTokenProviders();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
