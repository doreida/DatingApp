namespace API.Extensions;

public static class DateTimeExtensions
{
   public static int CalculateAge(this DateOnly dob)
   {
     var today = DateOnly.FromDateTime(DateTime.UtcNow);
     var age = today.Year - dob.Year;
     if(dob>today.AddYears(-age)) age--;
     if (dob.Month == 2 && dob.Day == 29 && !DateTime.IsLeapYear(today.Year)) age--;
     return age;
   }
}