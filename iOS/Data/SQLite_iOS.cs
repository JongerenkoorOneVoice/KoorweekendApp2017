using System;
using System.IO;
using System.Runtime.CompilerServices;
using KoorweekendApp2017.Interfaces;
using KoorweekendApp2017.iOS;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace KoorweekendApp2017.iOS
{
	
	public class SQLite_iOS: ISQLite
	{
		public SQLite_iOS()
		{
		}

		public SQLiteConnection GetConnection()
		{
			var sqliteFilename = "JongerenkoorOneVoice.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var conn = new SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}
