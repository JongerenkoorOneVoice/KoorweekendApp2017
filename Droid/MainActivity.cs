using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using KoorweekendApp2017.Messages;
using Xamarin.Forms;
using KoorweekendApp2017.Droid.Tasks;

namespace KoorweekendApp2017.Droid
{
	[Activity(Label = "KoorweekendApp2017.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
            //ZXing.Net.Mobile.Forms.Android.Platform.Init();

			MessagingCenter.Subscribe<StartApiContactSyncMessage>(this, "StartApiContactSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiContactSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiContactSyncMessage>(this, "StartStopContactSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiContactSyncTask));
				StopService(intent);
			});

			MessagingCenter.Subscribe<StartApiSongSyncMessage>(this, "StartApiSongSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiSongSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiSongSyncMessage>(this, "StopApiSongSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiSongSyncTask));
				StopService(intent);
			});

	
			MessagingCenter.Subscribe<StartApiEventSyncMessage>(this, "StartApiEventSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiEventSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiEventSyncMessage>(this, "StopApiEventSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiEventSyncTask));
				StopService(intent);
			});

			App oneVoiceApp = new App();
            LoadApplication(oneVoiceApp);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
