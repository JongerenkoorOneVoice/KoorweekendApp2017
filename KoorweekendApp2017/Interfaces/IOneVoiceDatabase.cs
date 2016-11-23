using System;
using KoorweekendApp2017.BusinessObjects;

namespace KoorweekendApp2017.Interfaces
{
	public interface IOneVoiceDatabase
	{

		LocalDatabase.ContactTable Contacts { get; set; }

		LocalDatabase.SongTable Songs { get; set; }

		LocalDatabase.EventTable Events { get; set; }
	}
}
