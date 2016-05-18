using CanIHazCodes.ServiceIntegration.Instagram.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CanIHazCodes.ServiceIntegration.Instagram.Models
{
    internal class User : IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Full_Name { get; set; }
        public Uri Profile_Picture { get; set; }
        public string Bio { get; set; }
        public Uri Website { get; set; }

        public IAsyncOperation<IEnumerable<IMedia>> GetRecentPostsAsync()
        {
            return Task.Run(async () =>
            {
                var req = InstagramClient.sRequestFactory.CreateRequest("users/self/media/recent");
                var media = await req.PerformRequestAsync();

                var mediaList = media.Cast<Media>().ToList();
                var imediaList = mediaList.Cast<IMedia>();

                return imediaList;

            }).AsAsyncOperation();
        }

        public IAsyncOperation<IEnumerable<IMedia>> GetLikesAsync()
        {
            return Task.Run(async () =>
            {
                var req = InstagramClient.sRequestFactory.CreateRequest("users/self/media/liked");
                var media = await req.PerformRequestAsync();

                var mediaList = media.Cast<Media>().ToList();
                var imediaList = mediaList.Cast<IMedia>();

                return imediaList;

            }).AsAsyncOperation();
        }

        public static User Deserialize(JToken serilaizedObject)
        {
            var user = new User();

            return user;
        }
    }
}
