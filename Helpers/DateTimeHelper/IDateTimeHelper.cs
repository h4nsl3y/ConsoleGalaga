namespace Helpers.DateTimeHelper
{
    public interface IDateTimeHelper
    {
        DateTime ConvertToUTC(DateTime dateTime);
        DateTime ConvertToLocalTime(DateTime dateTime);
    }
}
