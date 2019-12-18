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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
            //deixar a operação como sincona / primeiro termina a aplicação ai depois gera os dados
            // depois fazer uma operação assincrona.
        }

        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First(); não é mais preciso devida ao tratamento que ira colocar a ID do departamento
            _context.Add(obj); // para inserir obj no banco de dados
            await _context.SaveChangesAsync(); // para confirmar a gravação no banco de dados.
            
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
            //para achar os vendedores
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id); // para achar o ID do vendedor que será a referencia para o processamento
            _context.Seller.Remove(obj); // atravez do metodo remove ira remover o vendedor
            await _context.SaveChangesAsync(); // para gravar no banco de dados.
            
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if ( !hasAny)
            {
                throw new KeyNotFoundException("Id not fund");
            }

            try
            {
                _context.Update(obj);//atualiza o banco passando as informçaões.
                await _context.SaveChangesAsync(); //grava as informações no banco. 

            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            //serve para atualizar o banco pelo ID do banco estando no banco de dados.
        }

    }
}