using System;
using SQLite;

namespace KoorweekendApp2017
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();

	}
}
