using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Context
{
	/*veritabanı baglantısını olusturmak ıcın bu yapıyı kullanıyoruz bu yapının adı codefirst*/
	public class BlogDbContext : DbContext
	{
		/*veritabanını comfigöre edecegız*/
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//database yolunu ve adını belirtiyoruz
			optionsBuilder.UseSqlServer("Data Source = .; Database = BlogDb; Integrated Security = true; TrustServerCertificate = True;");
		}
		public DbSet<Blog> Blogs { get; set; }//veritabanında ki adını burada belirtip hangi tabloya baglanacagını belirtiyoruz
		public DbSet<Comment> Comments { get; set; }//veritabanında ki adını burada belirtip hangi tabloya baglanacagını belirtiyoruz
	}
}
