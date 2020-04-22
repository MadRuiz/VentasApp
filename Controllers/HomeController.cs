using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using VentasApp.Models;

namespace VentasApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContextBase _context;
        public HomeController(ContextBase context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            double ventasTotales = 0;
            var ventas = (from v in _context.Ventas select v).ToList();
            foreach (var item in ventas)
            {
                ventasTotales = ventasTotales + item.Total;
            }

            ViewBag.ventas = ventasTotales;
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
