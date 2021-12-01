using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    public interface IFillingWithData
    {
        void FillingInWithData(DataTable table, int numOfDays, bool passageControl);
    }
}
