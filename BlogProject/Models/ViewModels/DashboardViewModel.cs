namespace BlogProject.Models.ViewModels
{
	public class DashboardViewModel
	{
		//toplam blog sayısı
		public int TotalBlogCount { get; set; }
		//toplam goruntulenme sayısı
		public int TotalViewCount { get; set; }
		//toplam begeni sayısı
		public Blog MostViewedBlog { get; set; }
		//en cok goruntulenme alan blog
		public Blog LatestBlog { get; set; }
		//en son eklenen blog
		public int TotalCommentCount { get; set; }
		//toplam yorum sayısı
		public Blog MostCommentedBlog { get; set; }
		//en cok yorum alan blog
		public int TodayCommentCount { get; set; }
		//bugun yapılan yorum sayısı
	}
}
