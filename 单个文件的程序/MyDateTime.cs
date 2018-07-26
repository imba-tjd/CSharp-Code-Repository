using System;

namespace ImbaTJD.MyTime
{
#pragma warning disable CS0660 // 类型定义运算符 == 或运算符 !=，但不重写 Object.Equals(object o)
#pragma warning disable CS0661 // 类型定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
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

        public MyTimeSpan Negate() => new MyTimeSpan(-this.totalSeconds);
        public MyTimeSpan Duration() => totalSeconds < 0 ? new MyTimeSpan(-this.totalSeconds) : new MyTimeSpan(this.totalSeconds);

        public override string ToString() => string.Format($"{Day}. {Hour}:{Minute}:{Second}");

        public static MyTimeSpan operator -(MyTimeSpan ts) => ts.Negate();
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
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        readonly static int[] DaysPerMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static bool IsLeapYear(int year) => year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        public static int GetDaysPerMonth(int year, int month) => DaysPerMonth[month] + (IsLeapYear(year) ? 1 : 0);

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

        public static MyDateTime operator +(MyDateTime dateTime, MyTimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds < 0)
                return dateTime - timeSpan.Duration();

            // 仅处理timeSpan为正的情况
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day + timeSpan.Day;
            int hour = dateTime.Hour + timeSpan.Hour;
            int minute = dateTime.Minute + timeSpan.Minute;
            int second = dateTime.Second + timeSpan.Second;

            minute = second / 60;
            second %= 60;

            hour = minute / 60;
            minute %= 60;

            day = hour / 24;
            hour %= 24;

            while (day > GetDaysPerMonth(year, month))
            {
                day -= GetDaysPerMonth(year, month++);
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }
            return new MyDateTime(year, month, day, hour, minute, second);
        }
        public static MyDateTime operator -(MyDateTime dateTime, MyTimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds < 0)
                return dateTime + timeSpan.Duration();

            // 仅处理timeSpan为正的情况
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day - timeSpan.Day;
            int hour = dateTime.Hour - timeSpan.Hour;
            int minute = dateTime.Minute - timeSpan.Minute;
            int second = dateTime.Second - timeSpan.Second;

            if (second < 0)
            {
                second += 60;
                minute -= 1;
            }
            if (minute < 0)
            {
                minute += 60;
                hour -= 1;
            }
            if (hour < 0)
            {
                hour += 24;
                day -= 1;
            }
            while (day < 1)
            {
                day += GetDaysPerMonth(year, --month);
                if (month < 1)
                {
                    month = 12;
                    year--;
                }
            }
            return new MyDateTime(year, month, day, hour, minute, second);
        }
        public static MyTimeSpan operator -(MyDateTime dt1, MyDateTime dt2)
        {
            if (dt1 < dt2)
                return -(dt2 - dt1);

            // 只处理dt1 >= dt2的情况
            int year = dt2.Year;
            int month = dt2.Month;
            int day = dt1.Day - dt2.Day;
            int hour = dt1.Hour - dt2.Hour;
            int minute = dt1.Minute - dt2.Minute;
            int second = dt1.Second - dt2.Second;

            if (second < 0)
            {
                second += 60;
                minute -= 1;
            }
            if (minute < 0)
            {
                minute += 60;
                hour -= 1;
            }
            if (hour < 0)
            {
                hour += 24;
                day -= 1;
            }
            while (year != dt1.Year || month != dt1.Month)
            {
                day += GetDaysPerMonth(year, month++);
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }

            // new MyTimeSpan
        }
        public static bool operator ==(MyDateTime dt1, MyDateTime dt2) =>
            dt1.Second == dt2.Second || dt1.Minute == dt2.Minute ||dt1.Hour == dt2.Hour ||
            dt1.Day == dt2.Day || dt1.Month == dt2.Month || dt1.Year == dt2.Year;
        public static bool operator !=(MyDateTime dt1, MyDateTime dt2) => !(dt1 == dt2);
        public static bool operator >(MyDateTime dt1, MyDateTime dt2)
        {
            if (dt1.Year > dt2.Year)
                return true;
            else if (dt1.Month > dt2.Month)
                return true;
            else if (dt1.Day > dt2.Day)
                return true;
            else if (dt1.Hour > dt2.Hour)
                return true;
            else if (dt1.Minute > dt2.Minute)
                return true;
            else if (dt1.Second > dt2.Second)
                return true;

            return false;
        }
        public static bool operator <(MyDateTime dt1, MyDateTime dt2)
        {
            if (dt1.Year < dt2.Year)
                return true;
            else if (dt1.Month < dt2.Month)
                return true;
            else if (dt1.Day < dt2.Day)
                return true;
            else if (dt1.Hour < dt2.Hour)
                return true;
            else if (dt1.Minute < dt2.Minute)
                return true;
            else if (dt1.Second < dt2.Second)
                return true;

            return false;
        }
        public static bool operator >=(MyDateTime dt1, MyDateTime dt2)
        {
            if (dt1.Year >= dt2.Year)
                return true;
            else if (dt1.Month >= dt2.Month)
                return true;
            else if (dt1.Day >= dt2.Day)
                return true;
            else if (dt1.Hour >= dt2.Hour)
                return true;
            else if (dt1.Minute >= dt2.Minute)
                return true;
            else if (dt1.Second >= dt2.Second)
                return true;

            return false;
        }
        public static bool operator <=(MyDateTime dt1, MyDateTime dt2)
        {
            if (dt1.Year <= dt2.Year)
                return true;
            else if (dt1.Month <= dt2.Month)
                return true;
            else if (dt1.Day <= dt2.Day)
                return true;
            else if (dt1.Hour <= dt2.Hour)
                return true;
            else if (dt1.Minute <= dt2.Minute)
                return true;
            else if (dt1.Second <= dt2.Second)
                return true;

            return false;
        }


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
    }
}
