using Restorator.Mail.Models.Templates.Abstract;

namespace Restorator.Mail.Models.Templates
{
    public class ReservationCanceledMailTemplate(string email,
                                                 string username,
                                                 DateTime reservationDateTime,
                                                 string restaurantName) : MailTemplateBase(nameof(ReservationCanceledMailTemplate), "Ваша бронь отменена", email)
    {
        public string Username { get; } = username;
        public DateTime ReservationDateTime { get; } = reservationDateTime;
        public string RestaurantName { get; } = restaurantName;

        public override Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                {"@Username", Username},
                {"@ReservationDate", ReservationDateTime.ToShortDateString()},
                {"@ReservationTime", ReservationDateTime.ToShortTimeString()},
                {"@RestaurantName", RestaurantName}
            };
        }
    }
}
