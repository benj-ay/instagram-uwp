using CanIHazCodes.ServiceIntegration.Instagram.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CanIHazCodes.ServiceIntegration.Instagram.Models
{
    internal class Media : InstagramObject, IMedia
    {
        public string Type { get; set; }
        public List<IUser> UsersInPhoto { get; }
        public string Filter { get; set; }
        public List<IImage> Images { get; }

        IEnumerable<IImage> IMedia.Images
        {
            get
            {
                return this.Images;
            }
        }

        public Media()
        {
            this.UsersInPhoto = new List<IUser>();
            this.Images = new List<IImage>();
        }

        public static Media Deserialize(JToken serilaizedObject)
        {
            var media = new Media();

            foreach(var imageToken in serilaizedObject["images"].Children().ToList())
            {
                media.Images.Add(Image.Deserialize(imageToken));
            }

            return media;
        }
    }
}
