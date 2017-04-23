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
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using Plugin.Permissions;
using Acr.UserDialogs;

namespace KoorweekendApp2017.Droid
{
    [Activity(Icon = "@drawable/onevoice_logo_app_less", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Android.Support.V7.App.AppCompatActivity
    {
        //static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            // StartActivity(typeof(MainActivity));
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {});

            startupWork.ContinueWith(t => {
          
                StartActivity(typeof(MainActivity));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }

    }

    [Activity(Label = "One Voice", Icon = "@drawable/onevoice_logo_app_less", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation =ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            //SetPage(App.GetMainPage());
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

			MessagingCenter.Subscribe<StartApiNewsSyncMessage>(this, "StartApiNewsSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiNewsSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiNewsSyncMessage>(this, "StopApiNewsSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiNewsSyncTask));
				StopService(intent);
			});

			MessagingCenter.Subscribe<StartApiSongOccasionSyncMessage>(this, "StartApiSongOccasionSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiSongOccasionSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiSongOccasionSyncMessage>(this, "StopApiSongOccasionSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiSongOccasionSyncTask));
				StopService(intent);
			});

			MessagingCenter.Subscribe<StartApiPrayerRequestSyncMessage>(this, "StartApiPrayerRequestSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiPrayerRequestSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiPrayerRequestSyncMessage>(this, "StopApiPrayerRequestSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiPrayerRequestSyncTask));
				StopService(intent);
			});

			MessagingCenter.Subscribe<StartApiGlobalSettingsSyncMessage>(this, "StartApiGlobalSettingsSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiGlobalSettingsSyncTask));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopApiGlobalSettingsSyncMessage>(this, "StopApiGlobalSettingsSyncMessage", message =>
			{
				var intent = new Intent(this, typeof(ApiGlobalSettingsSyncTask));
				StopService(intent);
			});

            #region Resolver Init
            if (!Resolver.IsSet)
            {
                SimpleContainer container = new SimpleContainer();
                container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
                container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
                container.Register<INetwork>(t => t.Resolve<IDevice>().Network);

                Resolver.SetResolver(container.GetResolver());
            }
			#endregion
            
			//Application.service
			UserDialogs.Init(this);

			App oneVoiceApp = new App("");
            LoadApplication(oneVoiceApp);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
