using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Tasks;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class NewsArchive : ContentPage
	{
        public ListView NewsListView { get { return newsListView; } }
        public List<News> News = new List<News>();

        public NewsArchive()
		{
            try
            {
                InitializeComponent();
                NewsListView.ItemSelected += OnNewsSelected;
                NewsListView.IsPullToRefreshEnabled = true;
                NewsListView.Refreshing += ReloadnewsFromWebservice;
                SetupNewsDataForList();

            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        private void SetupNewsDataForList()
        {

			NewsListView.ItemsSource = App.Database.News.GetAll().FindAll(x => x.IsVisible != false).OrderByDescending(x => x.LastModified).ThenBy(x => x.Title).ToList();
        }
        void OnNewsSelected(object sender, SelectedItemChangedEventArgs e)
        {
			var item = e.SelectedItem as News;
			if (item != null)
			{
				Navigation.PushAsync(new NewsSingle() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				NewsListView.SelectedItem = null;

				//IsPresented = false;
			}
        }

        void ReloadnewsFromWebservice(object sender, EventArgs args)
        {
			DataSync.UpdateNewsInDbFromApi(true);
			SetupNewsDataForList();
            ListView listView = sender as ListView;
            listView.EndRefresh();

        }
    }

}
