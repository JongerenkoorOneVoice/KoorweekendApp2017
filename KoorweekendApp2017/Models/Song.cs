using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
    public class Song : DatabaseItemBase
    {

        public DateTime LastModified { get; set; }
        public String Title { get; set; }
        public String Lyrics { get; set; }
		public String YoutubeId { get; set; }

		[Ignore]
		private List<int> _songOccasions { get; set; }

		[Ignore]
		public List<int> SongOccasions
		{
			get
			{
				return _songOccasions;
			}
			set
			{
				_songOccasions = value;

			}
		}

		public String SongOccasionIds
		{
			get
			{
				return JsonConvert.SerializeObject(_songOccasions);
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
				{

					_songOccasions = JsonConvert.DeserializeObject<List<int>>(value);
				}
				else {
					throw new Exception("The json value for SongIds is invalid");
				}
			}

		}
    }
}
