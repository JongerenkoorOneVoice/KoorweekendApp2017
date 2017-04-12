using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
    public partial class TypeBarcodePage : ContentPage
    {
        //public Task<List<ChoirWeekendGame2Assignment>> Assignments = App.AppWebService.ChoirWeekend.Game2.GetAll();
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        public TypeBarcodePage()
        {
            InitializeComponent();

            oke.Clicked += OnOkeClicked;
            Annul.Clicked += OnAnullClicked;
        }

        void OnAnullClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
                ent.Text = null;
            });
        }
        void OnOkeClicked(object sender, EventArgs e)
        {
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
            x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            if (HuidigeAssL.Count >= 1)
            {
                HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
                ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ent.Text == HuidigeAss.Location.Code)
                    {
                        //await Navigation.PopAsync();
                        await Navigation.PushAsync(new GvraagPage());
                        ent.Text = null;
                    }
                    else if (ent.Text == null)
                    {
                        await DisplayAlert("Barcode", "Vul Een Barcode In", "OK");
                        await Navigation.PopAsync();
                        ent.Text = null;
                    }
                    else if (ent.Text != HuidigeAss.Location.Code)
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Barcode", "Geen Correcte Code", "OK");
                        ent.Text = null;
                    }
                });
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*List<ChoirWeekendGame2Assignment> HuidigeAssO = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            var Old = HuidigeAssO.Count;*/

            Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();

            /*List<ChoirWeekendGame2Assignment> HuidigeAssN = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            var New = HuidigeAssN.Count;
            if(New == Old-1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                });
            }*/
        }
    }
}