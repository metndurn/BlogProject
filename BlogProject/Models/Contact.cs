namespace BlogProject.Models
{
	public class Contact/*veritabanına iletisim ile ilgili bilgileri giriyoruz*/
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Message { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
