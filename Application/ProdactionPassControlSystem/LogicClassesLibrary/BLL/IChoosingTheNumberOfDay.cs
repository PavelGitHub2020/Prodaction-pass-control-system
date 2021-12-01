using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    /// <summary>
    /// A method for selecting day numbers to highlight Saturday and Sunday
    /// and a method for clearing days
    /// </summary>
    public interface IChoosingTheNumberOfDay
    {
        void ChoosingTheNumberOfDay(List<int> number);
        void ClearNumberOfDay();
    }
}
