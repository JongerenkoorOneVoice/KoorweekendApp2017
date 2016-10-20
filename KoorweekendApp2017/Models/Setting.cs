using System;
using SQLite;
using Xamarin.Forms;

namespace KoorweekendApp2017.Models
{
	public class Setting : DatabaseItemBase
	{
		[Indexed]
		public String Key { get; set; }
		public String Value { get; set; }

	}
}

