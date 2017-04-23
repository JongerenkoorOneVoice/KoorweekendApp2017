using System;
using KoorweekendApp2017.Enums;
using Xamarin.Forms;

namespace KoorweekendApp2017.BusinessObjects
{
	public class Timer
	{

		private bool _timerIsRunning {get; set;}

		private TimerType _timerType { get; set; }

		public Action _timerAction { get; set; }

		private TimeSpan _period { get; set; }

		public Timer(TimeSpan period, Action timerAction)
		{
			_constructor(period, timerAction, TimerType.TimeOut);
		}

		public Timer(TimeSpan period, Action timerAction, TimerType timerType)
		{
			_constructor(period, timerAction, timerType);
		}

		private void _constructor(TimeSpan period, Action timerAction, TimerType timerType)
		{
			_timerAction = timerAction;
			_period = period;
			_timerType = timerType;
		}

		public void Start()
		{
			_timerIsRunning = true;
			Device.StartTimer( _period, _timerLogic );
		}

		public void Stop()
		{
			_timerIsRunning = false;
		}

		private bool _timerLogic()
		{
			if (!_timerIsRunning) return false;

			_timerAction.Invoke();

			return _timerType == TimerType.Interval;
		}

	}
}
