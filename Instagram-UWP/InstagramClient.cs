using CanIHazCodes.ServiceIntegration.Instagram.Interfaces;
using CanIHazCodes.ServiceIntegration.Instagram.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using Windows.UI.Core;

namespace CanIHazCodes.ServiceIntegration.Instagram
{
    public enum ClientStatus
    {
        NotConnected,
        Unauthorized,
        Connected,
        Error_AuthFailed,
        Error
    }

    public sealed class InstagramClient
    {
        private const string cAuthUrl = "https://api.instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=token";


        private InstagramClientConfiguration clientConfig;
        private string authToken;

        private User user;

        internal static InstagramRequestFactory sRequestFactory;

        private ClientStatus status;

        public ClientStatus Status
        {
            get
            {
                return this.status;
            }
        }

        public InstagramClient()
        {

            this.status = ClientStatus.NotConnected;
            this.user = new Models.User();
        }

        public IAsyncOperation<ClientStatus> Initialize(InstagramClientConfiguration clientConfig)
        {
            this.clientConfig = clientConfig;

            return Task.Run(() => 
            {
                return ClientStatus.Unauthorized;
            }).AsAsyncOperation();
        }

        public IAsyncOperation<ClientStatus> Authorize()
        {
            var completionSource = new TaskCompletionSource<ClientStatus>();

            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async
                () =>
                {
                    completionSource.SetResult(await performAuthorzation());
                });

            return completionSource.Task.AsAsyncOperation();
        }

        public IUser User
        {
            get
            {
                return this.user;
            }
        }

        private async Task<ClientStatus> performAuthorzation()
        {
            string instAuthUrl = "https://api.instagram.com/oauth/authorize/?client_id=" + this.clientConfig.ClientId + "&redirect_uri=" + Uri.EscapeDataString(this.clientConfig.RedirectUrl) + "&response_type=token";
            Uri StartUri = new Uri(instAuthUrl);
            Uri EndUri = new Uri(this.clientConfig.RedirectUrl);
            var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);

            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                await this.performPostAuthorizationSetup(result.ResponseData.Remove(0, EndUri.ToString().Count()));
                return ClientStatus.Connected;
            }
            else
            {
                return ClientStatus.Error_AuthFailed;
            }
        }

        private async Task performPostAuthorizationSetup(string authToken)
        {
            // Setup request factory
            sRequestFactory = new InstagramRequestFactory(authToken);

            // Setup user
            var req = InstagramClient.sRequestFactory.CreateRequest("users/self");
            //await req.PerformRequestAsync();
            this.user = new User();
        }
    }
}
