using DataAccess.DataBase;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Product> SearchByName(string name)
        {
            return _context.Products
                           .Where(p => p.Name.Contains(name))
                           .ToList();
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products
                    .FirstOrDefault(p => p.Id == id);
        }


        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                
            }
        }
    }
}
