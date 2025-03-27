using BlogProject.Context;
using BlogProject.Identity;
using BlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly BlogDbContext _context;
		private readonly UserManager<BlogIdentityUser> _userManager;//kullanıcı bır maıl gonderdıgın var mı yok mu dıye kontrol etmek ıcın kullandık varsa login islemleri olacak
		private readonly SignInManager<BlogIdentityUser> _signInManager;//kullanıcı gırıs yaptıgında otomatık olarak gırıs yapacak

		public AccountController(BlogDbContext context, UserManager<BlogIdentityUser> userManager, SignInManager<BlogIdentityUser> signInManager)//alınan verıtabanı baglantısı ıcın constructor olusturuldu
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Login()//gırıs sayfası ıcın olusturuldu
		{
			return View();
		}
		[HttpPost]//veritabanı baglantısı olmayacak sadece kontrol amaclı olacak
		public async Task<IActionResult> Login(LoginViewModel model)//gırıs yapmak ıcın olusturuldu
		{
			var user = await _userManager.FindByEmailAsync(model.Email);//kullanıcı var mı yok mu dıye kontrol etmek ıcın olusturulan metodu cagırıyoruz
			if (user == null)
			{
				return View();//kullanıcı yoksa gırıs yapamaz varsa giris yapsın
			}
			var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);//kullanıcı varsa sıfre kontrolu yapılır

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Admin");
			}
			else
			{
				return View();
			}

		}
	}
}
