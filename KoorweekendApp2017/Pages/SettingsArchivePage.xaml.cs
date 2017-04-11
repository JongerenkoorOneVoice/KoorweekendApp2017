using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class SettingsArchivePage : ContentPage
	{
		public Boolean ShowErrorLogInMenu { get; set;}

		public Boolean LoginOnNextStartup { get; set; }

		public Boolean OnlySyncOnWifi { get; set; }



		public SettingsArchivePage()
		{
			InitializeComponent();



			// Only sync when using WiFi
			OnlySyncOnWifi = false;
			Setting onlySyncOnWifi = App.Database.Settings.GetByKey("onlySyncOnWifi");
			if (onlySyncOnWifi != null)
			{
				OnlySyncOnWifi = JsonConvert.DeserializeObject<Boolean>(onlySyncOnWifi.Value);

			}
			OnlySyncOnWifiSwitch.BindingContext = this;
			OnlySyncOnWifiSwitch.OnChanged += OnOnlySyncOnWifiToggled;


			settingListView.IsVisible = false;
			if (UserHelper.CurrentUserIsDeveloper())
			{

				settingListView.ItemsSource = App.Database.Settings.GetAll();
				settingListView.IsVisible = true;

				var devOptions = new TableSection()
				{
					Title = "Developer options"
				};
				SettingsRoot.Add(devOptions);

				var errorLogSwitch = new SwitchCell()
				{
					Text = "Errorlog in menu (restart app)",
					On = ShowErrorLogInMenu
				};
				devOptions.Add(errorLogSwitch);

				var loginOnStartupSwitch = new SwitchCell()
				{
					Text = "Login on next start (restart app)",
					On = LoginOnNextStartup
				};
				devOptions.Add(loginOnStartupSwitch);

				// Show error log
				ShowErrorLogInMenu = false;
				Setting showErrorLogInMenu = App.Database.Settings.GetByKey("showErrorLogInMenu");
				if (showErrorLogInMenu != null)
				{
					ShowErrorLogInMenu = JsonConvert.DeserializeObject<Boolean>(showErrorLogInMenu.Value);
				}
				errorLogSwitch.BindingContext = this;
				errorLogSwitch.OnChanged += OnShowErrorLogToggled;

				// Login on next startup
				LoginOnNextStartup = false;
				Setting loginOnNextStartup = App.Database.Settings.GetByKey("showErrorLogInMenu");
				if (loginOnNextStartup != null)
				{
					LoginOnNextStartup = JsonConvert.DeserializeObject<Boolean>(loginOnNextStartup.Value);
				}
				loginOnStartupSwitch.BindingContext = this;
				loginOnStartupSwitch.OnChanged += OnLoginNextStartupToggled;
			}

		}

		void OnShowErrorLogToggled(object sender, EventArgs e)
		{
			App.Database.Settings.Set("showErrorLogInMenu", ShowErrorLogInMenu);
		}

		void OnLoginNextStartupToggled(object sender, EventArgs e)
		{
			App.Database.Settings.Set("loginOnNextStart", LoginOnNextStartup);
		}

		void OnOnlySyncOnWifiToggled(object sender, EventArgs e)
		{
			App.Database.Settings.Set("onlySyncOnWifi", OnlySyncOnWifi);
		}

	}
}