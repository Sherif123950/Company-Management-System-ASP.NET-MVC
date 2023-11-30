using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
        public string Fname { get; set; }
		[Required]
		public string Lname { get; set; }
		[Required]
		public bool IsAgree { get; set; }
	}
}
