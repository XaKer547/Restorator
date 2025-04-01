using Restorator.API.Models.MailTemplates.Abstract;

namespace Restorator.API.Models.MailTemplates
{
    public class ReservationCanceledMailTemplate : MailTemplateBase
    {
        public override string TemplateName => "ReservationCanceledMailTemplate"; //remove?
        public override string SubjectName => "Ваша бронь отменена";

        public string Username { get; }
        public DateTime ReservationDateTime { get; }
        public string RestaurantName { get; }

        public ReservationCanceledMailTemplate(string username,
                                               DateTime reservationDateTime,
                                               string restaurantName)
        {
            Username = username;
            Email = email;
            ReservationDateTime = reservationDateTime;
            RestaurantName = restaurantName;
        }

        public override Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                {"@Username", Username},
                {"@ReservationDateTime", ReservationDateTime.ToShortDateString()},
                {"@ReservationTime", ReservationDateTime.ToShortTimeString()},
                {"@RestaurantName", RestaurantName}
            };
        }
    }
}
