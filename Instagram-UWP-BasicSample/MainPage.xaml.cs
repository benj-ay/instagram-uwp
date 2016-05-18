using CanIHazCodes.ServiceIntegration.Instagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Instagram_UWP_BasicSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private InstagramClient instaClient;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var config = new InstagramClientConfiguration
            {
                ClientId = this.ClientIdTextBox.Text,
                RedirectUrl = this.RedirectUriTextBox.Text
            };

            this.instaClient = new InstagramClient();
            var initResult = await this.instaClient.Initialize(config);
            if (ClientStatus.Unauthorized == initResult)
            {
                await this.instaClient.Authorize();
            }

            var pic = await this.instaClient.User.GetRecentPostsAsync();
            this.InstaPictureControl.Source = new BitmapImage(pic.First().Images.First().Location);
        }
    }
}
