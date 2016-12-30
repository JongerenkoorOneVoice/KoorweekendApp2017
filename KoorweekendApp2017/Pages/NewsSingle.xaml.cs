using System;
using System.Collections.Generic;
using System.Text;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class NewsSingle : ContentPage
	{
		public NewsSingle()
		{
			InitializeComponent();


		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var currentNews = BindingContext as News;
			newsView.Source = GetHTML(currentNews);
		}


		private HtmlWebViewSource GetHTML(News currentNews)
		{
			StringBuilder css = new StringBuilder();
			css.Append("* { font-size: 1rem; font-family: helvetica, arial ; }  h1 { font-size: 1.5rem; color: red;} img { display: block; width: 100%; }");
			StringBuilder html = new StringBuilder();
			html.AppendFormat("<h1>{0}</h1>", currentNews.Title);
			html.Append("<img src=\"https://s-media-cache-ak0.pinimg.com/564x/c8/d0/c2/c8d0c26ffe78cb188322a4ba77eee033.jpg\"/><span>In tegenstelling tot <a href=\"http://www.nu.nl\">test</a> wat algemeen aangenomen wordt is Lorem Ipsum niet zomaar willekeurige tekst. <b>het heeft zijn wortels</b> in een stuk klassieke latijnse literatuur uit 45 v.Chr. en is dus meer dan 2000 jaar oud. Richard McClintock, een professor latijn aan de Hampden-Sydney College in Virginia, heeft één van de meer obscure latijnse woorden, consectetur, uit een Lorem Ipsum passage opgezocht, en heeft tijdens het zoeken naar het woord in de klassieke literatuur de onverdachte bron ontdekt. <blockquote>Lorem Ipsum komt uit de secties 1.10.32 en 1.10.33 van \"de Finibus Bonorum et Malorum\"</blockquote> (De uitersten van goed en kwaad) door Cicero, geschreven in 45 v.Chr. Dit boek is een verhandeling over de theorie der ethiek, erg populair tijdens de renaissance. De eerste regel van Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", komt uit een zin in sectie 1.10.32.</span><span>In tegenstelling tot wat algemeen aangenomen wordt is Lorem Ipsum niet zomaar willekeurige tekst. het heeft zijn wortels in een stuk klassieke latijnse literatuur uit 45 v.Chr. en is dus meer dan 2000 jaar oud. Richard McClintock, een professor latijn aan de Hampden-Sydney College in Virginia, heeft één van de meer obscure latijnse woorden, consectetur, uit een Lorem Ipsum passage opgezocht, en heeft tijdens het zoeken naar het woord in de klassieke literatuur de onverdachte bron ontdekt. Lorem Ipsum komt uit de secties 1.10.32 en 1.10.33 van \"de Finibus Bonorum et Malorum\" (De uitersten van goed en kwaad) door Cicero, geschreven in 45 v.Chr. Dit boek is een verhandeling over de theorie der ethiek, erg populair tijdens de renaissance. De eerste regel van Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", komt uit een zin in sectie 1.10.32.</span><span>In tegenstelling tot wat algemeen aangenomen wordt is Lorem Ipsum niet zomaar willekeurige tekst. het heeft zijn wortels in een stuk klassieke latijnse literatuur uit 45 v.Chr. en is dus meer dan 2000 jaar oud. Richard McClintock, een professor latijn aan de Hampden-Sydney College in Virginia, heeft één van de meer obscure latijnse woorden, consectetur, uit een Lorem Ipsum passage opgezocht, en heeft tijdens het zoeken naar het woord in de klassieke literatuur de onverdachte bron ontdekt. Lorem Ipsum komt uit de secties 1.10.32 en 1.10.33 van \"de Finibus Bonorum et Malorum\" (De uitersten van goed en kwaad) door Cicero, geschreven in 45 v.Chr. Dit boek is een verhandeling over de theorie der ethiek, erg populair tijdens de renaissance. De eerste regel van Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", komt uit een zin in sectie 1.10.32.</span>");
			HtmlWebViewSource document = new HtmlWebViewSource()
			{
				Html = String.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", css.ToString(), html.ToString())
			};

			return document;
		}
	}
}
