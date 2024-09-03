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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       

        public void Update(Company companyObj)
        {
            _db.Companies.Update(companyObj);
          
        }
    }
}
