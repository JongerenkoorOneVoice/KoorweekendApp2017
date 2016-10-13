using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
    public partial class RepertoirePage : ContentPage
    {
        public RepertoirePage()
        {
            InitializeComponent();
            List<Song> songs = RestHelper.GetRestDataFromUrl<Song>("http://www.jongerenkooronevoice.nl/songs/all").Result;
        }
    }
}
