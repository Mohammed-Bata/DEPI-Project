using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<ReviewRepository>();

            return services;
        }
    }
}
