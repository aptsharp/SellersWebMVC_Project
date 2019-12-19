using Microsoft.EntityFrameworkCore;
using SellersWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellersWebMVC.Services
{
    public class SalesRecorsService
    {
        private readonly SellersWebMVCContext _context;
        //readonly = somente leitura
        public SalesRecorsService(SellersWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecords>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //a consulta não é especificada pela simples difinição dela, deve ser refinada.

            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            //faz a pesquisa por LINQ no banco de dados
            //    operação que busca por data
        }



        public async Task<List<IGrouping<Department, SalesRecords>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {

         //consulta não é especificada pela simples difinição dela, deve ser refinada.

        var result = from obj in _context.SalesRecords select obj;
        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        return await result
            .Include(x => x.Seller)
            .Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date)
            .GroupBy(x => x.Seller.Department)
            .ToListAsync();
        }        
    }
}


       
                