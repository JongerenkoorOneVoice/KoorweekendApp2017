using System;
namespace KoorweekendApp2017.Enums
{
	public enum AuthorizationCode
	{
		None,
		RequestIncomplete = 0,
		InvalidEmailaddress = 1,
		EmailAddressNotFound = 3,
		NotAuthorized = 4,
		Authorized = 100
	}
}
