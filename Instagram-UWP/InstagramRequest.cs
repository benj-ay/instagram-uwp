using CanIHazCodes.ServiceIntegration.Instagram.Interfaces;
using CanIHazCodes.ServiceIntegration.Instagram.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CanIHazCodes.ServiceIntegration.Instagram
{
    internal class InstagramRequest
    {
        private const string cUrlFormat = "{0}/{1}/{2}?access_token={3}";
        private const string cBaseUrl = "https://api.instagram.com";
        private const string cApiVersion = "v1";

        public string authToken;
        private string urlMethod;

        public InstagramRequest(string urlMethod, string authToken)
        {
            this.authToken = authToken;
            this.urlMethod = urlMethod;
        }

        public async Task<List<InstagramObject>> PerformRequestAsync()
        {
            var instaObjects = new List<InstagramObject>();

            var requestString = string.Format(cUrlFormat, cBaseUrl, cApiVersion, this.urlMethod, authToken);
            var jsonReuqest = WebRequest.Create(requestString);
            var response = await jsonReuqest.GetResponseAsync();
            string responseString = "";
          
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                responseString = reader.ReadToEnd();
            }

            JObject json = JObject.Parse(responseString);
            IList<JToken> tokens = json["data"].Children().ToList();
            foreach (var mediaToken in tokens)
            {
                instaObjects.Add(Media.Deserialize(mediaToken));
            }

            return instaObjects;
        }
    }
}
