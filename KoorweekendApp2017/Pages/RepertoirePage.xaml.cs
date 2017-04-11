using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using KoorweekendApp2017.Tasks;
using Plugin.Connectivity.Abstractions;

namespace KoorweekendApp2017.Pages
{
    public partial class RepertoirePage : ContentPage
    {
        public List<Song> Songs = new List<Song>();
		public List<SongOccasion> SongOccasions = new List<SongOccasion>();

		//songOccasionsFilter
		public String oldSearchValue = String.Empty;

        public RepertoirePage()
        {
            InitializeComponent();
            //songListView.ItemsSource = 
			try
			{
				songListView.ItemSelected += OnSongSelected;
                
                mainSearchBar.TextChanged += OnTextChanged;
                mainSearchBar.Focused += MainSearchFocused;

				SettingsHelper.TogglePullToRefresh(songListView);
				songListView.Refreshing += ReloadSongsFromWebservice;

				App.Network.ConnectivityTypeChanged += (object sender, ConnectivityTypeChangedEventArgs e) => {
					SettingsHelper.TogglePullToRefresh(songListView);
				};


				songOccasionsFilter.IsVisible = false;
				songOccasionsFilter.SelectedIndexChanged += (sender, e) =>
				{
					if (songOccasionsFilter.SelectedIndex != -1)
					{
						FilterRepertoire();

							songOccasionsFilter.BackgroundColor = Color.Red;
							songOccasionsFilter.TextColor = Color.White;
							songOccasionsFilter.Unfocus();

					}
				};

				songOccasionsFilter.Unfocused += (sender, e) => {
					if (songOccasionsFilter.SelectedIndex == -1 || songOccasionsFilter.SelectedIndex == 0)
					{
						songOccasionsFilter.Unfocus();
						songOccasionsFilter.IsVisible = false;
					}
				
				};


				/*ToolbarItems.Add(new ToolbarItem("Filter", "Filter.png", () =>
				{
						songOccasionsFilter.IsVisible = true;
						songOccasionsFilter.Focus();
				}));*/



				SetupRepertoireDataForList();
				SetupPickerData();


            }
			catch (Exception ex)
			{
				var a = ex.Message;
			}
        }

		private void FilterRepertoire()
		{
			List<Song> filteredSongs = new List<Song>();
			filteredSongs.AddRange(Songs);


			if (songOccasionsFilter.SelectedIndex != -1 && songOccasionsFilter.SelectedIndex != 0)
			{
				string filterName = songOccasionsFilter.Items[songOccasionsFilter.SelectedIndex];
				int currentFilterId = SongOccasions.Find(x => x.Title == filterName).Id;
				filteredSongs = filteredSongs.FindAll(x => x.SongOccasions.Contains(currentFilterId));
			}

			string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();
			if (searchValue == String.Empty)
			{
				songListView.ItemsSource = filteredSongs;
			}
			else
			{
				filteredSongs = filteredSongs.FindAll(
				x => x.Title.ToLower().Contains(searchValue) == true || x.Lyrics.ToLower().Contains(searchValue) == true);
				songListView.ItemsSource = filteredSongs;
			}
		}

		private void SetupPickerData()
		{
			SongOccasions = App.Database.SongOccasions.GetAll();
			List<int> indexedOccasionsFromSongs = new List<int>();
			foreach (Song song in Songs)
			{
				foreach (int occasionId in song.SongOccasions)
				{
					if (!indexedOccasionsFromSongs.Contains(occasionId))
						indexedOccasionsFromSongs.Add(occasionId);
				}
			}

			List<String> pickerFilterNames = SongOccasions
				.Where(x => indexedOccasionsFromSongs.Contains(x.Id))
				.Select(x => x.Title).ToList();

			songOccasionsFilter.Items.Add("Alle");
			foreach (String filterName in pickerFilterNames)
			{
				songOccasionsFilter.Items.Add(filterName);
			}
		}

		private void SetupRepertoireDataForList()
		{
			Songs = App.Database.Songs.GetAll();
			Songs = Songs.OrderBy(Songs => Songs.Title).ToList();
			songListView.ItemsSource = Songs;
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

			FilterRepertoire();
            
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

		void ReloadSongsFromWebservice(object sender, EventArgs args)
		{
			DataSync.UpdateSongsInDbFromApi(true);
			SetupRepertoireDataForList();
			FilterRepertoire();
			ListView listView = sender as ListView;
			listView.EndRefresh();

		}
    }
}
