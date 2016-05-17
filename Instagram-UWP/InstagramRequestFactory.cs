using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanIHazCodes.ServiceIntegration.Instagram
{
    internal class InstagramRequestFactory
    {
        private string authToken;

        internal InstagramRequestFactory(string authToken)
        {
            this.authToken = authToken;
        }

        internal InstagramRequest CreateRequest(string urlMethod)
        {
            return new InstagramRequest(urlMethod, this.authToken);
        }
    }
}
