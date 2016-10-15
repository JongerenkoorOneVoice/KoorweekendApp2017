using System;
namespace KoorweekendApp2017.Models
{
	public class Contact
	{

		public int Id { get; set; }
		public string LastModified { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string AreaCode { get; set; }
		public string City { get; set; }
		public string BirthDate { get; set; }
		public string StartDate { get; set; }
		public string Phone1 { get; set; }
		public string Mobile1 { get; set; }
		public string Email1 { get; set; }
		public string FullName
		{
			get
			{
				return String.Format("{0} {1}", FirstName, LastName);
			}
		}


	}
}
