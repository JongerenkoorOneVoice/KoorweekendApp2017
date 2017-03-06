using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
    public partial class ScorePage : ContentPage
    {
        public ScorePage()
        {
            InitializeComponent();
            App.AppWebService.ChoirWeekend.Game2.GetAll().ContinueWith((args) =>
            {
                var x = args;
            });
        }
    }
}
