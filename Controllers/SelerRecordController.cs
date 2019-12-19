using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Services;

namespace SellersWebMVC.Controllers
{
    public class SelerRecordController : Controller
    {
        private readonly SalesRecorsService _salesRecorsService;

        public SelerRecordController(SalesRecorsService salesRecorsService)
        {
            _salesRecorsService = salesRecorsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");

            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecorsService.FindByDateAsync(minDate, maxDate); 
            return View(result);
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}