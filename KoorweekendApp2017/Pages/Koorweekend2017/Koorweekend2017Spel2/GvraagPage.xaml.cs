using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
	public partial class GvraagPage : ContentPage
	{
		public string Answer = null;
		public string CAnswer = "D";
		public string BPunten = "15";
		public GvraagPage()
		{
			InitializeComponent();
			A.Clicked += (sender, e) =>
			{
				Answer = "A";
				Antwoord();
			};
			B.Clicked += (sender, e) =>
			{
				Answer = "B";
				Antwoord();
			};
			C.Clicked += (sender, e) =>
			{
				Answer = "C";
				Antwoord();
			};
			D.Clicked += (sender, e) =>
			{
				Answer = "D";
				Antwoord();
			};
		}
		void Antwoord()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				if (Answer == CAnswer)
				{
					var Antz = (string.Format("Dit was het goede antwoord! Jullie hebben {0} punten verdiend", Convert.ToString(BPunten)));
					await DisplayAlert("Goed!", Antz, "OK");
					await Navigation.PopAsync();

				}
				else if (Answer != CAnswer)
				{
					var AntZ = (string.Format("Het goede antwoord was {0}, jullie hebben helaas geen punten verdiend", Convert.ToString(CAnswer)));
					await DisplayAlert("Helaas", AntZ, "OK");
					await Navigation.PopAsync();

				}
			});
		}
	}
}
