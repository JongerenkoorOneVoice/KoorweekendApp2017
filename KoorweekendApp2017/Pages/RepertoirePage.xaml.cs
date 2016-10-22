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
        public List<Song> Songs = new List<Song>();
		public String oldSearchValue = String.Empty;

        public RepertoirePage()
        {
            InitializeComponent();
            //songListView.ItemsSource = 
			try
			{
				songListView.ItemSelected += OnSongSelected;
                Songs = App.Database.Songs.GetAll();
                Songs = Songs.OrderBy(song => song.Title).ToList();
                songListView.ItemsSource = Songs;
                mainSearchBar.TextChanged += OnTextChanged;
                mainSearchBar.Focused += MainSearchFocused;
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

        void OnTextChanged(object sender, EventArgs args)
        {
            string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();

            
            if(searchValue == String.Empty)
            {
                songListView.ItemsSource = Songs;
            }
            else
            {
                List<Song> foundSongs = Songs.FindAll(
                x => x.Title.ToLower().Contains(searchValue) == true || x.Lyrics.ToLower().Contains(searchValue) == true);
                songListView.ItemsSource = foundSongs;
            }
        }

		void MainSearchFocused(object sender, EventArgs args)
		{

			string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();
			if (String.IsNullOrEmpty(searchValue) && oldSearchValue.Length > 0)
			{
				//mainSearchBar.Unfocus();
				songListView.Focus();
			}

		}
    }
}
