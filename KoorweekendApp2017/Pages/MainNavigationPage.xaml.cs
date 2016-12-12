using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class MainNavigationPage : NavigationPage
	{

		public MainNavigationPage() : base()
		{
            InitializeComponent();
            PushAsync(new HomePage());

		}

		public MainNavigationPage(Page root) : base(root)
		{
			InitializeComponent();
		}



	}
}
