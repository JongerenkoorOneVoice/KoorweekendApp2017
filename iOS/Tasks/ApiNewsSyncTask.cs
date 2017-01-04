using System.Threading;
using UIKit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using KoorweekendApp2017.Messages;


namespace KoorweekendApp2017.iOS.Tasks{
                            
	public class ApiNewsSyncTask
	{
		nint _taskId;
		CancellationTokenSource _cts;

		public ApiNewsSyncTask()
		{

		}

		public async Task Start()
		{
			_cts = new CancellationTokenSource();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("ApiNewsSync", OnExpiration);

			try
			{
				KoorweekendApp2017.Tasks.DataSync.UpdateNewsInDbFromApi();

			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (_cts.IsCancellationRequested)
				{
					var message = new StopApiNewsSyncMessage();
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