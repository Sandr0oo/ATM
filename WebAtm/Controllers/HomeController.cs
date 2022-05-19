using Class.ATM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAtm.Models;

namespace WebAtm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public int CassetCount { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Cassette> cassettes = new()
            {
                new Cassette(20, 1000, false),
                new Cassette(100, 5, true),
                new Cassette(200, 10, true),
                new Cassette(500, 10, true),
                new Cassette(1000, 10, true),
                new Cassette(2000, 10, true),
                new Cassette(5000, 10, true),
                new Cassette(100, 5, true),
            };

            CassetCount = cassettes.Count();

            ATM atm = new ATM(cassettes);
            return View(atm);
        }

        [HttpPost]
        public IActionResult Index(int needSumm = 1200)
        {
            List<Cassette> cassettes = new();

            for (int i = 1; i <= 8; i++)
            {
                var nominal = int.Parse(HttpContext.Request.Form["nomainal" + i]);
                var banknoteCount = int.Parse(HttpContext.Request.Form["BanknoteCount" + i]);
                var isWork = HttpContext.Request.Form["isWork" + i] == "true" ? true : false;
                cassettes.Add(new Cassette(nominal, banknoteCount, isWork));
                
            }
            ATM atm = new ATM(cassettes);
            atm.GetMoney(needSumm);
            return View(atm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
