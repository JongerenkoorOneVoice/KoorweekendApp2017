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
        public LocatiePage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Add", "Score.png", () => { score(); }));
            ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning(); }));
            ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
            Lost.Clicked += navigeren;

        }
        void scanning()
        {
            var scanner = new ZXingScannerPage();

            Navigation.PushAsync(scanner);

            scanner.OnScanResult += (result) =>
            {
                // Stop scanning
                scanner.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.Text != "ik ben daniel")
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    }
                    else if (result.Text == "ik ben daniel")
                    {
                        await Navigation.PushAsync(new HomePage());
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                var answer = await DisplayAlert("Navigeren?", "Weet je zeker dat je wilt navigeren en daarbij punten verliezen", "Ja", "Nee");
                if (answer == true)
                {
                    string Location = "51.4080517,4.4332083";
                    Device.OpenUri(new Uri(string.Format("https://www.google.nl/maps/dir//{0}/data=!3m1!4b1!4m2!4m1!3e1", Location)));// vb: https://www.google.nl/maps/dir//longitude,lattitude/data=Auto;fiets;lopen => !3m1!4b1!4m2!4m1!3e1 = Fiets  !3m1!4b1!4m2!4m1!3e0 = Auto  !3m1!4b1!4m2!4m1!3e2 = Lopen
                }
            });
        }
    }
}
