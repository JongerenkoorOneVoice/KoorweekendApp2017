using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
    public partial class MainMenuPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MainMenuPage()
        {
            InitializeComponent();
	

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