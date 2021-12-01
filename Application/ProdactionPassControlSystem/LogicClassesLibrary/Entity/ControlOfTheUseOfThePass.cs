using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class ControlOfTheUseOfThePass : FieldsForChangingTheWorkShedule
    {
        public string Year { get; set; }
        public string Month { get; set; }

        public int WorkerId { get; set; }
        public string TimeOfUseOfThePass { get; set; }
        public string TheResultOfUsingThePass { get; set; }

        public ControlOfTheUseOfThePass() { }

        public ControlOfTheUseOfThePass(string year, string month, int id, string timeOfTheUseOfThePass, string theResultOfUsingThePass,
                                        int numOfDay, string condition, string sicneWhatTime, string tillWhatTime,
                                        string value) : base(numOfDay, condition,sicneWhatTime,tillWhatTime,value)
        {
            Year = year;
            Month = month;
            WorkerId = id;
            TimeOfUseOfThePass = timeOfTheUseOfThePass;
            TheResultOfUsingThePass = theResultOfUsingThePass;
        }
    }
}
