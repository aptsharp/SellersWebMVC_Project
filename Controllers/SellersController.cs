using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Models;
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


        [HttpPost] // anotação que é somente para dizer que é um POST e não um GET (principalmente para evitar vulnerabilidades no site "pentest")
        [ValidateAntiForgeryToken] // para evitar sequestro de sessão // evita ataques CSRF. referencias: https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-3.1 
        public IActionResult Create(Seller seller)
        {
            _sellerservice.Insert(seller);
            return RedirectToAction(nameof(Index)); 
            /*
             * nameof -> agilidade na manutenção do site caso mude o nome da pagina Index()[linha 21] será mudado automaticamente tambem [linha 38]
             */
        }
        



    }
}