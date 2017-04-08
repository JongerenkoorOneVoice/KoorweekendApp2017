using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
	public partial class LocatiePage : ContentPage
	{
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        public LocatiePage()
		{
            InitializeComponent();
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
            if (HuidigeAssL.Count >= 1)
            {
                ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
                LocationImage.Source = HuidigeAss.Location.Image;
                LocationDesc.Text = HuidigeAss.Location.Description;
                Navigate.Text = "Wanneer de weg echt kwijt bent, kun je navigeren naar deze locatie. Wel zijn er dan strafpunten aan verbonden...";
                var Lost = new Button() { Text = "Navigeer", HorizontalOptions = LayoutOptions.Center };
                Stack.Children.Add(Lost);

                ToolbarItems.Add(new ToolbarItem("Add", "Score.png", () => { score(); }));
                ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning(); }));
                ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
                Lost.Clicked += navigeren;
            }
            else if(HuidigeAssL.Count == 0)
            {
                LocationDesc.Text = "Dit was het laatste punt, ga terug naar de parkeerplaats";
            }
		}
		void scanning()
		{
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
            ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
            var scanner = new ZXingScannerPage();

			Navigation.PushAsync(scanner);

			scanner.OnScanResult += (result) =>
			{
				// Stop scanning
				scanner.IsScanning = false;

				// Pop the page and show the result
				Device.BeginInvokeOnMainThread(async () =>
				{
                    if (result.Text != HuidigeAss.Location.Code)
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    }
                    else if (result.Text == HuidigeAss.Location.Code)
                    {
                        await Navigation.PushAsync(new GvraagPage());
                    }
                });
			};
		}

		void score()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Navigation.PushAsync(new Koorweekend2017Spel2.ScorePage());
			});
		}

		void typing()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Navigation.PushAsync(new Koorweekend2017Spel2.TypeBarcodePage());
			});
		}

		void navigeren(object sender, EventArgs e)
		{
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
            ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
            Device.BeginInvokeOnMainThread(async () =>
			{
				var answer = await DisplayAlert("Navigeren?", "Weet je zeker dat je wilt navigeren en daarbij punten verliezen", "Ja", "Nee");
				if (answer == true)
				{
					ChoirWeekendBasePosition Location = HuidigeAss.Location.Position;
					Device.OpenUri(new Uri(string.Format("https://www.google.nl/maps/dir//{0},{1}/data=!3m1!4b1!4m2!4m1!3e1", Location.Lattitude, Location.Longitude)));// vb: https://www.google.nl/maps/dir//longitude,lattitude/data=Auto;fiets;lopen => !3m1!4b1!4m2!4m1!3e1 = Fiets  !3m1!4b1!4m2!4m1!3e0 = Auto  !3m1!4b1!4m2!4m1!3e2 = Lopen
				}
			});
		}
	}
}
