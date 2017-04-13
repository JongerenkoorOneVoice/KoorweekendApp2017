using KoorweekendApp2017.Models;
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
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        public Int64 ScoorMax = 0;
        public Int64 ScoorBeh = 0;
        public ScorePage()
        {
            InitializeComponent();
            /*App.AppWebService.ChoirWeekend.Game2.GetAll().ContinueWith((args) =>
            {
                var x = args;
            });*/
            if (Assignments.Count != 0)
            {
                List<ChoirWeekendGame2Assignment> VragenAll = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Settings.IsBonus == false);
                List<ChoirWeekendGame2Assignment> VragenBeantwoord = Assignments.FindAll(x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == true && x.Settings.IsBonus == false);
                for (int i = 0; i < VragenBeantwoord.Count; i++)
                {
                    ScoorMax += VragenBeantwoord[i].Settings.MaxScore;
                    ScoorBeh += VragenBeantwoord[i].Result.Score;
                    if (ScoorBeh >= 5)
                    {
                        ScoorBeh = ScoorBeh - VragenBeantwoord[i].Result.Time;
                    }
                }
                Score.Text = String.Format("{0}/{1}", ScoorBeh, ScoorMax);
                Locaties.Text = String.Format("{0}/{1}", VragenBeantwoord.Count, (VragenAll.Count));
            }
        }
    }
}
