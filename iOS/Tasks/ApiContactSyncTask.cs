using System.Threading;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using KoorweekendApp2017.Messages;
using UIKit;

namespace KoorweekendApp2017.iOS.Tasks{
                            
	public class ApiContactSyncTask
	{
		nint _taskId;
		CancellationTokenSource _cts;

		public ApiContactSyncTask()
		{

		}

		public async Task Start()
		{
			_cts = new CancellationTokenSource();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("ApiContactSync", OnExpiration);

			try
			{

				KoorweekendApp2017.Tasks.DataSync.UpdateContactsInDbFromApi();

			}
			catch (OperationCanceledException ex)
			{
				var a = ex;
			}
			finally
			{
				if (_cts.IsCancellationRequested)
				{
					var message = new StopApiContactSyncMessage();
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