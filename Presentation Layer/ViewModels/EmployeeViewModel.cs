using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Presentation_Layer.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [MaxLength(20, ErrorMessage = "Max Length Is 20 Charachter")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Charachter")]
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age's Range Must Be From 22 To 35")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime Hiredate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Departement")]
        public int? DepartementID { get; set; }
        [InverseProperty("Employees")]
        public Departement Departement { get; set; }
    }
}
