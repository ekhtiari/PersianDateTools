using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PersianDateTools
{
    public static class PersianDateExtensionMethods
    {
        private static CultureInfo _Culture;
        public static CultureInfo GetPersianCulture()
        {
            if (_Culture == null)
            {
                _Culture = new CultureInfo("fa-IR");
                DateTimeFormatInfo formatInfo = _Culture.DateTimeFormat;
                formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                //formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
                formatInfo.DayNames = new[] { "#1", "#2", "#3", "#4", "#5", "#6", "#0" };
                var monthNames = new[]
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
                formatInfo.AbbreviatedMonthNames =
                    formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
                formatInfo.AMDesignator = "ق.ظ";
                formatInfo.PMDesignator = "ب.ظ";
                formatInfo.ShortDatePattern = "yyyy/MM/dd";
                formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
                formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
                System.Globalization.Calendar cal = new PersianCalendar();

                FieldInfo fieldInfo = _Culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                    fieldInfo.SetValue(_Culture, cal);

                FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(formatInfo, cal);

                _Culture.NumberFormat.NumberDecimalSeparator = "/";
                _Culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
                _Culture.NumberFormat.NumberNegativePattern = 0;
            }
            return _Culture;
        }

        public static string ToPeString(this DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToString(format, GetPersianCulture());
        }
    }

    public static class getDateDetail
    {
        public static List<string> getDayOfThisMonth(DateTime _date)
        {
            List<string> ListDay = new List<string>();

            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = PersianDateExtensionMethods.GetPersianCulture();

            DateTime GetDate = _date;
            string month = GetDate.ToPeString("MM");
            bool firstDay = true;

            for (int i = 31; i > 0; i--)
            {

                if (GetDate.AddDays(-i).ToPeString("MM") == month)
                {
                    if (firstDay)
                    {
                        ListDay.Add(GetDate.AddDays(-i).ToPeString("dddd") + GetDate.AddDays(-i).ToPeString("dd"));
                        firstDay = false;
                    }
                    else
                    {
                        ListDay.Add(GetDate.AddDays(-i).ToPeString("dd"));
                    }

                }

            }



            if (firstDay)
            {
                ListDay.Add(GetDate.ToPeString("dddd") + GetDate.ToPeString("dd"));
                firstDay = false;
            }
            else
            {
                ListDay.Add("-" + GetDate.ToPeString("dd"));
            }

            for (int i = 1; i < 31; i++)
            {

                if (GetDate.AddDays(i).ToPeString("MM") == month)
                {
                    ListDay.Add(GetDate.AddDays(i).ToPeString("dd"));
                }
                else
                {
                    break;
                }

            }



            return ListDay;
        }
    }
}
