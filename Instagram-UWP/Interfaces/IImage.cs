using System;

namespace CanIHazCodes.ServiceIntegration.Instagram.Interfaces
{
    public enum Resolution
    {
        Low,
        Standard,
        Thumbnail
    }
    public interface IImage
    {
        Uri Location { get; }
        Resolution Resolution { get; }
    }
}
