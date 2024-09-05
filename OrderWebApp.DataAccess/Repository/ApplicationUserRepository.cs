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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       

        public void Update(ApplicationUser applicationUserObj)
        {
            _db.ApplicationUsers.Update(applicationUserObj);
          
        }
    }
}
