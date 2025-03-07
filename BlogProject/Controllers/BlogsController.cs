using BlogProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	public class BlogsController : Controller
	{
		//veri tabanı baglantısı olusturuldu
		private readonly BlogDbContext _context;

		public BlogsController(BlogDbContext context)//alınan verıtabanı baglantısı ıcın constructor olusturuldu
		{
			_context = context;
		}

		public IActionResult Index()//blog anasayfası ıcın liste olusturuldu
		{
			//var blogs = _context.Blogs.ToList(); //liste halinde veritabanındaki verileri alıyoruz
			var blogs = _context.Blogs.Where(x => x.Status == 1).ToList(); //sadece aktif olanları listelemek ıcın where sorgusu yazıldı
			return View(blogs);
		}

		public IActionResult Details(int id)//blog detay sayfası ıcın olusturuldu parametre verdık cunku her blogun ıd sı farklı olacak
		{
			var blog = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();//veritabanından ıd ye gore blogu cekıyoruz .FirstOrDefault() bir tane demektir
			return View(blog);
		}
	}
}
