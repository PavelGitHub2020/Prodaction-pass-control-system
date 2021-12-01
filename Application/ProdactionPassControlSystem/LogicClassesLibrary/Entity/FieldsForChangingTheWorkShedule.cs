using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class FieldsForChangingTheWorkShedule
    {
        public int NumberOfDay { get; set; }
        public string Condition { get; set; }
        public string SinceWhatTime { get; set; }
        public string TillWhatTime { get; set; }
        public string Value { get; set; }

        public FieldsForChangingTheWorkShedule() { }

        public FieldsForChangingTheWorkShedule(int numOfDay, string condition, string sinceWhatTime, string tillWhatTime, string value)
        {
            NumberOfDay = numOfDay;
            Condition = condition;
            SinceWhatTime = sinceWhatTime;
            TillWhatTime = tillWhatTime;
            Value = value;
        }
    }
}
