namespace Backend.Application;

public class DateChecker
{
    
    /// <summary>
    /// Checks if the Date lays inside the range which is given by the Open-Meteo Api.
    /// E.g. No Dates before 1.1.1940 are supported and Date cannot be in the future.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>ture in case of success, false in case of "invalid" date.</returns>
    public bool DateIsFromPreviousDay(DateTime dateTime)
    {
        return dateTime < DateTime.Today && dateTime >= new DateTime(1940, 1, 1);
    }
}