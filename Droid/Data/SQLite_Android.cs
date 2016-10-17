using System;
using System.IO;
using KoorweekendApp2017.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace KoorweekendApp2017.Droid
{
	public class SQLite_Android
	{
		public SQLite_Android()
		{
		}

		public SQLiteConnection GetConnection()
		{
			var sqliteFilename = "JongerenkoorOneVoice.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}
