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
    public class DepartementRepository : GenericRepository<Departement>,IDepartementRepository
    {
        private readonly MvcApplicationDbContext _dbcontext;
        public DepartementRepository(MvcApplicationDbContext dbContext):base(dbContext)
        {
            _dbcontext = dbContext;
        }
        #region MyRegion
        //public int Add(Departement departement)
        //{
        //    _dbcontext.Departements.Add(departement);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(Departement departement)
        //{
        //    _dbcontext.Departements.Remove(departement);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Departement> GetAll()
        //{
        //    return _dbcontext.Departements.AsNoTracking().ToList();
        //}

        //public Departement GetById(int id)
        //{
        //    return _dbcontext.Find<Departement>(id);
        //}

        //public int Update(Departement departement)
        //{
        //    _dbcontext.Departements.Update(departement);
        //    return _dbcontext.SaveChanges();
        //} 
        #endregion
    }
}
