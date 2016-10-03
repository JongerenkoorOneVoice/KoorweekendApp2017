using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class KoorweekendApp2017Page : MasterDetailPage
	{
		public KoorweekendApp2017Page()
		{
			InitializeComponent();

			var layout = new StackLayout();
			layout.Children.Add(new Label { Text = "Test1", Margin = new Thickness(0, 50, 0, 0) });
			layout.Children.Add(new Label { Text = "Test2" });
	
		

			Master = new ContentPage()
			{
				Content = new Label
				{
					Text = "menu",
					Margin = new Thickness(10, 20, 10 ,10),
					TextColor = Color.White
				},
				Title = "MasterPage",
				BackgroundColor = Color.Pink
			};

			Detail = new NavigationPage(new ContentPage()
			{
				Content =  layout,
				Title = "DetailPage"
			});
			Title = "MasterDetail";
		
		}
	}
}
