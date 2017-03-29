using System;
using Plugin.DeviceInfo.Abstractions;

namespace KoorweekendApp2017
{
	public class CustomDevice
	{
		public String Id { get; set; }
		public String Model { get; set;}
		public String Platform { get; set;}
		public String Version { get; set;}


		public CustomDevice(IDeviceInfo deviceInfo)
		{
			Id = deviceInfo.Id;
			Model = deviceInfo.Model;
			Platform = deviceInfo.Platform.ToString();
			Version = deviceInfo.Version;

		}
	}
}
