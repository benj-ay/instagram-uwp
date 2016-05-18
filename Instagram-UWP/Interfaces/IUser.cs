using System.Collections.Generic;
using Windows.Foundation;

namespace CanIHazCodes.ServiceIntegration.Instagram.Interfaces
{
    public interface IUser
    {
        IAsyncOperation<IEnumerable<IMedia>> GetRecentPostsAsync();
        IAsyncOperation<IEnumerable<IMedia>> GetLikesAsync();
    }
}
