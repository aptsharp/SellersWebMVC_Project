using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Models;

namespace SellersWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {

            List<Department> list = new List<Department>();

            list.Add(new Department { Id = 1, Name = "Computador" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            return View(list);
        }
    }
}