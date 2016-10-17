using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
    public partial class RepertoirePage : ContentPage
    {
        public RepertoirePage()
        {           
			try
			{
				InitializeComponent();
				songListView.ItemSelected += OnSongSelected;
				List<Song> songs = RestHelper.GetRestDataFromUrl<Song>("http://www.jongerenkooronevoice.nl/songs/all").Result;
				songs = songs.OrderBy(song => song.Title).ToList();
				songListView.ItemsSource = songs;


			}
			catch (Exception ex)
			{
				var a = ex.Message;
			}
        }

		void OnSongSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Song;
			if (item != null)
			{
				Navigation.PushAsync(new SongSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				songListView.SelectedItem = null;

				//IsPresented = false;
			}
		}
    }
}
