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
    public class PhotoDAO : BaseDAO, IPhotoDAO
    {
        private const string INSERT_PHOTO = "INSERT INTO Photo(Path, Worker_Id) VALUES(@path, @workerId)";
        private const string UPDATE = "UPDATE Photo SET Path = @path WHERE Photo.Worker_Id = @id";
        public void Add(Photo photo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = INSERT_PHOTO;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter path = new SqlParameter();
                path.ParameterName = "@path";
                path.SqlDbType = System.Data.SqlDbType.NVarChar;
                path.Value = photo.Path;
                command.Parameters.Add(path);

                SqlParameter workerId = new SqlParameter();
                workerId.ParameterName = "@workerId";
                workerId.SqlDbType = System.Data.SqlDbType.Int;
                workerId.Value = photo.WorkerId;
                command.Parameters.Add(workerId);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update(Photo photo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = UPDATE;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter path = new SqlParameter();
                path.ParameterName = "@path";
                path.SqlDbType = System.Data.SqlDbType.NVarChar;
                path.Value = photo.Path;
                command.Parameters.Add(path);

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.SqlDbType = System.Data.SqlDbType.Int;
                id.Value = photo.WorkerId;
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
