using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalDash.Models;
using DigitalDash.Services;

namespace DigitalDash.Controllers
{
    public class HomeController : Controller
    {
        Converter converter = new Converter();

        public IActionResult Index()
        {
            
            List<Chart> charts = converter.Convert();
            IEnumerable<Chart> display = charts;
            

            return View(display);
        }

        public IActionResult Settings()
        {
            
            ViewData["Message"] = "Edit Chart Types";
            List<String> displaytypes = new List<string> { "bar","pie","spline","donut","scatter"};
            List<String> filenames = converter.FileNames();
            List < List < String >> data = new List<List<String>>();
            data.Add(displaytypes);
            data.Add(filenames);
            return View(data);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult ChartType(string chart,string type)
        {
            converter.SetChartType(chart,type);
            return Redirect("~/Home/Settings");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
