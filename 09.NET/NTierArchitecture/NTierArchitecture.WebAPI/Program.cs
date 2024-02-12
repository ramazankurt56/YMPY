using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NTierArchitecture.Business;
using NTierArchitecture.Business.Mapping;
using NTierArchitecture.Business.Services;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.WebAPI.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    //opt.LogTo(Console.WriteLine, LogLevel.Information);
});


builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 6;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Dependency Injection
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<IClassRoomService, ClassRoomManager>();
builder.Services.AddScoped<IAppUserService, AppUserManager>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//Application
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true, //tokený gönderen kiþi bilgisi
        ValidateAudience = true, //tokený kullanacak site ya da kiþi bilgisi
        ValidateIssuerSigningKey = true, //tokenýn güvenlik anahtarý üretmesini saðlayan güvenlik sözcüðü
        ValidateLifetime = true, //tokenun yaþam süresini kontrol etmek istiyor musunuz
        ValidIssuer = "Taner Saydam",
        ValidAudience = "IT Desk Angular App",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My secret key my secret key aþlsdkaskdlþaskþldþklasd"))
    };
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
