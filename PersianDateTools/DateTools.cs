using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersianDateTools
{
    public static class DateTools
    {
        public static int CalculateAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;

            return age;
        }

        public static string CalculateBirthDay(DateTime? birthday)
        {
            if (!birthday.HasValue)
            {
                return $"0 سال 0 ماه";
            }

            var today = DateTime.Today;

            var months = today.Month - birthday.Value.Month;
            var years = today.Year - birthday.Value.Year;

            if (today.Day < birthday.Value.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }



            return
                $"{years} سال, {months} ماه";
        }

        public static string Miladi2Shamsi(string mydate)
        {
            try
            {
                var _date = DateTime.Parse(mydate);
                PersianCalendar pc = new PersianCalendar();
                StringBuilder sb = new StringBuilder();
                sb.Append(pc.GetYear(_date).ToString("0000"));
                sb.Append("/");
                sb.Append(pc.GetMonth(_date).ToString("00"));
                sb.Append("/");
                sb.Append(pc.GetDayOfMonth(_date).ToString("00"));
                return sb.ToString();
            }
            catch 
            {
                return "0000/00/00";
                
            }
           
        }

        public static DateTime Shamsi2Miladi(string _date)
        {
            int year = int.Parse(_date.Substring(0, 4));
            int month = int.Parse(_date.Substring(5, 2));
            int day = int.Parse(_date.Substring(8, 2));
            PersianCalendar p = new PersianCalendar();
            DateTime date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date;
        }

        public static string DayName(DateTime d)
        {
            var dname = d.DayOfWeek;
            switch (dname)
            {
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                    
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                default:
                    return "";
            }
        }
    }
}
