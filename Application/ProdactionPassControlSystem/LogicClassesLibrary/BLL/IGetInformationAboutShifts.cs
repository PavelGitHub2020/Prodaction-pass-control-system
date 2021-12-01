using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicClassesLibrary.DAL;
using LogicClassesLibrary.Entity;

namespace LogicClassesLibrary.BLL
{
    public interface IGetInformationAboutShifts
    {
        void GetInformationAboutShift(ScheduleOfShift scheduleOfShift, bool passageControl, int numOfDays);
    }
}
