using BlogProject.Context;
using BlogProject.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*burada dbcontexti implemente etmemiz gerekiyor yani projeyi ilk ayaga kald�rma yer� buras� olacakt�r*/
builder.Services.AddDbContext<BlogDbContext>();

//Identity db context servislerini eklemek �c�n kullan�l�r
/*blogidentitydbcontexti etmemiz gerekiyor yani kullan�c� ile ilgili olanlar�n yer� olacakt�r*/
builder.Services.AddDbContext<BlogIdentityDbContext>(options =>
{
    /*degiskenler uzer�nden atamalar yap�l�p eklemeler yap�ld� */
    var configuration = builder.Configuration;
	var connectionString = configuration.GetConnectionString("DefaultConnection");
	options.UseSqlServer(connectionString);//baglant�lar tamam �se burada baglant� olusmas� laz�m
});

//admin giris yapmad�g�nda kullan�c�lar� yonlend�rmek �c�n otomat�k b�r sayfaya yonlend�rmek �c�n kullan�l�r
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
		options.LoginPath = "/Blogs/Login";//g�r�s yapmad�g�nda yonlend�rmek �c�n kullan�l�r ekstra islem olarak �ndex s�l�n�p log�n yaz�ld�
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
