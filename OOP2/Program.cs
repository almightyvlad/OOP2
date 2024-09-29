using System;
using System.Linq;
public partial class Date
{
    private int day;
    private int month;
    private int year;

    public readonly int ID;

    private static int objectCounter;

    private const int minYear = 1900;
        
    public static void PrintClassInfo()
    {
        Console.WriteLine($"Object count: {objectCounter}");
    }
    private string GetMonthName()
    {
        return new DateTime(year, month, day).ToString("MMMM");
    }

    static Date()
    {
        objectCounter = 0;
    }

    private Date()
    {
        ID = GetHashCode();
    }

    public Date(int day, int month, int year) 
    {
        this.day = day;
        this.month = month;
        this.year = year;
        objectCounter++;
    }

    public Date(int day = 1, int month = 12) : this(day, month, minYear) { }

    public int Day
    {
        get { return day; }
        set
        {
            if (value < 1 || value > DateTime.DaysInMonth(year, month)) 
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан день.");
            }
            day = value;

        }
    }
    public int Month
    {
        get { return month; }
        set
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан месяц.");
            }
            month = value;
        }
    }

    public int Year
    {
        get { return year; }
        set
        {
            if (year < minYear || year > 2024)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан год.");
            }
            year = value;
        }
    }

    public void PrintDate()
    {
        Console.WriteLine($"{day}/{month}/{year}");
        Console.WriteLine($"{day} {GetMonthName()} {year} года");
    }

    public void UpdateDate(ref int newDay, ref int newMonth, out int updatedYear)
    {
        if (newDay < 1 || newDay > DateTime.DaysInMonth(year, newMonth))
        {
            throw new ArgumentOutOfRangeException(nameof(newDay), "Неверно указан день.");
        }

        if (newMonth < 1 || newMonth > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(newMonth), "Неверно указан месяц.");
        }

        day = newDay;
        month = newMonth;

        updatedYear = year;
    }

    public override bool Equals(object obj)
    {
        return obj is Date other && day == other.day && month == other.month && year == other.year;
    }
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + day.GetHashCode();
        hash = hash * 23 + month.GetHashCode();
        return hash;
    }

    public override string ToString()
    {
        return $"Date [day: {day}, month: {month}, year: {year}";
    }

}

namespace OOP2
{
    internal class Program
    {

        
        static void Main(string[] args)
        {

            // 2
            Date date1 = new Date(15, 8, 2023); 
            Date date2 = new Date(1, 12);

            date1.PrintDate();
            date2.PrintDate();

            date1.Day = 16;
            date1.Month = 9;
            date1.Year = 2024;

            date1.PrintDate();

            int newDay = 10;
            int newMonth = 11;
            int updatedYear;

            date1.UpdateDate(ref newDay, ref newMonth, out updatedYear);
            date1.PrintDate();

            Date date3 = new Date(10, 11, 2024);
            Console.WriteLine(date1.Equals(date3));

            Date.PrintClassInfo();

            Console.WriteLine("===========================");

            // 3

            Date[] dates =
            {
                new Date(1, 8, 2023),
                new Date(1, 12, 2024),
                new Date(1, 4, 2006),
                new Date(10, 11, 2024),
                new Date(23, 4, 2008),
                new Date(18, 7, 2023)
            };

            int yearForFilter = 2023;

            var filteredDatesByYear = dates.Where(date => date.Year == yearForFilter);

            foreach(var date in filteredDatesByYear)
            {
                date.PrintDate();
            }

            int dayForFilter = 1;

            var filteredDatesByDay = dates.Where(date => date.Day == dayForFilter);

            foreach(var date in filteredDatesByDay)
            {
                date.PrintDate();
            }

            Console.WriteLine("===========================");


            // 4

            var anonymClass = new
            {
                day = 15,
                month = 8,
                year = 2023,
            };


            Console.WriteLine($"Day: {anonymClass.day}, Month: {anonymClass.month}, Year: {anonymClass.year}");
        }
    }
}
