using FluentResults;
using Restorator.Domain.Models.Restaurant;

namespace Restorator.Domain.Services
{
    public interface ITemplateService
    {
        Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates();
        //Task<IReadOnlyCollection<Table> GetTableTemplates(); TableTemplateDTO
        //id
        //type
        //size
        //no rotate
        Task<Result> CreateRestaurantTemplate();
    }
}
