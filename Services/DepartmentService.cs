using Microsoft.EntityFrameworkCore;
using SellersWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellersWebMVC.Services
{
    public class DepartmentService
    {

        private readonly SellersWebMVCContext _context;
        //readonly = somente leitura
        public DepartmentService(SellersWebMVCContext context)
        {
            _context = context;
        }

        //public List<Department> FindAll()
        //{
        //    return _context.Department.OrderBy(x => x.Name).ToList();

        //    processamento sinclono

        //}

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
            /*
             * Async -> padrão adotado na linguagem para programação assíncrona 
             * Task -> componente central da programação assíncrona normalmente é executado de forma assíncrona em um thread do pool de threads em vez de sincronicamente no thread do aplicativo principal
             * await -> avisa para o compilador que a chamada é assíncrona
             */

        }
    }

}
/*
 * Erro de uma classe dentro da outra
 */
