using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    public interface IGetSpecificDay
    {
        void GetSpecificDayShiftNumber();
        void GetSpecificNightShiftNumber();
        void GetSpecificStartDayOffNumber();
        void GetSpecificEndDayOffNumber();
    }
}
