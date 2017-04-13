using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

using Xamarin.Forms;
using KoorweekendApp2017.Models;
using static KoorweekendApp2017.BusinessObjects.AppWebService;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
    public partial class Koorweekend2017Spel2Page : ContentPage
    {
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        //public Task<List<ChoirWeekendGame2Assignment>> Assignments = App.AppWebService.ChoirWeekend.Game2.GetAll();
        public Koorweekend2017Spel2Page()
        {
            InitializeComponent();
            this.BindingContext = this;
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            if (Assignment.Question.IsOpenQuestion == true)
            {
                ToolbarItems.Add(new ToolbarItem("Add", "Score.png", () => { score(); }));
            }
            ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning(); }));
            ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
            Bonusvraag.Clicked += BonusvraagClicked;
            Refresh.Clicked += RefreshClicked;
            Locatie.Clicked += LocatieClicked;
            SetupTekst();
        }

        void BonusvraagClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Koorweekend2017Spel2.Bonusvragen());
            });
        }

        void RefreshClicked(object sender, EventArgs e)
        {
            List<ChoirWeekendGame2Assignment> Beantw = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == true && x.Settings.IsBonus == false);
            Beantw.OrderBy(i => i.Settings.ConsecutionIndex);
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            Assignment.Question.IsOpenQuestion = false;
            App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(Assignment);
            for (int i = 0; i < Beantw.Count; i++)
            {
                ChoirWeekendGame2Assignment Temp = Beantw[i];
                Temp.Result.Score = 0;
                Temp.Question.IsOpenQuestion = false;
                Temp.Result.Time = 0;
                App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(Temp);
            }

            /*Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Koorweekend2017Spel2.GvraagPage());
            });*/
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
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            if (HuidigeAssL.Count >= 1)
            {
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
                        if(result.Text ==  Assignment.Location.Code)
                        {
                            Assignment.Question.IsOpenQuestion = true;
                            App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(Assignment);
                            await Navigation.PopAsync();
                            await DisplayAlert("Scanned Barcode", "Telefoon is nu de hoofdtelefoon", "OK");
                        }
                        else if (result.Text == HuidigeAss.Location.Code)
                        {
                            await Navigation.PopAsync();
                            await Navigation.PushAsync(new GvraagPage());
                        }
                        else if (result.Text != HuidigeAss.Location.Code)
                        {
                            await Navigation.PopAsync();
                            await DisplayAlert("Scanned Barcode", "Geen correcte barcode", "OK");
                        }
                    });
                };
            }
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
        void SetupTekst()
        {
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);

            if (Assignments.Count != 0)
            {
                Uitleg1.Text = Assignment.Settings.Description;
                Uitleg2.Text = Assignment.Location.Description;
            }
            else
            {
                Uitleg1.Text = "Geen tekst gevonden, haal data op van het internet";
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        }
    }
}
