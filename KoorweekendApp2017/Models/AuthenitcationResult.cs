using System;
using KoorweekendApp2017.Enums;

namespace KoorweekendApp2017.Models
{
	public class AuthenticationResult
	{
		public AuthorizationCode Code { get; set;}
		public string Description { get; set;}
	}
}
