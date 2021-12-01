using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicClassesLibrary.DAL
{
    public interface IBaseDAO
    {
        IDbConnection GetConnection();
        void ReleaseConnection(IDbConnection connection);
    }
}
