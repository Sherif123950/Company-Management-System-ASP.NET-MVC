using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Email
	{
        public int Id { get; set; }
        public string Subject { get; set; }
		public string Recipients { get; set; }
        public string Body { get; set; }
	}
}
