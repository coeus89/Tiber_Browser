using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tiber_Browser.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Web.WebView2;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tiber_Browser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            try
            {
                DataAccess dataAccess = new DataAccess();
                dataAccess.CreateSettingsFile();
            }
            catch (Exception ex)
            {
                // have to figure out how to do alerts in UWP.
            }
            
        }

        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(web_Browser.CanGoBack)
                    web_Browser.GoBack();
            }
            catch (Exception ex)
            {
                // Do nothing if you can't go back
                ContentDialog cantGoBack = new ContentDialog()
                {
                    Title = "I Can't Go Back.",
                    Content = "You've gone as far back in browser time as you can go.",
                    CloseButtonText = "OK"
                };

                await cantGoBack.ShowAsync();
            }
        }

        private async void btnForward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(web_Browser.CanGoForward)
                    web_Browser.GoForward();
            }
            catch (Exception ex)
            {
                // Do nothing if you can't go forward
                ContentDialog cantGoForward = new ContentDialog()
                {
                    Title = "I'm not a mind reader.",
                    Content = "You are as far forward as you can go. You'll have to take the long way round.",
                    CloseButtonText = "OK"
                };

                await cantGoForward.ShowAsync();
            }
        }

        private void SearchBar_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            try
            {
                if (e.Key == Windows.System.VirtualKey.Enter)
                    Search();
            }
            catch(Exception ex)
            {
                // Todo
            }
            
        }

        // Need to move to business layer
        private void Search()
        {
            web_Browser.Source = new Uri("https://www.bing.com/search?q=" + SearchBar.Text);
            //DataAccess da = new DataAccess();
        }
    }
}
