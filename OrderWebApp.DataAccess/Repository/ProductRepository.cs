using Microsoft.EntityFrameworkCore;
using OrderWebApp.DataAccess.Data;
using OrderWebApp.DataAccess.Repository.IRepository;
using OrderWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       

        public void Update(Product productObj)
        {
            _db.Products.Update(productObj);
          
        }
        public Product GetByIdAsNoTracking(int id)
        {
            return _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
    }
}
