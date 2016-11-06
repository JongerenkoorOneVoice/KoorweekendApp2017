using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Tasks;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class EventSinglePage : ContentPage
	{

		public EventSinglePage()
		{
			InitializeComponent();
			songForEventListView.ItemSelected += OnSongSelected;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var currentEvent = BindingContext as Event;

			List<Song> songsForEvent = new List<Song>();
			if (currentEvent.Songs != null)
			{
				foreach (int songId in currentEvent.Songs)
				{
					// Check if the song is still in the database
					Song songForEvent = App.Database.Songs.GetById(songId);
					if (songForEvent == null)
					{
						// Try to update the data from the webservice
						DataSync.UpdateSongsInDbFromApi(true);
						songForEvent = App.Database.Songs.GetById(songId);
						if (songForEvent == null)
						{
							songForEvent = new Song()
							{
								Id = 0,
								Title = "Dit liedje is verwijderd",
								Lyrics = "Het liedje dat je zoekt staat niet meer in de app en je kan het daarom niet meer bekijken\r\n\nDenk je dat dit niet kopt? Probeer eerst om de app te sluiten en opnieuw te starten met internet aan.\r\nNeem als het dan nog niet werkt even contact op met het bestuur.",
								LastModified = DateTime.Now
						};
						}
					}
					songsForEvent.Add(songForEvent);
				}
				songsHeaderTitle.IsVisible = songsForEvent.Count > 0;
				currentEvent.SongItems = songsForEvent;
			}

			songForEventListView.ItemsSource = currentEvent.SongItems;
		}

		void OnSongSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Song;
			if (item != null)
			{
				Navigation.PushAsync(new SongSinglePage() { BindingContext = item });
				songForEventListView.SelectedItem = null;
			}
		}

	}
}
