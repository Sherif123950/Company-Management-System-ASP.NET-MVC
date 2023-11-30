using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "This Email Is Not Valid")]
		public string Email { get; set; }
	}
}
