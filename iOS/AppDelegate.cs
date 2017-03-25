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
				
				ZXing.Net.Mobile.Forms.iOS.Platform.Init();

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

				ApiNewsSyncTask apiNewsSync = new ApiNewsSyncTask();
				MessagingCenter.Subscribe<StartApiEventSyncMessage>(this, "StartApiNewsSyncMessage", async message =>
				{
					apiNewsSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiEventSyncMessage>(this, "StopApiEventSyncMessage", message =>
				{
					apiNewsSync.Stop();
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

				ApiPrayerRequestSyncTask apiPrayerRequestSync = new ApiPrayerRequestSyncTask();
				MessagingCenter.Subscribe<StartApiPrayerRequestSyncMessage>(this, "StartApiPrayerRequestSyncMessage", async message =>
				{
					apiPrayerRequestSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiPrayerRequestSyncMessage>(this, "StopApiPrayerRequestSyncMessage", message =>
				{
					apiPrayerRequestSync.Stop();
				});

				ApiChoirweekendGame1SyncTask apiChoirweekendGame1Sync = new ApiChoirweekendGame1SyncTask();
				MessagingCenter.Subscribe<StartApiChoirweekendGame1SyncMessage>(this, "StartApiChoirweekendGame1SyncMessage", async message =>
				{
					apiChoirweekendGame1Sync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiChoirweekendGame1SyncMessage>(this, "StopApiChoirweekendGame1SyncMessage", message =>
				{
					apiChoirweekendGame1Sync.Stop();
				});

				ApiChoirweekendGame2SyncTask apiChoirweekendGame2Sync = new ApiChoirweekendGame2SyncTask();
				MessagingCenter.Subscribe<StartApiChoirweekendGame2SyncMessage>(this, "StartApiChoirweekendGame2SyncMessage", async message =>
				{
					apiChoirweekendGame2Sync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiChoirweekendGame2SyncMessage>(this, "StopApiChoirweekendGame2SyncMessage", message =>
				{
					apiChoirweekendGame1Sync.Stop();
				});

				ApiChoirweekendPackinglistSyncTask apiChoirweekendPackinglistSync = new ApiChoirweekendPackinglistSyncTask();
				MessagingCenter.Subscribe<StartApiChoirweekendPackinglistSyncMessage>(this, "StartApiChoirweekendPackinglistSyncMessage", async message =>
				{
					apiChoirweekendPackinglistSync.Start(); // don't use await
				});

				MessagingCenter.Subscribe<StopApiChoirweekendPackinglistSyncMessage>(this, "StopApiChoirweekendPackinglistSyncMessage", message =>
				{
					apiChoirweekendPackinglistSync.Stop();
				});

			}
			catch (Exception ex)
			{
				var y = ex;
			}


            #region Resolver Init
            if (!Resolver.IsSet)
            {
                SimpleContainer container = new SimpleContainer();
                container.Register<IDevice>(t => AppleDevice.CurrentDevice);
                container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
                container.Register<INetwork>(t => t.Resolve<IDevice>().Network);

                Resolver.SetResolver(container.GetResolver());
            }
			#endregion

 			App oneVoiceApp = new App();
			LoadApplication(oneVoiceApp);

			return base.FinishedLaunching(app, options);
		}


	}
}
