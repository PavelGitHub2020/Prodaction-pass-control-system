using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace LogicClassesLibrary.DAL
{
    public class WorkerDAO : BaseDAO, IWorkerDAO
    {
        private const string INSERT_WORKER = "INSERT INTO Worker(Surname, Name, Patronymic, Date_Of_Birth, Gender, Phone_Number, Department_Id, Profession, Date_Of_Start_To_Work, Number_Of_Shift) VALUES(@surname, @name, @patronymic, @dateOfBirth, @gender, @phoneNumber, @departmentId, @profession, @dateOfStartToWork, @numberOfShift)";
        private const string DELETE_WORKER_BY_ID = "DELETE FROM Worker WHERE Worker_Id = @Id";
        private const string Get_Max_Id = "SELECT MAX(Worker_Id) FROM Worker";
        private const string FIND_WORKER = "SELECT * FROM Worker, Address, Pass, Department, Photo WHERE Worker.Worker_Id = Pass.Worker_Id AND Worker.Department_Id = Department.Department_Id AND Worker.Worker_Id = Photo.Worker_Id AND Worker.Worker_Id = Address.Worker_Id AND Pass.Number = @passNumber AND Department.Name = @departmentName";
        private const string GetChangedInformation_ = "SELECT Number_Of_Day, Condition, Since_What_Time, Till_What_Time, Value FROM Individual_Work_Shedule_ WHERE Year = @year AND Month = @month AND Worker_Id = @workerId";
        private const string CheckingForChangedDataInTheDatabase_ = "SELECT * FROM Individual_Work_Shedule_ WHERE Year = @year AND Month = @month AND Worker_Id = @workId";
        private const string Get_Surname_DepartmentName_PassNumber = "SELECT Worker.Surname, Department.Name as 'Department name', Pass.Number as 'Pass number' FROM Worker, Department, Pass WHERE Worker.Department_Id = Department.Department_Id AND Worker.Worker_Id = Pass.Worker_Id";
        private const string Get_All_Worker = "SELECT * FROM Worker w LEFT JOIN Address a on a.Worker_Id = w.Worker_Id LEFT JOIN Pass p on p.Worker_Id = w.Worker_Id LEFT JOIN Department d on d.Department_Id = w.Department_Id LEFT JOIN Photo ph on ph.Worker_Id = w.Worker_Id";
        private const string Get_All_Worker_By_Department = "SELECT * FROM Worker w LEFT JOIN Address a on a.Worker_Id = w.Worker_Id LEFT JOIN Pass p on p.Worker_Id = w.Worker_Id LEFT JOIN Department d on d.Department_Id = w.Department_Id LEFT JOIN Photo ph on ph.Worker_Id = w.Worker_Id WHERE d.Name = @departmentName";
        private const string FindNumberWorkerInDepartment = "SELECT COUNT(Worker_Id) FROM Worker, Department WHERE Worker.Department_Id = Department.Department_Id AND Department.Name = @name";
        private const string Availability_Of_A_Specific_Worker_Id = "DECLARE @s INT EXEC @s = dbo.WorkerId @workerId";
        private const string UPDATE = "UPDATE Worker SET Surname = @surname, Name = @name, Patronymic = @patronymic, Date_Of_Birth = @dateOfBirth, Gender = @gender, Phone_Number = @phoneNumber, Department_Id = @departmentId, Profession = @profession, Date_Of_Start_To_Work = @dateOfStartToWork, Number_Of_Shift = @numberOfShift WHERE Worker.Worker_Id = @id";
        private const string UPDATE_CHANGED_SHEDULE = "UPDATE Individual_Work_Shedule_ SET Condition = @condition, Since_What_Time = @sinceWhatTime, Till_What_Time = @tillWhatTime, Value = @value WHERE Number_Of_Day = @numberOfDay AND Worker_Id = @workerId AND Year = @year AND Month = @month";
        private const string GET_WORKER_BY_ID = "SELECT * FROM Worker w LEFT JOIN Address a on a.Worker_Id = w.Worker_Id LEFT JOIN Pass p on p.Worker_Id = w.Worker_Id LEFT JOIN Department d on d.Department_Id = w.Department_Id LEFT JOIN Photo ph on ph.Worker_Id = w.Worker_Id WHERE w.Worker_Id = @id";
        private const string Get_Surname_DepartmentName_PassNumber_NumberOfShift = "SELECT Worker.Worker_Id, Worker.Surname, Department.Name as 'Department name', Pass.Number as 'Pass number', Worker.Number_Of_Shift FROM Worker, Department, Pass WHERE Worker.Department_Id = Department.Department_Id AND Worker.Worker_Id = Pass.Worker_Id";
        private const string Add_Changed_Information = "INSERT INTO Individual_Work_Shedule_(Number_Of_Day, Year, Month, Condition, Since_What_Time, Till_What_Time, Value, Worker_Id) VALUES(@numberOfDay, @year, @month, @condition, @sinceWhatTime, @tillWhatTime, @value, @workerId)";
        private const string AddDataOfTheUseOfAPassByAWorker = "IF NOT EXISTS (SELECT * FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE Year = @year AND Month = @month AND NumberOfDay = @numberOfDay AND WorkerId = @workerId AND TimeOfUseOfThePass = @timeOfUseOfThePass) INSERT INTO Data_Of_The_Use_Of_A_Pass_By_A_Worker(NumberOfDay, Year, Month, Condition, SinceWhatTime, TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass) VALUES (@numberOfDay, @year, @month, @condition, @sinceWhatTime, @tillWhatTime, @value, @workerId, @timeOfUseOfThePass, @theResultOfUsingOfThePass)";
        private const string Get_WorkerId_Number_Of_Shift = "SELECT Worker_Id, Number_Of_Shift FROM Worker";
        private const string Get_Values_Of_Time = "SELECT Since_What_Time, Till_What_Time FROM Individual_Work_Shedule_ WHERE Number_Of_Day = @numberOfDay AND Year = @year AND Month = @month AND Worker_Id = @workerId";
        private const string Get_WorkerId_From_Changed_Information = "SELECT DISTINCT Worker_Id FROM Individual_Work_Shedule_ WHERE Year = @year AND Month = @month";
        private const string GetAllInformationAboutUseThePass_ = "SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker";
        private const string GetAllInformationAboutUseThePassByWorkerId_ = "SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId";
        private const string GetAllInformationAboutUseThePassByWorkerIdYear_ = "SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year";
        private const string GetAllInformationAboutUseThePassByWorkerIdYearMonth_ = "SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month";
        private const string GetAllInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay_ = "SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month AND NumberOfDay = @numberOfDay";
        private const string DeleteAllFromInformationAboutUseThePass_ = "DELETE FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker";
        private const string DeleteAllInformationAboutUseThePassByWorkerId_ = "DELETE FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId";
        private const string DeletemoreSpecificInformationAboutUseThePassByWorkerIdYear_ = "DELETE FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year";
        private const string DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonth_ = "DELETE FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month";
        private const string DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay_ = "DELETE FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month AND NumberOfDay = @numberOfDay";
        private const string TotalNumberOfPassesUsed_ = "SELECT COUNT(*) FROM ( SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker) t";
        private const string TotalNumberOfPassesUsedByWorkerId_ = "SELECT COUNT(WorkerId) FROM( SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId) t";
        private const string TotalNumberOfPassesUsedByWorkerIdYear_ = "SELECT COUNT(WorkerId) FROM( SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year) t";
        private const string TotalNumberOfPassesUsedByWorkerIdYearMonth_ = "SELECT COUNT(WorkerId) FROM( SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month) t";
        private const string TotalNumberOfPassesUsedByWorkerIdYearMonthNumberOfDay_ = "SELECT COUNT(WorkerId) FROM( SELECT DISTINCT Year, Month, NumberOfDay, Condition, SinceWhatTime,TillWhatTime, Value, WorkerId, TimeOfUseOfThePass, TheResultOfUsingOfThePass FROM Data_Of_The_Use_Of_A_Pass_By_A_Worker WHERE WorkerId = @workerId AND Year = @year AND Month = @month AND NumberOfDay = @numberOfDay) t";
        private const string GetTheNumberOfWorkers = "SELECT COUNT (Worker_Id) FROM Worker";

        public List<Worker> worker = new List<Worker>();
        public List<Pass> pass_ = new List<Pass>();
        public List<Address> address = new List<Address>();
        public List<Photo> photo = new List<Photo>();
        public List<string> depName = new List<string>();

        public void Add(Worker worker)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = INSERT_WORKER;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter surname = new SqlParameter();
                surname.ParameterName = "@surname";
                surname.SqlDbType = System.Data.SqlDbType.NVarChar;
                surname.Value = worker.Surname;
                command.Parameters.Add(surname);

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@name";
                name.SqlDbType = System.Data.SqlDbType.NVarChar;
                name.Value = worker.Name;
                command.Parameters.Add(name);

                SqlParameter patronymic = new SqlParameter();
                patronymic.ParameterName = "@patronymic";
                patronymic.SqlDbType = System.Data.SqlDbType.NVarChar;
                patronymic.Value = worker.Patronymic;
                command.Parameters.Add(patronymic);

                SqlParameter dateOfBirth = new SqlParameter();
                dateOfBirth.ParameterName = "@dateOfBirth";
                dateOfBirth.SqlDbType = System.Data.SqlDbType.Date;
                dateOfBirth.Value = worker.DateOfBirth;
                command.Parameters.Add(dateOfBirth);

                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@gender";
                gender.SqlDbType = System.Data.SqlDbType.Bit;
                gender.Value = worker.Gender;
                command.Parameters.Add(gender);

                SqlParameter phoneNumber = new SqlParameter();
                phoneNumber.ParameterName = "@phoneNumber";
                phoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                phoneNumber.Value = worker.PhoneNumber;
                command.Parameters.Add(phoneNumber);

                SqlParameter departmentId = new SqlParameter();
                departmentId.ParameterName = "@departmentId";
                departmentId.SqlDbType = System.Data.SqlDbType.Int;
                departmentId.Value = worker.DepartmentId;
                command.Parameters.Add(departmentId);

                SqlParameter profession = new SqlParameter();
                profession.ParameterName = "@profession";
                profession.SqlDbType = System.Data.SqlDbType.NVarChar;
                profession.Value = worker.Profession;
                command.Parameters.Add(profession);

                SqlParameter dateOfStartToWork = new SqlParameter();
                dateOfStartToWork.ParameterName = "@dateOfStartToWork";
                dateOfStartToWork.SqlDbType = System.Data.SqlDbType.Date;
                dateOfStartToWork.Value = worker.DateOfStartToWork;
                command.Parameters.Add(dateOfStartToWork);

                SqlParameter numberOfShift = new SqlParameter();
                numberOfShift.ParameterName = "@numberOfShift";
                numberOfShift.SqlDbType = System.Data.SqlDbType.Int;
                numberOfShift.Value = worker.NumberOfShift;
                command.Parameters.Add(numberOfShift);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddFieldsForChanging(FieldsForChangingTheWorkShedule fieldsForChangingTheWorkShedule, int id, List<FieldsForChangingTheWorkShedule> fields, int year, string month)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = Add_Changed_Information;

            IDbConnection connection = GetConnection();

            command.Connection = (SqlConnection)connection;

            try
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    fieldsForChangingTheWorkShedule = new FieldsForChangingTheWorkShedule(fields[i].NumberOfDay, fields[i].Condition,
                                                                                          fields[i].SinceWhatTime, fields[i].TillWhatTime,
                                                                                          fields[i].Value);

                    SqlParameter numberOfDay = new SqlParameter();
                    numberOfDay.ParameterName = "@numberOfDay";
                    numberOfDay.SqlDbType = System.Data.SqlDbType.Int;
                    numberOfDay.Value = fieldsForChangingTheWorkShedule.NumberOfDay;
                    command.Parameters.Add(numberOfDay);

                    SqlParameter year_ = new SqlParameter();
                    year_.ParameterName = "@year";
                    year_.SqlDbType = System.Data.SqlDbType.Int;
                    year_.Value = year;
                    command.Parameters.Add(year_);

                    SqlParameter month_ = new SqlParameter();
                    month_.ParameterName = "@month";
                    month_.SqlDbType = System.Data.SqlDbType.VarChar;
                    month_.Value = month;
                    command.Parameters.Add(month_);

                    SqlParameter condition = new SqlParameter();
                    condition.ParameterName = "@condition";
                    condition.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (fieldsForChangingTheWorkShedule.Condition == "")
                    {
                        condition.Value = "Undefined";
                    }
                    else
                    {
                        condition.Value = fieldsForChangingTheWorkShedule.Condition;
                    }
                    command.Parameters.Add(condition);

                    SqlParameter sinceWhatTime = new SqlParameter();
                    sinceWhatTime.ParameterName = "@sinceWhatTime";
                    sinceWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (fieldsForChangingTheWorkShedule.SinceWhatTime == "-" || fieldsForChangingTheWorkShedule.SinceWhatTime == "")
                    {
                        sinceWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        sinceWhatTime.Value = fieldsForChangingTheWorkShedule.SinceWhatTime;
                    }
                    command.Parameters.Add(sinceWhatTime);

                    SqlParameter tillWhatTime = new SqlParameter();
                    tillWhatTime.ParameterName = "@tillWhatTime";
                    tillWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (fieldsForChangingTheWorkShedule.TillWhatTime == "-" || fieldsForChangingTheWorkShedule.TillWhatTime == "")
                    {
                        tillWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        tillWhatTime.Value = fieldsForChangingTheWorkShedule.TillWhatTime;
                    }
                    command.Parameters.Add(tillWhatTime);

                    SqlParameter value = new SqlParameter();
                    value.ParameterName = "@value";
                    value.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (fieldsForChangingTheWorkShedule.Value == "")
                    {
                        value.Value = "undefined";
                    }
                    else
                    {
                        value.Value = fieldsForChangingTheWorkShedule.Value;
                    }
                    command.Parameters.Add(value);

                    SqlParameter workerId = new SqlParameter();
                    workerId.ParameterName = "@workerId";
                    workerId.SqlDbType = System.Data.SqlDbType.Int;
                    workerId.Value = id;
                    command.Parameters.Add(workerId);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ReleaseConnection(connection);
        }

        public DataTable GetChangedInformation(int year, string month, int workId)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetChangedInformation_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter workId_ = new SqlParameter();
                workId_.ParameterName = "@workerId";
                workId_.SqlDbType = System.Data.SqlDbType.Int;
                workId_.Value = workId;
                command.Parameters.Add(workId_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public int ChekingForChangedDataInTheDatabase(int year, string month, int workId)
        {
            int id = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = CheckingForChangedDataInTheDatabase_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter workId_ = new SqlParameter();
                workId_.ParameterName = "@workId";
                workId_.SqlDbType = System.Data.SqlDbType.Int;
                workId_.Value = workId;
                command.Parameters.Add(workId_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return id;
        }

        public void FindByPassNumberAndDepartmentName(string passNumber, string departmentName)
        {
            worker = new List<Worker>();
            address = new List<Address>();
            pass_ = new List<Pass>();
            photo = new List<Photo>();
            depName = new List<string>();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = FIND_WORKER;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter passNumber_ = new SqlParameter();
                passNumber_.ParameterName = "@passNumber";
                passNumber_.SqlDbType = System.Data.SqlDbType.VarChar;
                passNumber_.Value = passNumber;
                command.Parameters.Add(passNumber_);

                SqlParameter departmentName_ = new SqlParameter();
                departmentName_.ParameterName = "@departmentName";
                departmentName_.SqlDbType = System.Data.SqlDbType.NVarChar;
                departmentName_.Value = departmentName;
                command.Parameters.Add(departmentName_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int workerId = (int)reader[0];
                        string surname = (string)reader[1] + "";
                        string name = (string)reader[2] + "";
                        string patronymic = (string)reader[3] + "";
                        string dateOfBirth = reader[4].ToString() + "";
                        dateOfBirth = dateOfBirth.Substring(0, dateOfBirth.Length - 7);
                        bool gender = (bool)reader[5];
                        string phoneNumber = (string)reader[6] + "";
                        int departmentId = (int)reader[7];
                        string profession = (string)reader[8] + "";
                        string dateOfStartToWork = reader[9].ToString() + "";
                        dateOfStartToWork = dateOfStartToWork.Substring(0, dateOfStartToWork.Length - 7);
                        int numberOfShift = (int)reader[10];

                        worker.Add(new Worker(workerId, surname, name, patronymic, dateOfBirth, gender, phoneNumber, departmentId,
                                              profession, dateOfStartToWork, numberOfShift));

                        int addressId = (int)reader[11];
                        string nameOfTheCity = (string)reader[12] + "";
                        string nameOfTheStreet = (string)reader[13] + "";
                        string houseNumber = (string)reader[14] + "";
                        int woId = (int)reader[15];

                        address.Add(new Address(addressId, nameOfTheCity, nameOfTheStreet, houseNumber, woId));

                        int passId = (int)reader[16];
                        string number = (string)reader[17] + "";
                        bool condition = (bool)reader[18];
                        int workId = (int)reader[19];

                        pass_.Add(new Pass(passId, number, condition, workId));

                        int depId = (int)reader[20];
                        depName.Add((string)reader[21] + "");

                        int photoId = (int)reader[22];
                        string path = ((string)reader[23] + "");
                        int worId = (int)reader[24];

                        photo.Add(new Photo(photoId, path, worId));
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int FindTheNumberOfEmployeesInDepartment(string nameOfDepartment)
        {
            int quantity = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = FindNumberWorkerInDepartment;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@name";
                name.SqlDbType = System.Data.SqlDbType.NVarChar;
                name.Value = nameOfDepartment;
                command.Parameters.Add(name);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantity = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return quantity;
        }

        public DataTable GetAllWorker()
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_All_Worker;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            return table;
        }

        public DataTable GetAllWorkerByDepartment(string departmentName)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_All_Worker_By_Department;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter departmentName_ = new SqlParameter();
                departmentName_.ParameterName = "@departmentName";
                departmentName_.SqlDbType = System.Data.SqlDbType.NVarChar;
                departmentName_.Value = departmentName;
                command.Parameters.Add(departmentName_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public int GetMaxId()
        {
            int id = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_Max_Id;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }

        public DataTable GetTable()
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_Surname_DepartmentName_PassNumber;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetTable1()
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_Surname_DepartmentName_PassNumber_NumberOfShift;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetWorkerById(int id)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GET_WORKER_BY_ID;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter id_ = new SqlParameter();
                id_.ParameterName = "@id";
                id_.SqlDbType = System.Data.SqlDbType.Int;
                id_.Value = id;
                command.Parameters.Add(id_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(table);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public int AvailabilityOfASpecificWorkerId(int workerId)
        {
            int id = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Availability_Of_A_Specific_Worker_Id;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }

        public void Remove(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DELETE_WORKER_BY_ID;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter id_ = new SqlParameter();
                id_.ParameterName = "@id";
                id_.SqlDbType = System.Data.SqlDbType.Int;
                id_.Value = id;

                command.Parameters.Add(id_);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public void Update(Worker worker)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = UPDATE;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter surname = new SqlParameter();
                surname.ParameterName = "@surname";
                surname.SqlDbType = System.Data.SqlDbType.NVarChar;
                surname.Value = worker.Surname;
                command.Parameters.Add(surname);

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@name";
                name.SqlDbType = System.Data.SqlDbType.NVarChar;
                name.Value = worker.Name;
                command.Parameters.Add(name);

                SqlParameter patronymic = new SqlParameter();
                patronymic.ParameterName = "@patronymic";
                patronymic.SqlDbType = System.Data.SqlDbType.NVarChar;
                patronymic.Value = worker.Patronymic;
                command.Parameters.Add(patronymic);

                SqlParameter dateOfBirth = new SqlParameter();
                dateOfBirth.ParameterName = "@dateOfBirth";
                dateOfBirth.SqlDbType = System.Data.SqlDbType.Date;
                dateOfBirth.Value = worker.DateOfBirth;
                command.Parameters.Add(dateOfBirth);

                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@gender";
                gender.SqlDbType = System.Data.SqlDbType.Bit;
                gender.Value = worker.Gender;
                command.Parameters.Add(gender);

                SqlParameter phoneNumber = new SqlParameter();
                phoneNumber.ParameterName = "@phoneNumber";
                phoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                phoneNumber.Value = worker.PhoneNumber;
                command.Parameters.Add(phoneNumber);

                SqlParameter departmentId = new SqlParameter();
                departmentId.ParameterName = "@departmentId";
                departmentId.SqlDbType = System.Data.SqlDbType.Int;
                departmentId.Value = worker.DepartmentId;
                command.Parameters.Add(departmentId);

                SqlParameter profession = new SqlParameter();
                profession.ParameterName = "@profession";
                profession.SqlDbType = System.Data.SqlDbType.NVarChar;
                profession.Value = worker.Profession;
                command.Parameters.Add(profession);

                SqlParameter dateOfStartToWork = new SqlParameter();
                dateOfStartToWork.ParameterName = "@dateOfStartToWork";
                dateOfStartToWork.SqlDbType = System.Data.SqlDbType.NVarChar;
                dateOfStartToWork.Value = worker.DateOfStartToWork;
                command.Parameters.Add(dateOfStartToWork);

                SqlParameter numberOfShift = new SqlParameter();
                numberOfShift.ParameterName = "@numberOfShift";
                numberOfShift.SqlDbType = System.Data.SqlDbType.Int;
                numberOfShift.Value = worker.NumberOfShift;
                command.Parameters.Add(numberOfShift);

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.SqlDbType = System.Data.SqlDbType.Int;
                id.Value = worker.ID;
                command.Parameters.Add(id);

                command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateChangedShedule(FieldsForChangingTheWorkShedule fieldsForChangingTheWorkShedule, int id, List<FieldsForChangingTheWorkShedule> fields, int year, string month)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = UPDATE_CHANGED_SHEDULE;

            IDbConnection connection = GetConnection();

            command.Connection = (SqlConnection)connection;

            try
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    fieldsForChangingTheWorkShedule = new FieldsForChangingTheWorkShedule(fields[i].NumberOfDay, fields[i].Condition,
                                                                                          fields[i].SinceWhatTime, fields[i].TillWhatTime,
                                                                                          fields[i].Value);

                    SqlParameter numberOfDay = new SqlParameter();
                    numberOfDay.ParameterName = "@numberOfDay";
                    numberOfDay.SqlDbType = System.Data.SqlDbType.Int;
                    numberOfDay.Value = fieldsForChangingTheWorkShedule.NumberOfDay;
                    command.Parameters.Add(numberOfDay);

                    SqlParameter condition = new SqlParameter();
                    condition.ParameterName = "@condition";
                    condition.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (fieldsForChangingTheWorkShedule.Condition == "")
                    {
                        condition.Value = "Undefined";
                    }
                    else
                    {
                        condition.Value = fieldsForChangingTheWorkShedule.Condition;
                    }
                    command.Parameters.Add(condition);

                    SqlParameter sinceWhatTime = new SqlParameter();
                    sinceWhatTime.ParameterName = "@sinceWhatTime";
                    sinceWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (fieldsForChangingTheWorkShedule.SinceWhatTime == "-" || fieldsForChangingTheWorkShedule.SinceWhatTime == "")
                    {
                        sinceWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        sinceWhatTime.Value = fieldsForChangingTheWorkShedule.SinceWhatTime;
                    }
                    command.Parameters.Add(sinceWhatTime);

                    SqlParameter tillWhatTime = new SqlParameter();
                    tillWhatTime.ParameterName = "@tillWhatTime";
                    tillWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (fieldsForChangingTheWorkShedule.TillWhatTime == "-" || fieldsForChangingTheWorkShedule.TillWhatTime == "")
                    {
                        tillWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        tillWhatTime.Value = fieldsForChangingTheWorkShedule.TillWhatTime;
                    }
                    command.Parameters.Add(tillWhatTime);

                    SqlParameter value = new SqlParameter();
                    value.ParameterName = "@value";
                    value.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (fieldsForChangingTheWorkShedule.Value == "")
                    {
                        value.Value = "undefined";
                    }
                    else
                    {
                        value.Value = fieldsForChangingTheWorkShedule.Value;
                    }
                    command.Parameters.Add(value);

                    SqlParameter workerId = new SqlParameter();
                    workerId.ParameterName = "@workerId";
                    workerId.SqlDbType = System.Data.SqlDbType.Int;
                    workerId.Value = id;
                    command.Parameters.Add(workerId);

                    SqlParameter year_ = new SqlParameter();
                    year_.ParameterName = "@year";
                    year_.SqlDbType = System.Data.SqlDbType.Int;
                    year_.Value = year;
                    command.Parameters.Add(year_);

                    SqlParameter month_ = new SqlParameter();
                    month_.ParameterName = "@month";
                    month_.SqlDbType = System.Data.SqlDbType.VarChar;
                    month_.Value = month;
                    command.Parameters.Add(month_);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ReleaseConnection(connection);
        }

        public DataTable GetWorkerIdAndNumberOfShift()
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_WorkerId_Number_Of_Shift;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetWorkerIdFromChangedInformation(int year, string month)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_WorkerId_From_Changed_Information;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetValuesOfTime(int numberOfDay, int year, string month, int workerId)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Get_Values_Of_Time;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter numberOfDay_ = new SqlParameter();
                numberOfDay_.ParameterName = "@numberOfDay";
                numberOfDay_.SqlDbType = System.Data.SqlDbType.Int;
                numberOfDay_.Value = numberOfDay;
                command.Parameters.Add(numberOfDay_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public void Add_Data_Of_The_Use_Of_A_Pass_By_A_Worker(ControlOfTheUseOfThePass controlOfTheUseOfThePass, List<ControlOfTheUseOfThePass> controls, string timeOfUseOfThePass)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = AddDataOfTheUseOfAPassByAWorker;

            IDbConnection connection = GetConnection();

            command.Connection = (SqlConnection)connection;

            try
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    controlOfTheUseOfThePass = new ControlOfTheUseOfThePass(controls[i].Year, controls[i].Month, controls[i].WorkerId, timeOfUseOfThePass,
                                                                           controls[i].TheResultOfUsingThePass, controls[i].NumberOfDay, controls[i].Condition,
                                                                           controls[i].SinceWhatTime, controls[i].TillWhatTime, controls[i].Value);

                    SqlParameter year = new SqlParameter();
                    year.ParameterName = "@year";
                    year.SqlDbType = System.Data.SqlDbType.Int;
                    year.Value = controlOfTheUseOfThePass.Year;
                    command.Parameters.Add(year);

                    SqlParameter month = new SqlParameter();
                    month.ParameterName = "@month";
                    month.SqlDbType = System.Data.SqlDbType.VarChar;
                    month.Value = controlOfTheUseOfThePass.Month;
                    command.Parameters.Add(month);

                    SqlParameter workerId = new SqlParameter();
                    workerId.ParameterName = "@workerId";
                    workerId.SqlDbType = System.Data.SqlDbType.Int;
                    workerId.Value = controlOfTheUseOfThePass.WorkerId;
                    command.Parameters.Add(workerId);

                    SqlParameter timeOfUseOfThePass_ = new SqlParameter();
                    timeOfUseOfThePass_.ParameterName = "@timeOfUseOfThePass";
                    timeOfUseOfThePass_.SqlDbType = System.Data.SqlDbType.Time;
                    timeOfUseOfThePass_.Value = timeOfUseOfThePass;
                    command.Parameters.Add(timeOfUseOfThePass_);

                    SqlParameter theResultOfUsingThePass = new SqlParameter();
                    theResultOfUsingThePass.ParameterName = "@theResultOfUsingOfThePass";
                    theResultOfUsingThePass.SqlDbType = System.Data.SqlDbType.VarChar;
                    theResultOfUsingThePass.Value = controlOfTheUseOfThePass.TheResultOfUsingThePass;
                    command.Parameters.Add(theResultOfUsingThePass);

                    SqlParameter numberOfDay = new SqlParameter();
                    numberOfDay.ParameterName = "@numberOfDay";
                    numberOfDay.SqlDbType = System.Data.SqlDbType.Int;
                    numberOfDay.Value = controlOfTheUseOfThePass.NumberOfDay;
                    command.Parameters.Add(numberOfDay);

                    SqlParameter condition = new SqlParameter();
                    condition.ParameterName = "@condition";
                    condition.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (controlOfTheUseOfThePass.Condition == "")
                    {
                        condition.Value = "Undefined";
                    }
                    else
                    {
                        condition.Value = controlOfTheUseOfThePass.Condition;
                    }
                    command.Parameters.Add(condition);

                    SqlParameter sinceWhatTime = new SqlParameter();
                    sinceWhatTime.ParameterName = "@sinceWhatTime";
                    sinceWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (controlOfTheUseOfThePass.SinceWhatTime == "-" || controlOfTheUseOfThePass.SinceWhatTime == "")
                    {
                        sinceWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        sinceWhatTime.Value = controlOfTheUseOfThePass.SinceWhatTime;
                    }
                    command.Parameters.Add(sinceWhatTime);

                    SqlParameter tillWhatTime = new SqlParameter();
                    tillWhatTime.ParameterName = "@tillWhatTime";
                    tillWhatTime.SqlDbType = System.Data.SqlDbType.Time;
                    if (controlOfTheUseOfThePass.TillWhatTime == "-" || controlOfTheUseOfThePass.TillWhatTime == "")
                    {
                        tillWhatTime.Value = "0:00:00";
                    }
                    else
                    {
                        tillWhatTime.Value = controlOfTheUseOfThePass.TillWhatTime;
                    }
                    command.Parameters.Add(tillWhatTime);

                    SqlParameter value = new SqlParameter();
                    value.ParameterName = "@value";
                    value.SqlDbType = System.Data.SqlDbType.VarChar;
                    if (controlOfTheUseOfThePass.Value == "")
                    {
                        value.Value = "undefined";
                    }
                    else
                    {
                        value.Value = controlOfTheUseOfThePass.Value;
                    }
                    command.Parameters.Add(value);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ReleaseConnection(connection);
        }

        public DataTable GetAllInformationAboutUseThePass()
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetAllInformationAboutUseThePass_;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception)
            {

                throw;
            }

            return table;
        }

        public DataTable GetAllInformationAboutUseThePassByWorkerId(int workerId)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetAllInformationAboutUseThePassByWorkerId_;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter workId = new SqlParameter();
                workId.ParameterName = "@workerId";
                workId.SqlDbType = System.Data.SqlDbType.Int;
                workId.Value = workerId;
                command.Parameters.Add(workId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetAllInformationAboutUseThePassByWorkerIdYear(int workerId, int year)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetAllInformationAboutUseThePassByWorkerIdYear_;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter workId = new SqlParameter();
                workId.ParameterName = "@workerId";
                workId.SqlDbType = System.Data.SqlDbType.Int;
                workId.Value = workerId;
                command.Parameters.Add(workId);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetAllInformationAboutUseThePassByWorkerIdYearMonth(int workerId, int year, string month)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetAllInformationAboutUseThePassByWorkerIdYearMonth_;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter workId = new SqlParameter();
                workId.ParameterName = "@workerId";
                workId.SqlDbType = System.Data.SqlDbType.Int;
                workId.Value = workerId;
                command.Parameters.Add(workId);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public DataTable GetAllInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay)
        {
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetAllInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay_;
                IDbConnection connection = GetConnection();
                command.Connection = (SqlConnection)connection;

                SqlParameter workId = new SqlParameter();
                workId.ParameterName = "@workerId";
                workId.SqlDbType = System.Data.SqlDbType.Int;
                workId.Value = workerId;
                command.Parameters.Add(workId);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter numberOfDay_ = new SqlParameter();
                numberOfDay_.ParameterName = "@numberOfDay";
                numberOfDay_.SqlDbType = System.Data.SqlDbType.Int;
                numberOfDay_.Value = numberOfDay;
                command.Parameters.Add(numberOfDay_);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return table;
        }

        public int DeleteAllFromInformationAboutUseThePass()
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DeleteAllFromInformationAboutUseThePass_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                count = command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int DeleteAllInformationAboutUseThePassByWorkerId(int workerId)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DeleteAllInformationAboutUseThePassByWorkerId_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                count = command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYear(int workerId, int year)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DeletemoreSpecificInformationAboutUseThePassByWorkerIdYear_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                count = command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonth(int workerId, int year, string month)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonth_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                count = command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter numberOfDay_ = new SqlParameter();
                numberOfDay_.ParameterName = "@numberOfDay";
                numberOfDay_.SqlDbType = System.Data.SqlDbType.Int;
                numberOfDay_.Value = numberOfDay;
                command.Parameters.Add(numberOfDay_);

                count = command.ExecuteNonQuery();

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int TotalNumberOfPassesUsed()
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = TotalNumberOfPassesUsed_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int TotalNumberOfPassesUsedByWorkerId(int workerId)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = TotalNumberOfPassesUsedByWorkerId_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int TotalNumberOfPassesUsedByWorkerIdYear(int workerId, int year)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = TotalNumberOfPassesUsedByWorkerIdYear_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int TotalNumberOfPassesUsedByWorkerIdYearMonth(int workerId, int year, string month)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = TotalNumberOfPassesUsedByWorkerIdYearMonth_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int TotalNumberOfPassesUsedByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = TotalNumberOfPassesUsedByWorkerIdYearMonthNumberOfDay_;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlParameter workerId_ = new SqlParameter();
                workerId_.ParameterName = "@workerId";
                workerId_.SqlDbType = System.Data.SqlDbType.Int;
                workerId_.Value = workerId;
                command.Parameters.Add(workerId_);

                SqlParameter year_ = new SqlParameter();
                year_.ParameterName = "@year";
                year_.SqlDbType = System.Data.SqlDbType.Int;
                year_.Value = year;
                command.Parameters.Add(year_);

                SqlParameter month_ = new SqlParameter();
                month_.ParameterName = "@month";
                month_.SqlDbType = System.Data.SqlDbType.VarChar;
                month_.Value = month;
                command.Parameters.Add(month_);

                SqlParameter numberOfDay_ = new SqlParameter();
                numberOfDay_.ParameterName = "@numberOfDay";
                numberOfDay_.SqlDbType = System.Data.SqlDbType.Int;
                numberOfDay_.Value = numberOfDay;
                command.Parameters.Add(numberOfDay_);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }

        public int GetNumberOfWorkers()
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = GetTheNumberOfWorkers;

                IDbConnection connection = GetConnection();

                command.Connection = (SqlConnection)connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = (int)reader[0];
                    }
                }

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return count;
        }
    }
}
