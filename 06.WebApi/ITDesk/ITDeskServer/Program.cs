using ITDeskServer.Context;
using ITDeskServer.Middleware;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(configure =>
{
    configure.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader() //contentType => application/json application/text mime type
            .AllowAnyOrigin() //www.taner.com www.ahmet.com
            .AllowAnyMethod(); //httpget httppost httput
    });
});
#region Dependency Injection
#endregion

#region Authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>//Kullan�c� giri� kontrol sistemi- pop�ler iki y�ntem JWT ve Cookie
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true, //token� g�nderen ki�i bilgisi
        ValidateAudience = true, //token� kullanacak site ya da ki�i bilgisi
        ValidateIssuerSigningKey = true, //token�n g�venlik anahtar� �retmesini sa�layan g�venlik s�zc���
        ValidateLifetime = true, //tokenun ya�am s�resini kontrol etmek istiyor musunuz
        ValidIssuer = "Ramazan Kurt",
        ValidAudience = "IT Desk Angular App",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key 1234 ... my secret key my secret key my secret key 1234 ..."))
    };
});
#endregion
#region DbContext and Identity
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Data Source=KURT\\SQLEXPRESS;Initial Catalog=ITDeskDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
});
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 2;
}).AddEntityFrameworkStores<AppDbContext>();
#endregion
#region Presentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion
#region Middlewares
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ExtensionsMiddleware.CreateFirstUser(app);
ExtensionsMiddleware.AutoMigration(app);


app.Run();
#endregion

