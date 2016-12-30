using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
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
			News.Add(new News()
			{
				LastModified = DateTime.Now,
				Title = "Zomaar een titel die lang genoeg is om op minimaal twee regels uit te komen.",
				Text = "Test"
			});
            NewsListView.ItemsSource = News;
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
            ListView listView = sender as ListView;
            listView.EndRefresh();

        }
    }

}
