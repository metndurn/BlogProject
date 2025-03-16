using Microsoft.AspNetCore.Identity;

namespace BlogProject.Identity
{
	/*Identity ıslemlerı yapılacak ve yol olarak verdıgım kısım verıtabanına baglanıp olusacak*/
	public class BlogIdentityUser : IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
	}
}
