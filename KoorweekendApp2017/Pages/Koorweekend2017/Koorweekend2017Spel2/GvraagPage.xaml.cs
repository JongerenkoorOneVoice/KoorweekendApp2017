using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017.Koorweekend2017Spel2
{
	public partial class GvraagPage : ContentPage
	{
        public List<ChoirWeekendGame2Assignment> Assignments = App.Database.ChoirWeekend2017.Game2.GetAll();
        public string Answer = null;
		public GvraagPage()
		{
			InitializeComponent();
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
            if(HuidigeAssL.Count >= 1)
            { 
            
            ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];
            QuestionDesc.Text = HuidigeAss.Question.Question;

            if (HuidigeAss.Question.Image.ToLower() != "x")
            {
                QuestionImage.Source = HuidigeAss.Question.Image;
            }

                if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 1)
                {
                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 0)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[0] != "x")
                        {
                            var A = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[0], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            FirstRow.Children.Add(A);
                            A.Clicked += (sender, e) =>
                            {
                                Answer = HuidigeAss.Question.MultipleChoiceAnswers[0];
                                Antwoord();
                            };
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 1)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[1] != "x")
                        {
                            var B = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[1], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            SecondRow.Children.Add(B);
                            B.Clicked += (sender, e) =>
                            {
                                Answer = HuidigeAss.Question.MultipleChoiceAnswers[1];
                                Antwoord();
                            };
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 2)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[2] != "x")
                        {
                            var C = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[2], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            FirstRow.Children.Add(C);
                            C.Clicked += (sender, e) =>
                            {
                                Answer = HuidigeAss.Question.MultipleChoiceAnswers[2];
                                Antwoord();
                            };
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 3)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[3] != "x")
                        {
                            var D = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[3], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            SecondRow.Children.Add(D);
                            D.Clicked += (sender, e) =>
                            {
                                Answer = HuidigeAss.Question.MultipleChoiceAnswers[3];
                                Antwoord();
                            };
                        }
                    }
                }

            }
            else if(HuidigeAssL.Count == 0)
            {
                QuestionDesc.Text = "Er zijn geen vragen (Meer) beschikbaar";
            }
		}
		void Antwoord()
		{
            List<ChoirWeekendGame2Assignment> HuidigeAssL = Assignments.FindAll(
                x => x.Question.IsMultipleChoice == true && x.Question.IsOpenQuestion == false && x.Settings.IsBonus == false);
            HuidigeAssL.OrderBy(i => i.Settings.ConsecutionIndex);
            ChoirWeekendGame2Assignment HuidigeAss = HuidigeAssL[0];

            Device.BeginInvokeOnMainThread(async () =>
			{
				if (Answer == HuidigeAss.Question.Answer)
				{
					var Antz = (string.Format("Dit was het goede antwoord! Jullie hebben {0} punten verdiend", HuidigeAss.Settings.MaxScore));
					await DisplayAlert("Goed!", Antz, "OK");
					await Navigation.PopAsync();

				}
				else if (Answer != HuidigeAss.Question.Answer)
				{
					var AntZ = (string.Format("Het goede antwoord was {0}, jullie hebben helaas geen punten verdiend", Convert.ToString(HuidigeAss.Question.Answer)));
					await DisplayAlert("Helaas", AntZ, "OK");
					await Navigation.PopAsync();

				}
			});
		}
	}
}
