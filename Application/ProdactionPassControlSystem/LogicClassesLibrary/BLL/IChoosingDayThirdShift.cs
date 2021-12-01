using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    /// <summary>
    /// Methods for displaying the work schedule during the month of the third shift
    /// and a method for clearing information
    /// </summary>
    public interface IChoosingDayThirdShift
    {
        void ChoosingTheDayThird(List<int> number);
        void ChoosingTheNightThird(List<int> number);
        void ChoosingTheDayOffThird(List<int> number);
        void ChoosingTheEndDayOffThird(List<int> number);

        void ClearTheDaysOfThird();
    }
}
