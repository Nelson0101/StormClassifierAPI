namespace Backend.Application;

public class DateChecker
{
    public bool DateIsFromPreviousDay(DateTime dateTime)
    {
        return dateTime < DateTime.Today;
    }
}