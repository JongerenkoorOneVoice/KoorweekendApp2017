﻿using System.Threading;
using UIKit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using KoorweekendApp2017.Messages;


namespace KoorweekendApp2017.iOS.Tasks{
                            
	public class ApiGlobalSettingsSyncTask
	{
		nint _taskId;
		CancellationTokenSource _cts;

		public ApiGlobalSettingsSyncTask()
		{

		}

		public async Task Start()
		{
			_cts = new CancellationTokenSource();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("ApiGlobalSettingsSync", OnExpiration);

			try
			{
				KoorweekendApp2017.Tasks.DataSync.UpdateGlobalSettingsInDbFromApi();

			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (_cts.IsCancellationRequested)
				{
					var message = new StopApiGlobalSettingsSync();
					Device.BeginInvokeOnMainThread(
						() => MessagingCenter.Send(message, "CancelledMessage")
					);
				}
			}

			UIApplication.SharedApplication.EndBackgroundTask(_taskId);
		}

		public void Stop()
		{
			_cts.Cancel();
		}

		void OnExpiration()
		{
			_cts.Cancel();
		}
	}
}