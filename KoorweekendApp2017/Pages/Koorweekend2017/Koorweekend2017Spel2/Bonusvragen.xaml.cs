using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
    public partial class Bonusvragen : ContentPage
    {
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        public Bonusvragen()
        {
            InitializeComponent();
            this.BindingContext = this;
            if (Assignments.Count != 0)
            {
                List<ChoirWeekendGame2Assignment> Bonusvragen = Assignments.FindAll(
                x => x.Settings.IsBonus == true);
                Bonusvragen.OrderBy(i => i.Settings.ConsecutionIndex);
                Foto.Text = Bonusvragen[0].Location.Description;
                Lied.Text = Bonusvragen[1].Location.Description;
                Scan.Text = Bonusvragen[2].Location.Description;
            }
            else
            {
                Foto.Text = "Geen tekst gevonden";
            }
        }
    }
}
