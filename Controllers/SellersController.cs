using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SellersWebMVC.Models;
using SellersWebMVC.Services;
using SellersWebMVC.Models.ViewModels;
using System.Diagnostics;
using SellersWebMVC.Services.Exceptions;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        //IActionResult => tipo de retorno de todas as ações.
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
            // ERRO DE REFERENCIA DE OBJETO.
            // erro ocorrido por ter uma classe dentro da outra em SellerService.
        }


        [HttpPost] // anotação que é somente para dizer que é um POST e não um GET (principalmente para evitar vulnerabilidades no site "pentest")
        [ValidateAntiForgeryToken] // para evitar sequestro de sessão // evita ataques CSRF. referencias: https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-3.1 
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
                //Faz a verificação mesmo estando com o JavaScript da maquina do usuario desligada.
            }
           await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
            /*
             * nameof -> agilidade na manutenção do site caso mude o nome da pagina Index()[linha 21] será mudado automaticamente tambem [linha 38]
             */
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null" }); // mostra uma resposta basica/generica
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // acha o obj para processar
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not fund" });
            }

            return View(obj); // retorna passando o objeto como argumento
        }

        //fazendo o post de confirmação do banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null ************** " });  // mostra uma resposta basica/generica
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // acha o obj para processar
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null *************" });
            }

            return View(obj); // retorna passando o objeto como argumento

        }

        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null ********" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null **********" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID is null *********" }); ;
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (DllNotFoundException)
            {
                return BadRequest();
            }
        }

        public IActionResult Error(String message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // em paginas publicadas não se deve usar este metodo pois os erros podem ser explorados por teceiros, ou seja somente em ambiente de desenvolvimento

                /*
                 * as informações que o tratamento de erro mostra são:
                 * Rastreamento de pilha
                 * Parâmetros de cadeia de caracteres de consulta (se houver algum)
                 * Cookies (se houver algum)
                 * Cabeçalhos
                 * ***
                 * RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                 * Current? -> opcional por causa do ponto de interrogação, se for null usando o operador de coalescência nula
                 * ?? -> coalescência nula: retorna o valor do operador esquerdo se não for null; caso contrario ele avaliara o operador direito e retornara seu resultado. Não ira avaliar 
                 * operador direito se o operador esquedo for avaliado como não nulo. 
                 * 
                 */
            };
            return View(viewModel);
        }

    }
}