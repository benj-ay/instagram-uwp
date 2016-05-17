using System.Collections.Generic;

namespace CanIHazCodes.ServiceIntegration.Instagram.Interfaces
{
    public interface IMedia
    {
        IEnumerable<IImage> Images { get; }
    }
}
