using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1_DescriptionPage : ContentPage
	{
		public Koorweekend2017Spel1_DescriptionPage()
		{
			InitializeComponent();

			var device = Resolver.Resolve<IDevice>();
			desciptionHtmlView.Source = GetHTML();
			backButton.IsVisible = false;
			//backButton.Clicked += BackButtonClicked;
			desciptionHtmlView.HeightRequest = device.Display.Height - backButton.Height;
		}

		void BackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private HtmlWebViewSource GetHTML()
		{
			HtmlWebViewSource document = new HtmlWebViewSource();
			document.Html = String.Format("<html><head><style>{0}</style></head><body>{1}</body></html>",  _getCss(), _getHtml());



			return document;
		}

		private string _getHtml()
		{
			return @"
				<h1>Speluitleg</h1>
				<h3>Timer</h3>
				<p>Je het twee uur om dit spel te spelen en je moet een beetje doorlezen, want de timer is al gestart. Als de timer op nul staat is het spel afgelopen en kan je geen punten meer halen.</p>
				<h3>Doel van het spel</h3>
				<p>Het doel van het spel is om in twee uur zoveel mogelijk punten te behalen. Punten krijg je door lokaties te vinden. Sommige lokaties zijn moeilijker te vinden dan anderen. Voor moeilijkere lokaties krijg je meer punten. De moeilijkheidsgraad word weergeven door de kleur van de lokatie op het scherm.</p>
				<h3>Legenda</h3>
				<table>
				<thead>
				<th>Kleur</th>
				<th>Omschrijving</th>
				<th>Waarde</th>
				</thead>
				<tbody>
				<tr>
				<td><div class=""circle green""></div></td>
				<td>Gemiddeld</td>
				<td>1 punt</td>
				</tr>
				<tr>
				<td><div class=""circle yellow""></div></td>
				<td>Lastiger</td>
				<td>5 punten</td>
				</tr>
				<tr>
				<td><div class=""circle red""></div></td>
				<td>Moeilijk</td>
				<td>10 punten</td>
				</tr>
				<tr>
				<td><div class=""circle blue""></div></td>
				<td>Start/Eindpunt</td>
				<td>0 punten</td>
				</tr>
				<tr>
				<td><div class=""circle white""></div></td>
				<td>Gevonden</td>
				<td></td>
				</tr>
				</tbody>
				</table>
				<p></p>
			";

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
