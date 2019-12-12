using SellersWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellersWebMVC.Services.Exceptions;

namespace SellersWebMVC.Services
{
    public class SellerService
    {
        private readonly SellersWebMVCContext _context;
        //readonly = somente leitura
        public SellerService(SellersWebMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
            //deixar a operação como sincona / primeiro termina a aplicação ai depois gera os dados
            // depois fazer uma operação assincrona.
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First(); não é mais preciso devida ao tratamento que ira colocar a ID do departamento
            _context.Add(obj); // para inserir obj no banco de dados
            _context.SaveChanges(); // para confirmar a gravação no banco de dados.
            
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
            //para achar os vendedores
        }

        public void Remove (int id)
        {
            var obj = _context.Seller.Find(id); // para achar o ID do vendedor que será a referencia para o processamento
            _context.Seller.Remove(obj); // atravez do metodo remove ira remover o vendedor
            _context.SaveChanges(); // para gravar no banco de dados.
            
        }

        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new KeyNotFoundException("Id not fund");
            }

            try
            {
                _context.Update(obj);//atualiza o banco passando as informçaões.
                _context.SaveChanges(); //grava as informações no banco. 

            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            //serve para atualizar o banco pelo ID do banco estando no banco de dados.
        }

    }
}