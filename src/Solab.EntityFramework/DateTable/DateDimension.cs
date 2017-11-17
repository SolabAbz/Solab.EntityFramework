using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Solab.EntityFramework.DateTable
{
    public class DateDimension
    {
        [Key]
        public DateTime Date { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int DayOfWeek { get; set; }

        public int DayOfYear { get; set; }

        [MaxLength(16)]
        public string DayOfWeekName { get; set; }

        [MaxLength(16)]
        public string MonthName { get; set; }

        public int Week { get; set; }

        [MaxLength(8)]
        public string Suffix { get; set; }

        public DateDimension(DateTime date)
        {
            Date = date.Date;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
            DayOfWeek = (int)date.DayOfWeek;
            DayOfWeekName = Enum.GetName(typeof(DayOfWeek), date.DayOfWeek);
            MonthName = date.ToString("MMM");
            DayOfYear = date.DayOfYear;
            Week = GetWeek(date);
            Suffix = GetSuffix(date);
        }

        private string GetSuffix(DateTime date)
        {
            switch (date.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        private int GetWeek(DateTime date)
        {
            var culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, System.DayOfWeek.Monday);
        }
    }
}
