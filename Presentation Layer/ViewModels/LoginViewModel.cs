using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "This Email Is Not Valid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
