using System;
using System.Text;
using System.Text.RegularExpressions;

namespace KoorweekendApp2017.Extensions
{
	public static class StringExtensions
	{
		public static bool IsValidEmailAddres(this String strIn)
		{
			// Return true if strIn is in valid e-mail format.
			try
			{
				return Regex.IsMatch(strIn,
					  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		public static string ToBase64(this String strIn)
		{
			var bytes = Encoding.UTF8.GetBytes(strIn);
			var base64 = Convert.ToBase64String(bytes);
			return base64;
		}
	}
}
