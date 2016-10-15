using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace KoorweekendApp2017.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			App oneVoiceApp = new App();        
			LoadApplication(oneVoiceApp);

			return base.FinishedLaunching(app, options);
		}
	}
}
