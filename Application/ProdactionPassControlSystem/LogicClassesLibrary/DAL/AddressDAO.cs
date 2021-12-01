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
    public class AddressDAO : BaseDAO, IAddressDAO
    {
        private const string INSERT_ADDRESS = "INSERT INTO ADDRESS(Name_Of_The_City, Name_Of_The_Street, House_Number, Worker_Id) VALUES(@nameOfTheCity, @nameOfTheStreet, @houseNumber, @workerId)";
        private const string UPDATE = "UPDATE Address SET Name_Of_The_City = @nameOfTheCity, Name_Of_The_Street = @nameOfTheStreet, House_Number = @houseNumber WHERE Address.Worker_Id = @id";
        public void Add(Address address)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = INSERT_ADDRESS;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter nameOfTheCity = new SqlParameter();
                nameOfTheCity.ParameterName = "@nameOfTheCity";
                nameOfTheCity.SqlDbType = System.Data.SqlDbType.NVarChar;
                nameOfTheCity.Value = address.NameOfTheCity;
                command.Parameters.Add(nameOfTheCity);

                SqlParameter nameOfTheStreet = new SqlParameter();
                nameOfTheStreet.ParameterName = "@nameOfTheStreet";
                nameOfTheStreet.SqlDbType = System.Data.SqlDbType.NVarChar;
                nameOfTheStreet.Value = address.NameOfTheStreet;
                command.Parameters.Add(nameOfTheStreet);

                SqlParameter houseNumber = new SqlParameter();
                houseNumber.ParameterName = "@houseNumber";
                houseNumber.SqlDbType = System.Data.SqlDbType.NVarChar;
                houseNumber.Value = address.HouseNumber;
                command.Parameters.Add(houseNumber);

                SqlParameter workerId = new SqlParameter();
                workerId.ParameterName = "@workerId";
                workerId.SqlDbType = System.Data.SqlDbType.Int;
                workerId.Value = address.WorkerId;
                command.Parameters.Add(workerId);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update(Address address)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = UPDATE;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter nameOfTheCity = new SqlParameter();
                nameOfTheCity.ParameterName = "@nameOfTheCity";
                nameOfTheCity.SqlDbType = System.Data.SqlDbType.NVarChar;
                nameOfTheCity.Value = address.NameOfTheCity;
                command.Parameters.Add(nameOfTheCity);

                SqlParameter nameOfTheStreet = new SqlParameter();
                nameOfTheStreet.ParameterName = "@nameOfTheStreet";
                nameOfTheStreet.SqlDbType = System.Data.SqlDbType.NVarChar;
                nameOfTheStreet.Value = address.NameOfTheStreet;
                command.Parameters.Add(nameOfTheStreet);

                SqlParameter houseNumber = new SqlParameter();
                houseNumber.ParameterName = "@houseNumber";
                houseNumber.SqlDbType = System.Data.SqlDbType.NVarChar;
                houseNumber.Value = address.HouseNumber;
                command.Parameters.Add(houseNumber);

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.SqlDbType = System.Data.SqlDbType.Int;
                id.Value = address.WorkerId;
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
