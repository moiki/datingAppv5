namespace API.Extensions;

public static class DateTimeCustomExtensions
{
    public static int GetAge(this DateTime dateOfAge)
    {
        var result = dateOfAge.Year - DateTime.Now.Year;
        if (dateOfAge.Date > DateTime.Now.AddYears(-result)) result--;
        return result;
    }
}