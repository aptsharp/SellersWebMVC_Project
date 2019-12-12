using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Models;
using SellersWebMVC.Services;
using SellersWebMVC.Models.ViewModels;

namespace SellersWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
           
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //IActionResult => tipo de retorno de todas as ações.
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
            // ERRO DE REFERENCIA DE OBJTO.
            // erro ocorrido por ter uma classe dentro da outra em SellerService.
        }


        [HttpPost] // anotação que é somente para dizer que é um POST e não um GET (principalmente para evitar vulnerabilidades no site "pentest")
        [ValidateAntiForgeryToken] // para evitar sequestro de sessão // evita ataques CSRF. referencias: https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-3.1 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); 
            /*
             * nameof -> agilidade na manutenção do site caso mude o nome da pagina Index()[linha 21] será mudado automaticamente tambem [linha 38]
             */
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // mostra uma resposta basica/generica
            }

            var obj = _sellerService.FindById(id.Value); // acha o obj para processar
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj); // retorna passando o objeto como argumento
        }

        //fazendo o post de confirmação do banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound(); // mostra uma resposta basica/generica
            }

            var obj = _sellerService.FindById(id.Value); // acha o obj para processar
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj); // retorna passando o objeto como argumento

        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);


        }

        



    }
}