using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    /// <summary>
    /// Methods for displaying the work schedule during the month of the second shift
    /// and a method for clearing information
    /// </summary>
    public interface IChoosingDaySecondShift
    {
        void ChoosingTheDaySecond(List<int> number);
        void ChoosingTheNightSecond(List<int> number);
        void ChoosingTheDayOffSecond(List<int> number);
        void ChoosingTheEndDayOffSecond(List<int> number);
        void ClearTheDaysOfSecond();
    }
}
