using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Spel2Page : ContentPage
	{
        public Entry ent = new Entry() { VerticalOptions = LayoutOptions.Center };
        public PopupPage page = new PopupPage();

        public Koorweekend2017Spel2Page()
		{
			InitializeComponent();
            this.BindingContext = this;
            
            ToolbarItems.Add(new ToolbarItem("Add", "Scanner.png", () => { scanning();}));
            ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () => { typing(); }));
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

        void typing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var stack1 = new StackLayout();
                var stack2 = new StackLayout();
                var stack3 = new StackLayout();
                //var ent = new Entry(){ VerticalOptions = LayoutOptions.Center };
                var oke = new Button(){ Text = "Oke"};
                var Annul = new Button() { Text = "Annuleren" };
                //PopupPage page = new PopupPage();
                ent.Keyboard = Keyboard.Plain;
                stack1.Children.Add(new Label { Text = "Vul hier de barcode in:", HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center });
                stack1.Children.Add(ent);  
                stack1.HorizontalOptions = LayoutOptions.FillAndExpand;
                stack1.VerticalOptions = LayoutOptions.Start;
                stack1.BackgroundColor = Color.White;

                stack2.Children.Add(Annul);
                stack2.Children.Add(oke);
                stack2.Padding = new Thickness(10,0,10,0);
                stack2.Orientation = StackOrientation.Horizontal;
                stack2.HorizontalOptions = LayoutOptions.Center;
                stack2.VerticalOptions = LayoutOptions.Start;
                stack2.BackgroundColor = Color.White;

                stack3.Children.Add(stack1);
                stack3.Children.Add(stack2);
                stack3.HorizontalOptions = LayoutOptions.FillAndExpand;
                stack3.VerticalOptions = LayoutOptions.Start;
                stack3.BackgroundColor = Color.White;

                page.Content = stack3;
                page.Padding = new Thickness(20, 30, 20, 0);
                page.CloseWhenBackgroundIsClicked = false;
                oke.Clicked += OnOkeClicked;
                Annul.Clicked += OnAnullClicked;
                
                
                await Navigation.PushAsync(page); // Navigation.PushPopupAsync
           });
        }
        void OnAnullClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
                ent.Text = null;
            });
        }
        void OnOkeClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (ent.Text == "ik ben daniel")
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "U heeft de volgende locatie & vraag 1 vrijgespeeld", "OK");
                    ent.Text = null;
                }
                else if(ent.Text == null)
                {
                    //await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "Vul Een Barcode In", "OK");
                    ent.Text = null;
                }
                else if (ent.Text != "ik ben daniel")
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Barcode", "Geen Correcte Code", "OK");
                    ent.Text = null;
                }
            });
        }
    }
}
