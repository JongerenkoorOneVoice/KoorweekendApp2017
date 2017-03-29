using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
    public partial class Koorweekend2017Spel2Page : ContentPage
    {
        public Koorweekend2017Spel2Page()
        {
            InitializeComponent();
            this.BindingContext = this;

            ToolbarItems.Add(new ToolbarItem("Add", "Score.png", () => { score(); }));
            ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning(); }));
            ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
            Bonusvraag.Clicked += BonusvraagClicked;
            GVraag.Clicked += GVraagClicked;
            Locatie.Clicked += LocatieClicked;
        }

        void BonusvraagClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Koorweekend2017Spel2.Bonusvragen());
            });
        }

        void GVraagClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Koorweekend2017Spel2.GvraagPage());
            });
        }

        void LocatieClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Koorweekend2017Spel2.LocatiePage());
            });
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
    }
}
