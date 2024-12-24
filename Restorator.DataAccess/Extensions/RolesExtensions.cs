using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;

namespace Restorator.DataAccess.Extensions
{
    public static class RolesExtensions
    {
        public static Role FromEnum(this IQueryable<Role> query, Roles role)
        {
            return query.Single(r => r.Id == (int)role);
        }
    }
}
