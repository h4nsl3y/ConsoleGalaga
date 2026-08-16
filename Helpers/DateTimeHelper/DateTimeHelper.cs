
namespace Helpers.DateTimeHelper
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            dateTime = dateTime.ToLocalTime();
            return dateTime;
        }

        public DateTime ConvertToUTC(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime();
            return dateTime;
        }
    }
}
