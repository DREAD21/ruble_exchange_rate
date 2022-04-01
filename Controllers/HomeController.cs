using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ruble_exchange_rate.Models;

namespace ruble_exchange_rate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("/")]
        public IActionResult Index()
        {
            DateTime date = DateTime.Today;
            string url = string.Format("http://api.currencylayer.com/live?access_key=2bca30d02f7b0504ae1b2e160dd15c1a");
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            RubleInfo Response = JsonConvert.DeserializeObject<RubleInfo>(response);
            var bucks = Response.quotes.USDRUB;
            var euro = Response.quotes.USDEUR;
            ViewBag.eu = string.Format("{0:0.00}", bucks/euro);
            ViewBag.buck = string.Format("{0:0.00}", bucks);
            ViewBag.date_ = date.ToString("D");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
