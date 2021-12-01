using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class ScheduleOfShift
    {
        public int ID { get; }
        public string NameOfMonth { get; set; }
        public string DayOfWeek_D { get; set; }
        public string DayShift { get; set; }
        public string StartDayShift { get; set; }
        public string EndDayShift { get; set; }

        public string Day_Of_Week_N { get; set; }
        public string NightShift { get; set; }
        public string StartNightShift { get; set; }
        public string EndNightShift { get; set; }

        public string Day_Of_Week_Off { get; set; }
        public string DayOff { get; set; }
        public string StartDayOff { get; set; }

        public string Day_Of_Week_End { get; set; }
        public string EndDayOff { get; set; }
        public string EndDayOffTime { get; set; }

        public ScheduleOfShift() { }

        public ScheduleOfShift(int id, string nameOfMonth, string dayOfWeek, string dayShift, string startDayShift,
                            string endDayShift, string day_Of_Week_N, string nightShift, string startNightShift,
                            string endNightShift, string day_Of_Week_Off, string dayOff, string startDayOff,
                            string day_Of_Week_End, string endDayOff,string endDayOffTime)
        {
            ID = id;
            NameOfMonth = nameOfMonth;
            DayOfWeek_D = dayOfWeek;
            DayShift = dayShift;
            StartDayShift = startDayShift;
            EndDayShift = endDayShift;
            Day_Of_Week_N = day_Of_Week_N;
            NightShift = nightShift;
            StartNightShift = startNightShift;
            EndNightShift = endNightShift;
            Day_Of_Week_Off = day_Of_Week_Off;
            DayOff = dayOff;
            StartDayOff = startDayOff;
            Day_Of_Week_End = day_Of_Week_End;
            EndDayOff = endDayOff;
            EndDayOffTime = endDayOffTime;
        }

        public override string ToString()
        {
            return $"Name of month - {NameOfMonth}\n" +
                   $"Day of week of day shift - {DayOfWeek_D}\n" +
                   $"Day shift - {DayShift}\n" +
                   $"Start day shift - {StartDayShift}\n" +
                   $"End day shift - {EndDayShift}\n" +
                   $"Day of week of night shift - {Day_Of_Week_N}" +
                   $"Night shift - {NightShift}\n" +
                   $"Start night shift - {StartNightShift}\n" +
                   $"End night shift - {EndNightShift}\n" +
                   $"Day of week of day off - {Day_Of_Week_Off}" +
                   $"Day off - {DayOff}\n" +
                   $"Start day off - {StartDayOff}\n" +
                   $"Day of week of end day off - {Day_Of_Week_End}" +
                   $"End day off - {EndDayOff}\n" +
                   $"End day of time - {EndDayOffTime}";
        }
    }
}
