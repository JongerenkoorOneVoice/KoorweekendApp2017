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
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            if (HuidigeAssL.Count >= 1)
            {
                HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
                ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
                LocationImage.Source = HuidigeAss.Location.Image;
                LocationDesc.Text = HuidigeAss.Location.Description;

                if (Assignment.Question.IsOpenQuestion == true)
                {
                    Navigate.Text = "Wanneer je de weg echt kwijt bent, kun je navigeren naar deze locatie. Wel zijn er dan strafpunten aan verbonden...";
                    var Lost = new Button() { Text = "Navigeer", HorizontalOptions = LayoutOptions.Center };
                    Stack.Children.Add(Lost);
                    Lost.Clicked += navigeren;

                    ToolbarItems.Add(new ToolbarItem("Add", "Score.png", () => { score(); }));
                }
                ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning(); }));
                ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
                
            }
            else if (HuidigeAssL.Count == 0)
            {
                if (HuidigeAssL.Count >= 1)
                {
                    HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
                    ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
                    LocationImage.Source = HuidigeAss.Location.Image;
                    LocationDesc.Text = HuidigeAss.Location.Description;
                }
                else if (HuidigeAssL.Count == 0)
                {
                    List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
                    Int64 ScoorMax = 0;
                    Int64 ScoorBeh = 0;
                    Stack.Children.Clear();
                    var over = new Label { Text = "Dit was het laatste punt, ga terug naar de parkeerplaats", HorizontalTextAlignment = TextAlignment.Center };
                    Stack.Children.Add(over);

                    if (Assignment.Question.IsOpenQuestion == true)
                    {
                        if (Assignments.Count != 0)
                        {
                            List<ChoirWeekendGame2Assignment> VragenAll = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Settings.IsBonus == false);
                            List<ChoirWeekendGame2Assignment> VragenBeantwoord = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == true && x.Settings.IsBonus == false);
                            List<ChoirWeekendGame2Assignment> Bonusvragen = Assignments.FindAll(
                                x => x.Settings.IsBonus == true && x.IsVisible == true);
                            Bonusvragen.OrderBy(i => i.Settings.ConsecutionIndex);
                            for (int i = 0; i < VragenBeantwoord.Count; i++)
                            {
                                ScoorMax += VragenBeantwoord[i].Settings.MaxScore;
                                ScoorBeh += VragenBeantwoord[i].Result.Score;
                                if (ScoorBeh >= 5)
                                {
                                    ScoorBeh = ScoorBeh - VragenBeantwoord[i].Result.Time;
                                }
                            }
                            var ScoreU = new Label { Text = String.Format("Jullie behaalde score is: {0}/{1}", ScoorBeh, ScoorMax), HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                            var Bonus = new Label { Text = String.Format("Jullie bonuspunten zijn: {0}/50", Bonusvragen[2].Result.Score), HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                            Stack.Children.Add(ScoreU);
                            Stack.Children.Add(Bonus);
                        }
                    }
                    else
                    {
                        var Tekst = new Label { Text = "Zie de hoofdtelefoon voor de behaalde score!", HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                        Stack.Children.Add(Tekst);
                    }
                }
            }
        }
        void scanning()
        {
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            List<ChoirWeekendGame2Assignment> Bonusvragen = Assignments.FindAll(
                x => x.Settings.IsBonus == true && x.IsVisible == true);
            Bonusvragen.OrderBy(i => i.Settings.ConsecutionIndex);
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
                        if (result.Text == Assignment.Location.Code)
                        {
                            Assignment.Question.IsOpenQuestion = true;
                            App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(Assignment);
                            await Navigation.PopAsync();
                            await DisplayAlert("Hoofdtelefoon", "Telefoon is nu de hoofdtelefoon", "OK");
                        }
                        else if (result.Text == HuidigeAss.Location.Code)
                        {
                            await Navigation.PopAsync();
                            await Navigation.PushAsync(new GvraagPage());
                        }
                        else if(result.Text == Bonusvragen[2].Location.Code)
                        {
                            Bonusvragen[2].Result.Score = 50;
                            App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(Bonusvragen[2]);
                            await Navigation.PopAsync();
                            await DisplayAlert("Vla gevonden!", "Gefeliciteerd jullie hebben de vla gevonden! En daarbij 50 punten verdiend", "OK");
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

        void navigeren(object sender, EventArgs e)
        {
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            if (HuidigeAssL.Count >= 1)
            {
                HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
                ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var answer = await DisplayAlert("Navigeren?", "Weet je zeker dat je wilt navigeren en daarbij punten verliezen", "Ja", "Nee");
                    if (answer == true)
                    {
                        HuidigeAss.Result.Time = 5;
                        App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(HuidigeAss);
                        ChoirWeekendBasePosition Location = HuidigeAss.Location.Position;
                        string Lattitude = Convert.ToString(Location.Lattitude);
                        string Longitude = Convert.ToString(Location.Longitude);
                        Device.OpenUri(new Uri(string.Format("https://www.google.nl/maps/dir//{0},{1}/data=!3m1!4b1!4m2!4m1!3e1", Lattitude.Replace(',', '.'), Longitude.Replace(',', '.'))));// vb: https://www.google.nl/maps/dir//longitude,lattitude/data=Auto;fiets;lopen => !3m1!4b1!4m2!4m1!3e1 = Fiets  !3m1!4b1!4m2!4m1!3e0 = Auto  !3m1!4b1!4m2!4m1!3e2 = Lopen
                    }
                });
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            if (HuidigeAssL.Count >= 1)
            {
                HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
                ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
                LocationImage.Source = HuidigeAss.Location.Image;
                LocationDesc.Text = HuidigeAss.Location.Description;
            }
            else if (HuidigeAssL.Count == 0)
            {
               List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
                ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                    x => x.Id.Equals("0") == true);
                Int64 ScoorMax = 0;
                Int64 ScoorBeh = 0;
                Stack.Children.Clear();
                var over = new Label { Text = "Dit was het laatste punt, ga terug naar het startpunt", HorizontalTextAlignment = TextAlignment.Center, FontSize = 15};
                Stack.Children.Add(over);

                if (Assignment.Question.IsOpenQuestion == true)
                {
                    if (Assignments.Count != 0)
                    {
                        List<ChoirWeekendGame2Assignment> VragenAll = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Settings.IsBonus == false);
                        List<ChoirWeekendGame2Assignment> VragenBeantwoord = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == true && x.Settings.IsBonus == false);
                        List<ChoirWeekendGame2Assignment> Bonusvragen = Assignments.FindAll(
                            x => x.Settings.IsBonus == true && x.IsVisible == true);
                        Bonusvragen.OrderBy(i => i.Settings.ConsecutionIndex);
                        for (int i = 0; i < VragenBeantwoord.Count; i++)
                        {
                            ScoorMax += VragenBeantwoord[i].Settings.MaxScore;
                            ScoorBeh += VragenBeantwoord[i].Result.Score;
                            if (ScoorBeh >= 5)
                            {
                                ScoorBeh = ScoorBeh - VragenBeantwoord[i].Result.Time;
                            }
                        }
                        var ScoreU = new Label { Text = String.Format("Jullie behaalde score is: {0}/{1}", ScoorBeh, ScoorMax), HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                        var Bonus = new Label { Text = String.Format("Jullie bonuspunten zijn: {0}/50", Bonusvragen[2].Result.Score), HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                        Stack.Children.Add(ScoreU);
                        Stack.Children.Add(Bonus);
                    }
                }
                else
                {
                    var Tekst = new Label { Text = "Zie de hoofdtelefoon voor de behaalde score!", HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 };
                    Stack.Children.Add(Tekst);
                }
            }
        }
    }
}
