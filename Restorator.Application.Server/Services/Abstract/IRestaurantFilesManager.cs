using Restorator.Application.Server.Models;

namespace Restorator.Application.Server.Services.Abstract
{
    public interface IRestaurantFilesManager
    {
        Task<RestaurantFilesInfoDTO> CreateRestaurantFolder(string restaurantName, IEnumerable<byte[]> images, byte[] menu);
        Task<RestaurantFilesInfoDTO> UpdateRestaurantFolder(string restaurantName, IEnumerable<byte[]> images, byte[] menu);
        Task DeleteRestaurantFolder(string restaurantName);
    }
}