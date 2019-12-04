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
    }
}
