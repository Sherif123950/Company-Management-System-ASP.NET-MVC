using DataAccessLayer.Models;
using System.Net;
using System.Net.Mail;

namespace Presentation_Layer.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email Email) 
		{ 
			var Client= new SmtpClient("smtp.gmail.com",587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("shikoashraf12345@gmail.com", "zlyppqhdpbhvisje");
			Client.Send("shikoashraf12345@gmail.com", Email.Recipients, Email.Subject, Email.Body);
		}
	}
}
