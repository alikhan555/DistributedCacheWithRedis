using DistributedCacheWithRadis.Models;
using DistributedCacheWithRedis.Models;
using DistributedCacheWithRedis.Service.DistributedCache;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DistributedCacheWithRedis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCacheService _distributedCacheService;

        public HomeController(ILogger<HomeController> logger, IDistributedCacheService distributedCacheService)
        {
            _logger = logger;
            _distributedCacheService = distributedCacheService;
        }

        public IActionResult Index()
        {
            // Setting data to Cache
            List<EmployeeModel> model = [
                new EmployeeModel { Id = 101, Name = "Ali", CNIC = "1000000000001", Exp = 5, Age = 10 },
                new EmployeeModel { Id = 102, Name = "Ahmed", CNIC = "1000000000002", Exp = 6, Age = 20 },
                new EmployeeModel { Id = 103, Name = "Saif", CNIC = "1000000000003", Exp = 7, Age = 30 },
            ];

            _distributedCacheService.SetAsync("key", model, TimeSpan.FromMinutes(2));

            return View();
        }

        public IActionResult Privacy()
        {
            // Setting data to Cache
            List<EmployeeModel> cachedData = _distributedCacheService.GetAsync<List<EmployeeModel>>("key").GetAwaiter().GetResult();
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
