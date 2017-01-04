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
			css.Append("* { font-size: 1rem; font-family: helvetica, arial ; }  h1 { font-size: 1.5rem; color: red;} img { display: block; width: 100%; } a { color: red; }");

			StringBuilder html = new StringBuilder();
			html.AppendFormat("<h1>{0}</h1>", currentNews.Title);
			html.Append(currentNews.HTML);

			HtmlWebViewSource document = new HtmlWebViewSource()
			{
				Html = String.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", css.ToString(), html.ToString())
			};

			return document;
		}
	}
}
