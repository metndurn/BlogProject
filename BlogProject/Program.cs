using BlogProject.Context;
using BlogProject.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*burada dbcontexti implemente etmemiz gerekiyor yani projeyi ilk ayaga kaldýrma yerý burasý olacaktýr*/
builder.Services.AddDbContext<BlogDbContext>();

//Identity db context servislerini eklemek ýcýn kullanýlýr
/*blogidentitydbcontexti etmemiz gerekiyor yani kullanýcý ile ilgili olanlarýn yerý olacaktýr*/
builder.Services.AddDbContext<BlogIdentityDbContext>(options =>
{
    /*degiskenler uzerýnden atamalar yapýlýp eklemeler yapýldý */
    var configuration = builder.Configuration;
	var connectionString = configuration.GetConnectionString("DefaultConnection");
	options.UseSqlServer(connectionString);//baglantýlar tamam ýse burada baglantý olusmasý lazým
});

//admin giris yapmadýgýnda kullanýcýlarý yonlendýrmek ýcýn otomatýk býr sayfaya yonlendýrmek ýcýn kullanýlýr
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
		options.LoginPath = "/Blogs/Login";//gýrýs yapmadýgýnda yonlendýrmek ýcýn kullanýlýr ekstra islem olarak ýndex sýlýnýp logýn yazýldý
	});

builder.Services.AddIdentity<BlogIdentityUser,BlogIdentityRole>()
    .AddEntityFrameworkStores<BlogIdentityDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blogs}/{action=Index}/{id?}");

app.Run();
