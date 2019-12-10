using SellersWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellersWebMVC.Services
{
    public class SellerService
    {
        private readonly SellersWebMVCContext _context;
        //readonly = somente leitura
        public SellerService (SellersWebMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
            //deixar a operação como sincona / primeiro termina a aplicação ai depois gera os dados
            // depois fazer uma operação assincrona.
        }

        public void Insert (Seller obj)
        {
            var department = _context.Department.FirstOrDefault(); // cria o objeto departamento e joga dentro do objeto departamento
            obj.DepartmentId = department.Id; //passa o obj departamento para o id do departamento
            _context.Add(obj); // para inserir obj no banco de dados
            _context.SaveChanges(); // para confirmar a gravação no banco de dados.
        }

    }
}