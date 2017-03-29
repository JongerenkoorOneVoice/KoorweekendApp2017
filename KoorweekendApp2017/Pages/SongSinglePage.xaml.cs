using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace KoorweekendApp2017.Pages
{
	public partial class SongSinglePage : ContentPage
	{

		public SongSinglePage()
		{
			InitializeComponent();




		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var currentSong = BindingContext as Song;

			if (!String.IsNullOrEmpty(currentSong.YoutubeId))
			{
				HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();
                var network = Resolver.Resolve<INetwork>();
                if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
                {
                    htmlWebViewSource.Html = BuildFinalHtml(BuildEmbedUrl(currentSong.YoutubeId));
                }
                else
                {
                    htmlWebViewSource.Html = BuildFinalHtml("<style>body{background-color: #e3e3e3; color: white;position:relative} h1{position: relative; text-align: center; top: 50%; font-size: 1rem;transform: translateY(-50%);font-family: arial;text-transform: uppercase;}</style><h1>Geen internet beschikbaar</h1>");
                }
				youtubeVideoView.Source = htmlWebViewSource;

				youtubeVideoView.HorizontalOptions = LayoutOptions.Fill;
				youtubeVideoView.VerticalOptions = LayoutOptions.Fill;
				youtubeVideoView.Margin = new Thickness(0, 0, 0, 15);
			}
			else
			{
				youtubeVideoView.IsVisible = false;
			}


		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
			youtubeVideoView.WidthRequest = width;
			youtubeVideoView.HeightRequest = width * 0.5625;

		}


		private string BuildEmbedUrl(string videoSource)
		{
			var iframeURL = string.Format("<div class=\"videoWrapper\"><iframe width=\"320\" height=\"600\" src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe><style>body{{padding:0; margin:0;}} .videoWrapper {{position: relative;padding-bottom: 56.25%; /* 16:9 */padding-top: 25px;height: 0;}}.videoWrapper iframe {{position: absolute;top: 0;left: 0;width: 100%;height: 100%;}}</style></div>", videoSource);
			return iframeURL;
		}

		private string BuildFinalHtml(string embedUrl)
		{
			string finalUrl = string.Format("<html><body>{0}</body></html>", embedUrl);
			return finalUrl;
		}

	}
}