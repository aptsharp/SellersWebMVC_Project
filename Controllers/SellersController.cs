using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Services;

namespace SellersWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerservice;

        public SellersController(SellerService sellerservice)
        {
            _sellerservice = sellerservice;
        }

        public IActionResult Index()
        {
            var list = _sellerservice.FindAll();
            return View(list);
        }

        //IActionResult => tipo de retorno de todas as ações.
        public IActionResult Create()
        {
            return View();
        }
        



    }
}