using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class SettingsArchivePage : ContentPage
	{
		public Boolean ShowErrorLogInMenu { get; set;}

		public Boolean LoginOnNextStartup { get; set; }

		public SettingsArchivePage()
		{
			InitializeComponent();

			settingListView.ItemsSource = App.Database.Settings.GetAll();

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

		void OnShowErrorLogToggled(object sender, EventArgs e)
		{
			App.Database.Settings.Set("showErrorLogInMenu", ShowErrorLogInMenu);
		}

		void OnLoginNextStartupToggled(object sender, EventArgs e)
		{
			App.Database.Settings.Set("loginOnNextStart", LoginOnNextStartup);
		}
	}
}