using Lab7.Database;
using Microsoft.AspNetCore.Mvc;
using Lab7.Database.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Lab7.RequestModels;
using System.Net.Http.Json;
using System.Net.Http;

namespace Lab7.Controllers
{
    public class DataViewsController : Controller
    {
        private readonly CallCentersDbContext _context;

        public DataViewsController(CallCentersDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Staff()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:3000/api/staffs");
            IEnumerable<Staff>? staffs = null;

            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                staffs = JsonSerializer.Deserialize<List<Staff>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception)
            {

            }
            return View(staffs);
        }
        public async Task<IActionResult> Customers()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:3000/api/customers");
            IEnumerable<Customer>? customers = null;

            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                customers = JsonSerializer.Deserialize<List<Customer>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception)
            {

            }
            return View(customers);
        }
        public async Task<IActionResult> Contracts()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:3000/api/contracts");
            IEnumerable<Contract>? contracts = null;

            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                contracts = JsonSerializer.Deserialize<List<Contract>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception)
            {

            }
            return View(contracts);
        }
        [HttpGet]
        public IActionResult SearchContracts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchContracts(SearchContractsRequest request)
        {
            if (request == null)
            {
                return View();
            }
            HttpClient httpClient = new HttpClient();
            var formattedDate= request.DateTime == null ? null : request.DateTime.Value.ToString("O");
            var response = await httpClient.GetAsync($"http://localhost:3000/api/contracts/search?dateTime={formattedDate}&&customerIds={request.CustomerIds}&&otherDetails={request.OtherDetails}");

            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<IEnumerable<Contract>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(results);
            }

            return View();
        }
    }
}
