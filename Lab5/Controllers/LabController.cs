using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [Authorize]
    public class LabController : Controller
    {

        public IActionResult Lab1()
        {
            return View();
        }
        public IActionResult Lab2()
        {
            return View();
        }
        public IActionResult Lab3()
        {
            return View();
        }
    }
}
