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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert( Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id); // carrega somente o vendedor sem o department
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // esta linha busca o vendedor e o departamento do vendedor
        }

        public void Remove( int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            // este if verifica se não existe algum objeto com - 
            // - este id no banco de dado
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
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
