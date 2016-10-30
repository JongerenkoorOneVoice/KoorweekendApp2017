using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{

            InitializeComponent();
            var linkOpen = new TapGestureRecognizer();
            linkOpen.Tapped += (s, e) => {
                Device.OpenUri(new Uri(((Label)s).Text));
            };
            label.GestureRecognizers.Add(linkOpen);



			NavigationPage.SetTitleIcon(this, "app_logo_red.jpg");
        }

    }
}
