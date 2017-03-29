using System;
using System.Collections.Generic;
using System.Text;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class DailyBibleVersSingle : ContentPage
	{
		public String Today {get; set;}

		public DailyBibleVersSingle()
		{
			InitializeComponent();


		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			var todaysVerse = BindingContext as DailyBreadVerse;

			Today = todaysVerse.ts.ToString("d MMM yyyy");
			Source.Text = todaysVerse.source;
			Text.Text = todaysVerse.text.hsv;
			                         
		}



	}
}
