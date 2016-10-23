using System.Threading;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using KoorweekendApp2017.Messages;
using Android.App;
using Android.OS;
using Android.Content;


namespace KoorweekendApp2017.Droid.Tasks{

	[Service]
	public class ApiSongSyncTask : Service
	{
		CancellationTokenSource _cts;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource();

			Task.Run(async () =>
			{
				try
				{
					//INVOKE THE SHARED CODE
					KoorweekendApp2017.Tasks.DataSync.UpdateSongsInDbFromApi();
				}
				catch (System.OperationCanceledException)
				{
				}
				finally
				{
					if (_cts.IsCancellationRequested)
					{
						var message = new StopApiSongSyncMessage();
						Device.BeginInvokeOnMainThread(
							() => MessagingCenter.Send(message, "CancelledMessage")
						);
					}
				}

			}, _cts.Token);

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			if (_cts != null)
			{
				_cts.Token.ThrowIfCancellationRequested();

				_cts.Cancel();
			}
			base.OnDestroy();
		}
	}

                            

}