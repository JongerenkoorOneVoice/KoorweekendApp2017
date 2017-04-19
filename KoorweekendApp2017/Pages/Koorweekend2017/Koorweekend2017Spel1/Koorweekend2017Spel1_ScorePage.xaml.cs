using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1_ScorePage : ContentPage
	{
		public Koorweekend2017Spel1_ScorePage()
		{			
			InitializeComponent();
			var device = Resolver.Resolve<IDevice>();
			scoreHtmlView.Source = GetHTML();
			backButton.IsVisible = false;
			//backButton.Clicked += BackButtonClicked;
			scoreHtmlView.HeightRequest = device.Display.Height - backButton.Height;
		}

		void BackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private HtmlWebViewSource GetHTML()
		{
			var assignmentList = App.Database.ChoirWeekend2017.Game1.GetAll();

			int redsFound = assignmentList.FindAll(x=>x.Result.Score != 0 && x.Settings.MaxScore == 10).Count;
			int yellowsFound = assignmentList.FindAll(x => x.Result.Score != 0 && x.Settings.MaxScore == 5).Count;
			int greensFound = assignmentList.FindAll(x => x.Result.Score != 0 && x.Settings.MaxScore == 1).Count;
			int penaltyPoints = 0;
			var setting = App.Database.Settings.GetByKey("2017game1PenaltyPoints");
			if (setting != null)
			{
				penaltyPoints = Convert.ToInt32(setting.Value);
			}


			HtmlWebViewSource document = new HtmlWebViewSource();
			document.Html = String.Format(
				"<html><head><style>{0}</style></head><body>{1}</body></html>",
				_getCss(),
				_getHtml(redsFound, yellowsFound, greensFound, penaltyPoints)
			);



			return document;
		}

		private string _getHtml(int redsFound, int yellowsFound, int greensFound, int penaltyPoints)
		{

			int redPoints = redsFound * 10;
			int yellowPoints = yellowsFound * 5;
			int greenPoints = greensFound;
			int totalScore = redPoints + yellowPoints + greenPoints - penaltyPoints;

			return String.Format(@"
				<p class=""score"">{0}</p>
				<p class=""scoretext"">PUNTEN</p>
				<h3>Puntenopbouw</h3>
				<table>
				<thead>
				<th>Kleur</th>
				<th>Aantal</th>
				<th>Waarde</th>
				</thead>
				<tbody>
				<tr>
				<td><div class=""circle green""></div></td>
				<td>{3}</td>
				<td>{6} punt(en)</td>
				</tr>
				<tr>
				<td><div class=""circle yellow""></div></td>
				<td>{2}</td>
				<td>{5} punten</td>
				</tr>
				<tr>
				<td><div class=""circle red""></div></td>
				<td>{1}</td>
				<td>{4} punten</td>
				</tr>
				<tr>
				<td>Strafpunten</td>
				<td>{7}</td>
				<td>-{7} punten</td>
				</tr>
				</tbody>
				</table>
				<p></p>
			", totalScore, redsFound, yellowsFound, greensFound, redPoints, yellowPoints, greenPoints, penaltyPoints);

		}

		private string _getCss()
		{
			return @"
				body{
					padding: 5px;
					background-color: black;
					color: white;
				}

				.circle{
					display: block;
					width: 1rem;
					height: 1rem;
					display: block;
					border-radius: 50%;
					margin-left: auto;
					margin-right: auto;
				}

				h3{
					margin-bottom: 0px;
				}

				p {
					margin-top: 0px;
				}				

				.score{
					font-size: 5rem;
					line-heigth: 5rem;
					text-align: center;
					margin-bottom: 0px;
					margin-top: 3rem;
				}

				.scoretext{
					font-size: 2.5rem;
					line-heigth: 2.5rem;
					text-align: center;

				}

				.circle.green{
					background-color: green;
				}

				.circle.yellow{
					background-color: yellow;
				}

				.circle.red{
					background-color: red;
				}

				.circle.blue{
					background-color: blue;
				}

				.circle.white{
					background-color: white;
				}

				table{
					width: 100%;
				}
	
				th{
					text-align: left;
				}			
			";
				
	}
	}
}
