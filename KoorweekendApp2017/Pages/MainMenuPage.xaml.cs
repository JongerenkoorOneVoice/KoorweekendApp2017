using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages.Koorweekend2017;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
    public partial class MainMenuPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MainMenuPage()
        {
            InitializeComponent();
			LoadItems();

            
        }

		public void LoadItems()
		{
			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Home",
				IconSource = "contacts.png",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Repertoire",
				IconSource = "icon.png",
				TargetType = typeof(RepertoirePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Koorleden",
				IconSource = "reminders.png",
				TargetType = typeof(ContactArchivePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Agenda",
				IconSource = "reminders.png",
				TargetType = typeof(EventArchivePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Koorweekend 2017",
				IconSource = "reminders.png",
				TargetType = typeof(Koorweekend2017Page)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Instellingen",
				IconSource = "reminders.png",
				TargetType = typeof(SettingsArchivePage)
			});

			Setting showErrorLogInMenu = App.Database.Settings.GetByKey("showErrorLogInMenu");

			if (showErrorLogInMenu != null)
			{
				Boolean showLog = JsonConvert.DeserializeObject<Boolean>(showErrorLogInMenu.Value);
				if (showLog)
				{
					masterPageItems.Add(new MasterPageItem
					{
						Title = "DebugLog",
						IconSource = "reminders.png",
						TargetType = typeof(LogArchivePage)
					});
				}
			}



			listView.ItemsSource = masterPageItems;
		}

    }

    public class MasterPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}