﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Spel2Page : ContentPage
	{
		public Koorweekend2017Spel2Page()
		{
			InitializeComponent();
            this.BindingContext = this;
            
            ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning();}));

        }
        void scanning()
        {
            var scanner = new ZXingScannerPage();

            Navigation.PushAsync(scanner);

            scanner.OnScanResult += (result) =>
            {
                // Stop scanning
                scanner.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.Text != "ik ben daniel")
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    }
                    else if (result.Text == "ik ben daniel")
                    {
                        await Navigation.PushAsync(new HomePage());
                    }
                });
            };
        }
    }
}
