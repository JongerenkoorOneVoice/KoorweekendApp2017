using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using KoorweekendApp2017.Messages;
using KoorweekendApp2017.iOS.Tasks;
using UIKit;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace KoorweekendApp2017.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			try
			{
				ApiContactSyncTask apiContactSync = new ApiContactSyncTask();
				MessagingCenter.Subscribe<StartApiContactSyncMessage>(this, "StartApiContactSyncMessage", async message =>
				{
					apiContactSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiContactSyncMessage>(this, "StartStopContactSyncMessage", message =>
				{
					apiContactSync.Stop();
				});

				ApiSongSyncTask apiSongSync = new ApiSongSyncTask();
				MessagingCenter.Subscribe<StartApiSongSyncMessage>(this, "StartApiSongSyncMessage", async message =>
				{
					apiSongSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiSongSyncMessage>(this, "StopApiSongSyncMessage", message =>
				{
					apiSongSync.Stop();
				});

				ApiEventSyncTask apiEventSync = new ApiEventSyncTask();
				MessagingCenter.Subscribe<StartApiEventSyncMessage>(this, "StartApiEventSyncMessage", async message =>
				{
					apiEventSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiEventSyncMessage>(this, "StopApiEventSyncMessage", message =>
				{
					apiEventSync.Stop();
				});

				ApiSongOccasionSyncTask apiSongOccasionSync = new ApiSongOccasionSyncTask();
				MessagingCenter.Subscribe<StartApiSongOccasionSyncMessage>(this, "StartApiSongOccasionSyncMessage", async message =>
				{
					apiSongOccasionSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiSongOccasionSyncMessage>(this, "StopApiSongOccasionSyncMessage", message =>
				{
					apiSongOccasionSync.Stop();
				});

			}
			catch (Exception ex)
			{
				var y = ex;
			}

            
			#region Resolver Init
			SimpleContainer container = new SimpleContainer();
			container.Register<IDevice>(t => AppleDevice.CurrentDevice);
			container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
			container.Register<INetwork>(t => t.Resolve<IDevice>().Network);

			Resolver.SetResolver(container.GetResolver());
			#endregion

			App oneVoiceApp = new App();
			LoadApplication(oneVoiceApp);

			return base.FinishedLaunching(app, options);
		}


	}
}
