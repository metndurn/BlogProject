using BlogProject.Context;
using BlogProject.Identity;
using BlogProject.Models;
using BlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	[Authorize]//sadece yetkili kısılerın gırebılecegı alanlar ıcın kullanılır
	public class AdminController : Controller
	{
		//burada veritabanına baglanıp asagıda cözuyoruz
		private readonly BlogDbContext _context;
		private readonly UserManager<BlogIdentityUser> _userManager;//bunu eklerken eksık ekledım dıye hata aldım suan ıcın halledildi
		private readonly SignInManager<BlogIdentityUser> _signInManager;

		public AdminController(BlogDbContext context, UserManager<BlogIdentityUser> userManager, SignInManager<BlogIdentityUser> signInManager)//baglanan verıtabanı cozumlenır
		{
			_context = context;
			_userManager = userManager;//baglanan verıtabanı cozumlenır
			_signInManager = signInManager;
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
			_context.SaveChanges();
			return RedirectToAction("Blogs");
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
			return RedirectToAction("Blogs");
		}
		public IActionResult CreateBlog()//blog ekleme sayfası ıcın olusturuldu
		{
			return View();
		}
		[HttpPost]//veritabanına veri eklemek ıcın kullanılır
		public IActionResult CreateBlog(Blog model)//blog turunden model belırttık
		{
			model.PublishDate = DateTime.Now;//blogun ne zaman olusturuldugunu belırtmek ıcın datetime.now metodu kullanılır
			model.Status = 1;//blogun durumunu belırtmek ıcın 1 yazdık
			_context.Blogs.Add(model);
			_context.SaveChanges();
			return RedirectToAction("Blogs");
		}
		public IActionResult Comments(int? blogId)//butun yorumları ıstıyorum
		{
			/*bos liste olusturup sıfıra esitledik if ile kosul olusturup veritabanına
			 hem kayıt yaptı hemde ekranda gosterdık*/
			var comments = new List<Comment>();
			if (blogId == null)
			{
				comments = _context.Comments.ToList();
			}
			else
			{
				comments = _context.Comments.Where(x => x.BlogId == blogId).ToList();
			}
			return View(comments);
		}
		public IActionResult DeleteComment(int id)//yorumu sılmek ıcın olusturuldu
		{
			var comment = _context.Comments.Where(x => x.Id == id).FirstOrDefault();
			_context.Comments.Remove(comment);
			_context.SaveChanges();
			return RedirectToAction("Comments");
		}
		public IActionResult Register()//kullanıcı kayıt sayfası ıcın olusturuldu
		{
			return View();
		}
		[HttpPost]//veritabanına veri eklemek ıcın kullanılır
		public async Task<IActionResult>  Register(RegisterViewModel model)
		{
			if (model.Password == model.RePassword)
			{
				var user = new BlogIdentityUser
				{//hata kodun burada olustu ve username ekleyıp burayı duzelttık
					Name = model.Name,
					Surname = model.Surname,
					Email = model.Email,
					UserName = model.Email,
				};
				//hatalı alandır
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}
			}
			else
			{
				return View();
			}
		}
		public async Task<IActionResult> Logout()//task olarak gonderme yaptık
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Blogs");
		}
	}
}
