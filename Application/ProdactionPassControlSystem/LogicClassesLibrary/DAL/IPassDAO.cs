using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicClassesLibrary.Entity;

namespace LogicClassesLibrary.DAL
{
    /// <summary>
    /// Adding information and updating in case of any change
    /// </summary>
    public interface IPassDAO
    {
        void Add(Pass pass);
        void Update(Pass pass);
    }
}
