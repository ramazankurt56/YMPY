using AutoMapper;
using BookStoreServer.WebApi.Context;
using BookStoreServer.WebApi.DTOs;
using BookStoreServer.WebApi.Maping;
using BookStoreServer.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using UserModel = BookStoreServer.WebApi.Models.User;
namespace BookStoreServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public AuthController(AppDbContext context,IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Register(RegisterDto request)
        {
            //UserModel user =new()
            //{
            //    Name = request.Name,
            //    Lastname = request.Lastname,
            //    Email = request.Email,
            //    Password = request.Password,
            //    Username = request.Username,
            //};
            UserModel user=mapper.Map<UserModel>(request);
            context.Add(user);
            context.SaveChanges();
            return Ok(new { message = "Kayıt işlemi başarıyla gerçekleşti." });
        }
        [HttpPost]
        public IActionResult Login(LoginDto request)
        {
            UserModel user = context.Users.Where(p => p.Username == request.UsernameOrEmail || p.Email == request.UsernameOrEmail).FirstOrDefault() ;
            if(user is null)
            {
                return BadRequest("Kullanıcı kaydı bulunamadı");
            }
            if (user.Password !=request.Password)
            {
                return BadRequest("Şifre Yanlış");
            }
            string token = JwtService.CreatToken(user);
            return Ok(new LoginResponseDto(token:token,UserId:user.Id,UserName:user.GetName()));
        }
    }
}
