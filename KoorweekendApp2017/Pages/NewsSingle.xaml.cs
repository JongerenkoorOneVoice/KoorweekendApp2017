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
			newsView.Navigating += OnNavigating;
		}


		private HtmlWebViewSource GetHTML(News currentNews)
		{
			StringBuilder css = new StringBuilder();
			css.Append("* { font-size: 1rem; font-family: helvetica, arial ; }  h1 { font-size: 1.5rem; color: red;} img { display: block; width: 100%; } a { color: red; } .timestamp { font-size: .75rem; display: block; color: grey; margin-top: -10px; margin-bottom: 10px;}");

			StringBuilder html = new StringBuilder();
			html.AppendFormat("<h1>{0}</h1>", currentNews.Title);
			html.AppendFormat("<span class=\"timestamp\">{0}</span>", currentNews.LastModifiedDateAndTimeFormatted);
			html.Append(currentNews.HTML);

			HtmlWebViewSource document = new HtmlWebViewSource()
			{
				Html = String.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", css.ToString(), html.ToString())
			};

			return document;
		}

		void OnNavigating(object sender, WebNavigatingEventArgs e)
		{
			if (e.Url.StartsWith("mailto", StringComparison.OrdinalIgnoreCase))
			{
				var uri = new Uri(e.Url);
				Device.OpenUri(uri);
				e.Cancel = true;

			}
		}
	}
}
