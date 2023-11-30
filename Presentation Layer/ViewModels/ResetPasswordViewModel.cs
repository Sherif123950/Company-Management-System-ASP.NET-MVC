using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Password Doesn't Match")]
		public string ConfirmNewPassword { get; set; }
	}
}
