using System.ComponentModel;

namespace Restorator.DataAccess.Data.Entities.Enums
{
    public enum Roles
    {
        [Description("Пользователь")]
        User = 1,

        [Description("Менеджер ресторана")]
        Manager,

        [Description("Администратор")]
        Admin
    }
}