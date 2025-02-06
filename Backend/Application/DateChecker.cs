namespace Backend.Application;

public class DateChecker
{
    
    /// <summary>
    /// Checks if the Date lays inside the range which is given by the Open-Meteo Api.
    /// E.g. No Dates before 1.1.1940 are supported and Date must lay 2 days back in the past, as the Open-Meteo Archive API is called,
    /// which only has the past dates in the archive.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>ture in case of success, false in case of "invalid" date.</returns>
    public bool DateIsFromPreviousDay(DateTime dateTime)
    {
        return dateTime < DateTime.Today.AddDays(-1) && dateTime >= new DateTime(1940, 1, 1);
    }
}