using System;
using System.Collections.Generic;

namespace Presentation_Layer.ViewModels
{
	public class UserViewModel
	{
        public string Id { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public IEnumerable<string> Roles { get; set; }
        public UserViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }

    }
}
