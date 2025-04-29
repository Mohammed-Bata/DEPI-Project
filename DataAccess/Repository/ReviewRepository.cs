using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataBase;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
        public async Task<Review> GetReviewWithProductAsync(int productId)
        {
            return await _context.Reviews
                .Include(r => r.Product)
                //.ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(r => r.ProductId == productId);
        }
    }
}
