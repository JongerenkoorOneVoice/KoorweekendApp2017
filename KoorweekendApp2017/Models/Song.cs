using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoorweekendApp2017.Models
{
    class Song : DatabaseItemBase
    {

        public String LastModified { get; set; }
        public String Title { get; set; }
        public String Lyrics { get; set; }


    }
}
