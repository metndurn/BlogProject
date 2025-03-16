using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Identity
{
	/*burada ayrıca bır verıtabanı bırlestırdık sadece burada gecerlı ıslemler yapacak
	 dıger verıtabanına karısmayacak*/
	public class BlogIdentityDbContext : IdentityDbContext<BlogIdentityUser, BlogIdentityRole, string>
	{
		/*ıdentityleri burada dbcontexte bagladık builder tarzında bir options ekledık*/
		public BlogIdentityDbContext(DbContextOptions<BlogIdentityDbContext> options) : base(options)
		{

		}
	}
}
