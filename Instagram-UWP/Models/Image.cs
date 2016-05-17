using CanIHazCodes.ServiceIntegration.Instagram.Interfaces;
using Newtonsoft.Json.Linq;
using System;

namespace CanIHazCodes.ServiceIntegration.Instagram.Models
{
    internal class Image : IImage
    {
        public Uri Location { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Resolution Resolution { get; set; }

        public static Image Deserialize(JToken serializedObject)
        {
            var obj = serializedObject as JProperty;
            Resolution resolution = Resolution.Standard;
            Uri location = null;

            switch (obj.Name)
            {
                case "low_resolution":
                    resolution = Resolution.Low;
                    break;
                case "standard_resolution":
                    resolution = Resolution.Standard;
                    break;
                case "thumbnail":
                    resolution = Resolution.Thumbnail;
                    break;
            }

            var childTokens = obj.Children().Children();
            foreach (JProperty property in childTokens)
            {
                switch(property.Name)
                {
                    case "url":
                        location = new Uri(property.Value.Value<string>());
                        break;
                }
            }

            var imageObject = new Image()
            {
                Resolution = resolution,
                Location = location
            };

            return imageObject;
        }
    }
}
