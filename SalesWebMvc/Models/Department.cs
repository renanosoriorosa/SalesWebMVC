using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sallers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        // QUANDO CRIAR UM METODO CONSTRUTOR COM ARGUMENTOS, NÃO ADD ATRIBUTOS Q SAO COLEÇÕES
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sallers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sallers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
