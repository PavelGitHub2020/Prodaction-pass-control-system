using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.DAL
{
    public interface IScheduleOfShiftsDAO
    {
        void AddingDataAboutShifts(ScheduleOfShift sheduleOfShift, int number);
        void CalculationOfDataOnTheShift(ScheduleOfShift sheduleOfShift, int number, SqlDataAdapter adapter, DataTable table, int numberOfHours);
        void FillingInRows(ScheduleOfShift sheduleOfShift, DataRow dataRow);
        void FillingInRowsForFourthShift(ScheduleOfShift sheduleOfShift, DataRow dataRow);
        void CalculationLogic(ScheduleOfShift sheduleOfShift, int numberOfHours);
        void CalculationLogicForFourthShift(ScheduleOfShift sheduleOfShift, int numberOfHours);
    }
}
