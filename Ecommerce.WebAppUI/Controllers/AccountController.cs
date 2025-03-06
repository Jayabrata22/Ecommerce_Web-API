using Ecommerce.WebAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.WebAppUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Checks if input data is valid
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:44301/api/User/register", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login"); // Redirect to Login page after successful registration
                }

                ModelState.AddModelError("", "Registration failed.");
            }
            return View(model);
        }
    }
}
