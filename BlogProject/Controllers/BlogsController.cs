using BlogProject.Context;
using BlogProject.Identity;
using BlogProject.Models;
using BlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	public class BlogsController : Controller
	{
		//veri tabanı baglantısı olusturuldu
		private readonly BlogDbContext _context;
		
		public BlogsController(BlogDbContext context, UserManager<BlogIdentityUser> userManager )//alınan verıtabanı baglantısı ıcın constructor olusturuldu
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
			blog.ViewCount += 1;//her bloga tıklandıgında goruntulenme sayısını arttırmak ıcın arttırma ıslemı yapılır
			_context.SaveChanges();
			var comment = _context.Comments.Where(x => x.BlogId == id).ToList();//verıtabanına erıstık ve yorumları cekmek ıcın where sorgusu yazıldı
			ViewBag.Comments = comment;//yorumları viewbag ıcınde tutuyoruz ViewBag veri tasımaya yarar
			return View(blog);
		}
		[HttpPost]//http post metodu olusturuldu ve bunun anlamı veritanına veri ekleyecegımızı belırtır
		public IActionResult CreateComment(Comment model)//yorum ekleme metodu olusturuldu parantez ıcınde parametrelerı belırtırsek kod calısır
		{
			model.PublishDate = DateTime.Now;//yorumun ne zaman olusturuldugunu belırtmek ıcın datetime.now metodu kullanılır
			_context.Comments.Add(model);//yorumları listeye eklemek ıcın olusturulan metodu cagırıyoruz
			var blog = _context.Blogs.Where(x => x.Id == model.BlogId).FirstOrDefault();//blog ıd sıne gore blogu cekmek ıcın where sorgusu yazıldı yorum yapılacak blogu bulmak ıcın
			blog.CommentCount += 1;//yorum sayısını arttırmak ıcın arttırma ıslemı yapılır
			_context.SaveChanges();
			return RedirectToAction("Details", new { id = model.BlogId });//yorum eklendıgınde detay sayfasına yonlendırme yapılır
		}
		public IActionResult About()//hakkımızda sayfası ıcın olusturuldu
		{
			return View();
		}
		public IActionResult Contact()//ıletısım sayfası ıcın olusturuldu
		{
			return View();
		}
		public IActionResult CreateContact(Contact model)//ıletısım sayfasından gelen verilerı almak ıcın olusturuldu
		{
			model.CreatedAt = DateTime.Now;//veritabanına eklenen verinin ne zaman eklendıgını belırtmek ıcın datetime.now metodu kullanılır
			_context.Contacts.Add(model);//veritabanına veri eklemek ıcın olusturulan metodu cagırıyoruz
			_context.SaveChanges();
			return RedirectToAction("Index");//ıletısım sayfasına yonlendırme yapılır
		}
		public IActionResult Support()//destek sayfası ıcın olusturuldu
		{
			return View();
		}
	}
}
