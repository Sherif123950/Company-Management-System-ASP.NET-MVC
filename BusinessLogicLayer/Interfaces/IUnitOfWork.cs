using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IDepartementRepository DepartementRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        Task<int> CompleteAsync();
    }
}
