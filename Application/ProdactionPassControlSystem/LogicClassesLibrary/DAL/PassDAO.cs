using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogicClassesLibrary.DAL
{
    public class PassDAO : BaseDAO, IPassDAO
    {
        private const string INSERT_PASS = "INSERT INTO Pass(Number, Condition, Worker_Id) VALUES(@number, @condition, @workerId)";
        private const string UPDATE = "UPDATE Pass SET Number = @number, Condition = @condition WHERE Pass.Worker_Id = @id";
        public void Add(Pass pass)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = INSERT_PASS;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter number = new SqlParameter();
                number.ParameterName = "@number";
                number.SqlDbType = System.Data.SqlDbType.VarChar;
                number.Value = pass.Number;
                command.Parameters.Add(number);

                SqlParameter condition = new SqlParameter();
                condition.ParameterName = "@condition";
                condition.SqlDbType = System.Data.SqlDbType.Bit;
                condition.Value = pass.Condition;
                command.Parameters.Add(condition);

                SqlParameter workerId = new SqlParameter();
                workerId.ParameterName = "@workerId";
                workerId.SqlDbType = System.Data.SqlDbType.Int;
                workerId.Value = pass.WorkerId;
                command.Parameters.Add(workerId);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update(Pass pass)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = UPDATE;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter number = new SqlParameter();
                number.ParameterName = "@number";
                number.SqlDbType = System.Data.SqlDbType.VarChar;
                number.Value = pass.Number;
                command.Parameters.Add(number);

                SqlParameter condition = new SqlParameter();
                condition.ParameterName = "@condition";
                condition.SqlDbType = System.Data.SqlDbType.Bit;
                condition.Value = pass.Condition;
                command.Parameters.Add(condition);

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.SqlDbType = System.Data.SqlDbType.Int;
                id.Value = pass.WorkerId;
                command.Parameters.Add(id);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
