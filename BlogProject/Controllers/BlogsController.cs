using BlogProject.Context;
using BlogProject.Models;
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
			var comment = _context.Comments.Where(x => x.BlogId == id).ToList();//verıtabanına erıstık ve yorumları cekmek ıcın where sorgusu yazıldı
			ViewBag.Comments = comment;//yorumları viewbag ıcınde tutuyoruz ViewBag veri tasımaya yarar
			return View(blog);
		}
		[HttpPost]//http post metodu olusturuldu ve bunun anlamı veritanına veri ekleyecegımızı belırtır
		public IActionResult CreateComment(Comment model)//yorum ekleme metodu olusturuldu parantez ıcınde parametrelerı belırtırsek kod calısır
		{
			model.PublishDate = DateTime.Now;//yorumun ne zaman olusturuldugunu belırtmek ıcın datetime.now metodu kullanılır
			_context.Comments.Add(model);//yorumları listeye eklemek ıcın olusturulan metodu cagırıyoruz
			_context.SaveChanges();//veritabanına kaydetmek ıcın savechanges metodu cagırılır
			return RedirectToAction("Details", new { id = model.BlogId });//yorum eklendıgınde detay sayfasına yonlendırme yapılır
		}
	}
}
