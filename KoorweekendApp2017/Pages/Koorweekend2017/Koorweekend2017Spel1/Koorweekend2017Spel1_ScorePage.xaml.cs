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
			backButton.Clicked += BackButtonClicked;
			scoreHtmlView.HeightRequest = device.Display.Height - backButton.Height;
		}

		void BackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}

		private HtmlWebViewSource GetHTML()
		{

			int redsFound = 3;
			int yellowsFound = 5;
			int greensFound = 10;
			int redPoints = 30;
			int yellowPoints = 25;
			int greenPoints = 10;
			int totalScore = redPoints + yellowPoints + greenPoints;


			HtmlWebViewSource document = new HtmlWebViewSource();
			document.Html = String.Format(
				"<html><head><style>{0}</style></head><body>{1}</body></html>",
				_getCss(),
				_getHtml(totalScore, redsFound, yellowsFound, greensFound, redPoints, yellowPoints, greenPoints)
			);



			return document;
		}

		private string _getHtml(int totalScore, int redsFound, int yellowsFound, int greensFound, int redPoints, int yellowPoints, int greenPoints)
		{
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
				</tbody>
				</table>
				<p></p>
			", totalScore, redsFound, yellowsFound, greensFound, redPoints, yellowPoints, greenPoints);

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
