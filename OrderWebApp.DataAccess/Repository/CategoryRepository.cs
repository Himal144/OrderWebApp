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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       

        public void Update(Category categoryObj)
        {
            _db.Categories.Update(categoryObj);
          
        }
    }
}
