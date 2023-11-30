using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Departement")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
