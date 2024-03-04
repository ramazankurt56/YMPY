using Keycloak.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Keycloak.WebAPI.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
    {
        // Keycloak ile bağlantı kurmak için bir HttpClient örneği oluşturun.
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:8080/admin/master/console/#/myrealm/users/add-user");

        // Kullanıcı bilgilerini içeren bir JSON nesnesi oluşturun.
        var user = new
        {
            email = model.Email,
            firstName = model.FirstName,
            lastName = model.LastName,
        
        };

        // Kullanıcıyı kaydetmek için bir POST isteği gönderin.
        var response = await httpClient.PostAsync("/add-user", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // Yanıtın başarılı olup olmadığını kontrol edin.
        if (response.IsSuccessStatusCode)
        {
            // Kullanıcı başarıyla kaydedildi.
            return Ok();
        }
        else
        {
            // Kullanıcı kaydedilemedi. Hata mesajını döndürün.
            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(error);
        }
        
    }
    [HttpGet]
   
    public IActionResult GetAll()
    {
        return Ok();
    }
}
