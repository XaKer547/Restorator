using MediatR;

namespace Restorator.Desktop.Notifications
{
    public class RestaurantInfoNoification : INotification
    {
        public RestaurantInfoNoification(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
        public int RestaurantId { get; }
    }
}
