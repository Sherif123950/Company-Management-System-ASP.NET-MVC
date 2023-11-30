using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly MvcApplicationDbContext _mvcApplicationDbContext;

        public GenericRepository(MvcApplicationDbContext mvcApplicationDbContext)
        {
            _mvcApplicationDbContext = mvcApplicationDbContext;
        }
        public async Task AddAsync(T Entity)
        =>await _mvcApplicationDbContext.Set<T>().AddAsync(Entity);
         
        public void Delete(T Entity)
        => _mvcApplicationDbContext.Set<T>().Remove(Entity);

        public async Task< IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>)await _mvcApplicationDbContext.Employees.Include(E => E.Departement).ToListAsync();
            }
            return await _mvcApplicationDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task< T> GetByIdAsync(int id)
        {
            var Employee = await _mvcApplicationDbContext.Set<T>().FindAsync(id);
            return Employee;
        }

        public void Update(T Entity)
        =>_mvcApplicationDbContext.Set<T>().Update(Entity);
           
        
    }
}
