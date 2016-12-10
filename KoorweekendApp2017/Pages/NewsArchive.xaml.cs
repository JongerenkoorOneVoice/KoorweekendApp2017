using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class NewsArchive : ContentPage
	{
        public ListView NewsListView { get { return newsListView; } }
        public List<String> News = new List<String>();

        public NewsArchive()
		{
            try
            {
                InitializeComponent();
                NewsListView.ItemSelected += OnContactSelected;
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
            NewsListView.ItemsSource = News;
        }
        void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
        {
                Navigation.PushAsync(new NewsSingle());
                NewsListView.SelectedItem = null;

        }

        void ReloadnewsFromWebservice(object sender, EventArgs args)
        {
            ListView listView = sender as ListView;
            listView.EndRefresh();

        }
    }

}
