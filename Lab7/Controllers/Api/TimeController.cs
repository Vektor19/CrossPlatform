using Lab7.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Lab7.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        [Route("kyiv")]
        public IActionResult GetKyivTime([FromQuery] string dateTime)
        {
            try
            {
                if (!DateTime.TryParse(dateTime, out var inputDate))
                {
                    var currentKyivTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "FLE Standard Time");
                    return Ok(new
                    {
                        KyivTime = currentKyivTime.ToString("yyyy-MM-ddTHH:mm:ss")
                    });
                }
                var convertedKyivTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(inputDate, "FLE Standard Time");
                return Ok(new
                {
                    KyivTime = convertedKyivTime.ToString("yyyy-MM-ddTHH:mm:ss")
                });
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }
    }
}
