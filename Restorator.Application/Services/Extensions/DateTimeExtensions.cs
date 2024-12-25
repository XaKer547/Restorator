namespace Restorator.Application.Services.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool Between(this DateTime input, DateTime left, DateTime right)
        {
            return input > left && input < right;
        }
    }
}
