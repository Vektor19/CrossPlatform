using Microsoft.AspNetCore.Mvc;
using Lab6.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
namespace Lab6.Controllers
{
    public class TimeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:3000/api/time/kyiv?dateTime="+DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

            var jsonData = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(jsonData);
            ViewData["KyivTime"] = json.GetString("kyivTime");
            return View();
        }
    }
}
