using System;
using KoorweekendApp2017.Extensions;

namespace KoorweekendApp2017
{
	public class BasicAuthentication
	{
		public String UserName { get; set;}

		public String PassWord { get; set;}

		public String HeaderValue
		{
			get
			{
				
				String base64String = String.Format("{0}:{1}", UserName, PassWord).ToBase64();;
				String fullString = String.Format("basic {0}", base64String);
				return fullString;
			}
		}

	}
}
