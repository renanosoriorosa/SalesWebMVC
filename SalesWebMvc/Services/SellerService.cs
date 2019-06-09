using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Data;
using System.Runtime.Serialization;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //sincrona
        //public List<Seller> FindAll()
        //{
        //    return _context.Seller.ToList();
        //}

        // assincrona
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //sincrona
        //public void Insert( Seller obj)
        //{
        //    _context.Add(obj); // é feito em memoria
        //    _context.SaveChanges(); // acessa o BD, nela deve ter a versao async
        //}

        // assincrona
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); // é feito em memoria
            await _context.SaveChangesAsync(); // acessa o BD, nela deve ter a versao async
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id); // carrega somente o vendedor sem o department
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // esta linha busca o vendedor e o departamento do vendedor
        }

        public async Task  RemoveAsync( int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            // este if verifica se não existe algum objeto com - 
            // - este id no banco de dado
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }

    [Serializable]
    internal class DbConcurrencyException : Exception
    {
        public DbConcurrencyException()
        {
        }

        public DbConcurrencyException(string message) : base(message)
        {
        }

        public DbConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
