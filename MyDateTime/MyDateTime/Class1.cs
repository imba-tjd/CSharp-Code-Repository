using System;

namespace ImbaTJD.MyTime
{
    public struct MyTimeSpan
    {
        int totalSeconds;

        public int Day => totalSeconds / (3600 * 24);
        public int Hour => totalSeconds / 3600 % 24;
        public int Minute => totalSeconds / 60 % 60;
        public int Second => totalSeconds % 60;

        public double TotalDays => TotalHours / 24;
        public double TotalHours => TotalMinutes / 60;
        public double TotalMinutes => TotalSeconds / 60;
        public double TotalSeconds => totalSeconds;

        public MyTimeSpan(int second) => totalSeconds = second;
        public MyTimeSpan(int hour, int minute, int second) : this(0, hour, minute, second) { }
        public MyTimeSpan(int day, int hour, int minute, int second) =>
            totalSeconds = (((day * 24) + hour) * 60 + minute) * 60 + second;

        public static MyTimeSpan operator +(MyTimeSpan ts1, MyTimeSpan ts2) => new MyTimeSpan(ts1.Second + ts2.Second);
        public static MyTimeSpan operator -(MyTimeSpan ts1, MyTimeSpan ts2) => new MyTimeSpan(ts1.Second - ts2.Second);
        public static bool operator ==(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds == ts2.totalSeconds;
        public static bool operator !=(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds != ts2.totalSeconds;
        public static bool operator >(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds > ts2.totalSeconds;
        public static bool operator <(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds < ts2.totalSeconds;
        public static bool operator >=(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds >= ts2.totalSeconds;
        public static bool operator <=(MyTimeSpan ts1, MyTimeSpan ts2) => ts1.totalSeconds <= ts2.totalSeconds;

        /*
        public int Day => (int)TotalDays;
        public int Hour => (int)TotalHours % 24;
        public int Minute => (int)TotalMinutes % 60;
        public int Second => (int)TotalSeconds % 60;
        */
    }

    public struct MyDateTime
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        readonly static int[] DaysPerMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        static bool IsLeapYear(int year) => year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        static int GetDaysPerMonth(int year, int month) => DaysPerMonth[month] + (IsLeapYear(year) ? 1 : 0);

        public MyDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            if (year < 1 || month < 1 || month > 12 || day < 1 || day > GetDaysPerMonth(year, month) ||
                hour < 0 || hour > 24 || minute < 0 || minute > 24 || second < 0 || second > 24)
                throw new ArgumentOutOfRangeException();

            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
        }
        public MyDateTime(int year, int month, int day) : this(year, month, day, 0, 0, 0) { }


        /*
        bool IsValidDay(int day)
        {
            if (day < 1 || day > 31)
                return false;
            switch (month)
            {
                case 2:
                    if (day > 29 || !IsLeapYear && day == 28)
                        return false;
                    else
                        return true;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day == 30)
                        return false;
                    else
                        return true;
                default:
                    return true;
            }
        }
        */

        public static MyDateTime operator +(MyDateTime dateTime, MyTimeSpan timeSpan)
        {
            int year = dateTime.Year + timeSpan.Year;
            int month = dateTime.Month + timeSpan.Month;
            int day = dateTime.Day + timeSpan.Day;
            int hour = dateTime.Hour + timeSpan.Hour;
            int minute = dateTime.Minute + timeSpan.Minute;
            int second = dateTime.Second + timeSpan.Second;

            minute = second % 60;
            second /= 60;

            hour = minute % 60;
            minute /= 60;

            return new MyDateTime(year, month, day, hour, minute, second);
        }

        public static MyDateTime operator -(MyDateTime dateTime, MyTimeSpan timeSpan) =>
            new MyDateTime(dateTime.Year - timeSpan.Year, dateTime.Month - timeSpan.Month, dateTime.Day - timeSpan.Day,
                dateTime.Hour - timeSpan.Hour, dateTime.Minute - timeSpan.Minute, dateTime.Second - timeSpan.Second);
        public static MyTimeSpan operator -(MyDateTime dateTime1, MyDateTime dateTime2) =>
            new MyTimeSpan(dateTime1.Year - dateTime2.Year, dateTime1.Month - dateTime2.Month, dateTime1.Day - dateTime2.Day,
                dateTime1.Hour - dateTime2.Hour, dateTime1.Minute - dateTime2.Minute, dateTime1.Second - dateTime2.Second);
    }
}
