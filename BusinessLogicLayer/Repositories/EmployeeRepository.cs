using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly MvcApplicationDbContext _dbcontext;

        public EmployeeRepository(MvcApplicationDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IQueryable<Employee> GetEmployeeByAddress(string Address)
        => _dbcontext.Employees.Where(E => E.Address == Address);

        public IQueryable<Employee> Search(string Name)
        => _dbcontext.Employees.Where(E => E.Name.Trim().ToLower().Contains(Name.Trim().ToLower()));
    }
}
