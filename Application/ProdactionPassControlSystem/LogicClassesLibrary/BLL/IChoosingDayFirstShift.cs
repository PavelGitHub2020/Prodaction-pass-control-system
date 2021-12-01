using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    /// <summary>
    /// Methods for displaying the work schedule during the month of the first shift
    /// and a method for clearing information
    /// </summary>
    public interface IChoosingDayFirstShift
    {
        void ChoosingTheDayFirst(List<int> number);
        void ChoosingTheNightFirst(List<int> number);
        void ChoosingTheDayOffFirst(List<int> number);
        void ChoosingTheEndDayOffFirst(List<int> number);

        void ClearTheDaysOfFirst();
    }
}
