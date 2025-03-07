namespace BlogProject.Models
{
	public class Comment//yorumlar ıcın model olusturuldu
	{
		public int Id { get; set; }//yorum ıd
		public string Name { get; set; }//yorum yapan kısı ısım
		public string Email { get; set; }//yorum yapan kısı email
		public string Message { get; set; }//yorum ıcerıgı
		public int BlogId { get; set; }//yorumun hangı bloga aıt oldugunu belırtmek ıcın blog ıd
		public DateTime PublishDate { get; set; }//yorumun ne zaman olusturuldugunu belırtmek
	}
}
