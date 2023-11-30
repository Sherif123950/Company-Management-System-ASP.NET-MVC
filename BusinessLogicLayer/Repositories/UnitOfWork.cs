using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly MvcApplicationDbContext _dbContext;

        public IDepartementRepository DepartementRepository { get; set ; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public UnitOfWork(MvcApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            DepartementRepository = new DepartementRepository(_dbContext);
            EmployeeRepository = new EmployeeRepository(_dbContext);
        }
        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public void Dispose()
        => _dbContext.Dispose();
    }
}
