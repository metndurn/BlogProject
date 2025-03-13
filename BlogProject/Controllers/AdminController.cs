using BlogProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	public class AdminController : Controller
	{
		//burada veritabanına baglanıp asagıda cözuyoruz
		private readonly BlogDbContext _context;

		public AdminController(BlogDbContext context)//baglanan verıtabanı cozumlenır
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Blogs()//butun blogları ıstıyorum
		{
			var blogs = _context.Blogs.ToList();
			return View(blogs);
		}
		public IActionResult EditBlog(int id)//blogu duzenlemek ıcın olusturuldu
		{
			var blog = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();//blog ıd sıne gore blogu cekmek ıcın where sorgusu yazıldı
			return View(blog);
		}
	}
}
