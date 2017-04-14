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
            ChoirWeekendGame2Assignment Assignment = Assignments.Find(
                x => x.Id.Equals("0") == true);
            if (HuidigeAssL.Count >= 1)
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
                            FirstRow.Orientation = StackOrientation.Horizontal;
                            FirstRow.Children.Add(A);
                            if (Assignment.Question.IsOpenQuestion == true)
                            {
                                A.Clicked += (sender, e) =>
                                {
                                    Answer = HuidigeAss.Question.MultipleChoiceAnswers[0];
                                    Antwoord();
                                };
                            }
                            else if(Assignment.Question.IsOpenQuestion == false)
                            {
                                A.TextColor = Color.Gray;

                                    A.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await DisplayAlert("Niet mogelijk", "Alleen de hoofdtelefoon kan de antwoorden geven op de vragen", "OK");
                                    });
                                };
                                var Verder = new Button { Text = "Volgende locatie", Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                                ThirdRow.Children.Add(Verder);
                                Verder.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        HuidigeAss.Question.IsOpenQuestion = true;
                                        App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(HuidigeAss);
                                        await Navigation.PopAsync();
                                    });
                                };
                            }
                        }
                        else
                        {
                            if (Assignment.Question.IsOpenQuestion == true)
                            {
                                var Type = new Entry { HorizontalOptions = LayoutOptions.Center, Placeholder = "Code van leiding", Keyboard = Keyboard.Numeric };
                                FirstRow.Children.Add(Type);
                                var Typed = new Button { Text = "Oke", Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                                FirstRow.Children.Add(Typed);
                                Typed.Clicked += (sender, e) =>
                                {
                                    Answer = Type.Text;
                                    Antwoord();
                                    Type.Text = null;
                                };
                            }
                            else if (Assignment.Question.IsOpenQuestion == false)
                            {
                                var Verder = new Button { Text = "Volgende locatie", Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                                ThirdRow.Children.Add(Verder);
                                Verder.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        HuidigeAss.Question.IsOpenQuestion = true;
                                        App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(HuidigeAss);
                                        await Navigation.PopAsync();
                                    });
                                };
                            }
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 1)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[1] != "x")
                        {
                            var B = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[1], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            if (HuidigeAss.Question.MultipleChoiceAnswers[1].Length < 15)
                            {
                                FirstRow.Orientation = StackOrientation.Horizontal;
                                SecondRow.Children.Add(B);
                            }
                            else
                            {
                                FirstRow.Orientation = StackOrientation.Vertical;
                                FirstRow.Children.Add(B);
                            }
                            if (Assignment.Question.IsOpenQuestion == true)
                            {
                                B.Clicked += (sender, e) =>
                                {
                                    Answer = HuidigeAss.Question.MultipleChoiceAnswers[1];
                                    Antwoord();
                                };
                            }
                            else if (Assignment.Question.IsOpenQuestion == false)
                            {
                                B.TextColor = Color.Gray;

                                B.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await DisplayAlert("Niet mogelijk", "Alleen de hoofdtelefoon kan de antwoorden geven op de vragen", "OK");
                                    });
                                };
                            }
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 2)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[2] != "x")
                        {
                            var C = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[2], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };

                            FirstRow.Children.Add(C);
                            if (Assignment.Question.IsOpenQuestion == true)
                            {
                                C.Clicked += (sender, e) =>
                                {
                                    Answer = HuidigeAss.Question.MultipleChoiceAnswers[2];
                                    Antwoord();
                                };
                            }
                            else if (Assignment.Question.IsOpenQuestion == false)
                            {
                                C.TextColor = Color.Gray;

                                C.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await DisplayAlert("Niet mogelijk", "Alleen de hoofdtelefoon kan de antwoorden geven op de vragen", "OK");
                                    });
                                };
                            }
                        }
                    }

                    if (HuidigeAss.Question.MultipleChoiceAnswers.Count > 3)
                    {
                        if (HuidigeAss.Question.MultipleChoiceAnswers[3] != "x")
                        {
                            var D = new Button { Text = HuidigeAss.Question.MultipleChoiceAnswers[3], Margin = new Thickness(0, 0, 20, 0), HorizontalOptions = LayoutOptions.Center, FontSize = 16 };
                            if (HuidigeAss.Question.MultipleChoiceAnswers[3].Length < 17)
                            {
                                SecondRow.Children.Add(D);
                            }
                            else
                            {
                                FirstRow.Orientation = StackOrientation.Vertical;
                                FirstRow.Children.Add(D);
                            }
                            if (Assignment.Question.IsOpenQuestion == true)
                            {
                                D.Clicked += (sender, e) =>
                                {
                                    Answer = HuidigeAss.Question.MultipleChoiceAnswers[3];
                                    Antwoord();
                                };
                            }
                            else if (Assignment.Question.IsOpenQuestion == false)
                            {
                                D.TextColor = Color.Gray;

                                D.Clicked += (sender, e) =>
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await DisplayAlert("Niet mogelijk", "Alleen de hoofdtelefoon kan de antwoorden geven op de vragen", "OK");
                                    });
                                };
                            }
                        }
                    }
                }

            }
            else if (HuidigeAssL.Count == 0)
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
                    HuidigeAss.Result.Score = HuidigeAss.Settings.MaxScore;
                    HuidigeAss.Question.IsOpenQuestion = true;
                    App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(HuidigeAss);

                    var Antz = (string.Format("Dit was het goede antwoord! Jullie hebben {0} punten verdiend", HuidigeAss.Settings.MaxScore));
                    await DisplayAlert("Goed!", Antz, "OK");
                    await Navigation.PopAsync();

                }
                else if (Answer != HuidigeAss.Question.Answer)
                {
                    HuidigeAss.Question.IsOpenQuestion = true;
                    App.Database.ChoirWeekend2017.Game2.UpdateOrInsert(HuidigeAss);

                    var AntZ = (string.Format("Het goede antwoord was {0}, jullie hebben helaas geen punten verdiend", Convert.ToString(HuidigeAss.Question.Answer)));
                    await DisplayAlert("Helaas", AntZ, "OK");
                    await Navigation.PopAsync();

                }
            });
        }
    }
}
