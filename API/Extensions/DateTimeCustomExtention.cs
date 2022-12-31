namespace API.Extensions;

public static class DateTimeCustomExtensions
{
    public static int GetAge(this DateTime dateOfAge)
    {
        var result = DateTime.Now.Year - dateOfAge.Year;
        if (dateOfAge.Date > DateTime.Now.AddYears(-result)) result--;
        return result;
    }
}