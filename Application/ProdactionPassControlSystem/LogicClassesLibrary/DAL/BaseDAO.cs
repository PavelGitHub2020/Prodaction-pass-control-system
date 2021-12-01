using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LogicClassesLibrary.DAL
{
    public class BaseDAO : IBaseDAO
    {
        public const string STRING_CONNECTION = "Data Source=DESKTOP-1OA4FC7;Initial Catalog=Prodaction_Pass_Control_System;Integrated Security=True";
        public IDbConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(STRING_CONNECTION);
            connection.Open();
            return connection;
        }

        public void ReleaseConnection(IDbConnection connection)
        {
            connection.Close();
        }
    }
}
