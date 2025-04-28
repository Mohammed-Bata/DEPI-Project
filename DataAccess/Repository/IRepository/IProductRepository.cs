using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> SearchByName(string name);
        Product GetById(int id);
        void Add(Product category);
        void Update(Product product);
        
        void Delete(int id);
    }
    
}
