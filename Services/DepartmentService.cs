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

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }

}
/*
 * Erro de uma classe dentro da outra
 */