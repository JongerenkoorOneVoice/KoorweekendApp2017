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
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (ent.Text == "156")
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "U heeft de volgende locatie & vraag 1 vrijgespeeld", "OK");
                    ent.Text = null;
                }
                else if (ent.Text == null)
                {
                    //await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "Vul Een Barcode In", "OK");
                    ent.Text = null;
                }
                else if (ent.Text != "156")
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "Geen Correcte Code", "OK");
                    ent.Text = null;
                }
            });
        }
    }
}