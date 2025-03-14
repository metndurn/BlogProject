using BlogProject.Context;
using BlogProject.Models;
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
		public IActionResult DeleteBlog(int id)//blogu silmek ıcın olusturuldu
		{
			var blog = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();//blog ıd sıne gore blogu cekmek ıcın where sorgusu yazıldı
			_context.Blogs.Remove(blog);//blogu sıldık
			_context.SaveChanges();//degısıklıklerı kaydettık
			return RedirectToAction("Blogs");//blogları gosteren sayfaya yonlendırdık
		}

		//edit post islemi
		[HttpPost]//veritabanına verileri gondermek ıcın kullanıyoruz
		public IActionResult EditBlog(Blog model)//Blog turunde model belirtiyoruz bu sabit olacak
		{
			var blog = _context.Blogs.Where(x => x.Id == model.Id).FirstOrDefault();//blog turunden model belirttik veritabanına kayıt etmesı ıcın
			blog.Name = model.Name;//blogun adını degıstırdık
			blog.Description = model.Description;//blogun acıklamasını degıstırdık
			blog.ImageUrl = model.ImageUrl;//blogun resmını degıstırdık
			blog.Tags = model.Tags;//blogun etıketlerını degıstırdık
			_context.SaveChanges();
			return RedirectToAction("Blogs");
		}
		//status degıstırme yani toggle blogu ac kapa 
		public IActionResult ToggleStatus(int id)//blogun durumunu degıstırmek ıcın olusturuldu
		{
			var blog = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();
			if (blog.Status == 1)//blogun durumu 1 ise
			{
				blog.Status = 0;//blogun durumunu 0 yap
			}
			else
			{
				blog.Status = 1;//degılse blogun durumunu 1 yap
			}
			_context.SaveChanges();
			return RedirectToAction("Blogs");//blogları gosteren sayfaya yonlendırdık
		}
	}
}
