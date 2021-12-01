using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    /// <summary>
    /// Methods for displaying the work schedule during the month of the fourth shift
    /// and a method for clearing information
    /// </summary>
    public interface IChoosingDayFourthShift
    {
        void ChoosingTheDayFourth(List<int> number);
        void ChoosingTheNightFourth(List<int> number);
        void ChoosingTheDayOffFourth(List<int> number);
        void ChoosingTheEndDayOffFourth(List<int> number);

        void ClearTheDaysOfFourth();
    }
}
