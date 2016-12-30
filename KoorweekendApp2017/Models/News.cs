using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
    public class News : DatabaseItemBase
    {

        public DateTime LastModified { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }



    }
}
