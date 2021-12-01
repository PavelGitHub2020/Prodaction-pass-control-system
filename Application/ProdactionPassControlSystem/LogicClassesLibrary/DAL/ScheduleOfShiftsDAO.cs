using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Windows;

namespace LogicClassesLibrary.DAL
{
    /// <summary>
    /// The logic of calculating 4 work schedules of 4 shifts of 12 hours
    /// (day and night shifts) and weekends for 10 years from 2021 to 2030
    /// </summary>
    public class ScheduleOfShiftsDAO : BaseDAO, IScheduleOfShiftsDAO
    {
        private static int condition = 0;
        private static DateTime date = new DateTime(2021,1,1);

        private const string SELECT_FIRST_SHIFTS = "SELECT * FROM First_Shift";
        private const string SELECT_SECOND_SHIFTS = "SELECT * FROM Second_Shift";
        private const string SELECT_THIRD_SHIFTS = "SELECT * FROM Third_Shift";
        private const string SELECT_FOURTH_SHIFTS = "SELECT * FROM Fourth_Shift";

        public void AddingDataAboutShifts(ScheduleOfShift sheduleOfShift, int number)
        {
            try
            {
                SqlCommand first = new SqlCommand();
                first.CommandText = SELECT_FIRST_SHIFTS;

                SqlCommand second = new SqlCommand();
                second.CommandText = SELECT_SECOND_SHIFTS;

                SqlCommand third = new SqlCommand();
                third.CommandText = SELECT_THIRD_SHIFTS;

                SqlCommand fourth = new SqlCommand();
                fourth.CommandText = SELECT_FOURTH_SHIFTS;

                IDbConnection connection = GetConnection();

                first.Connection = (SqlConnection)connection;
                second.Connection = (SqlConnection)connection;
                third.Connection = (SqlConnection)connection;
                fourth.Connection = (SqlConnection)connection;

                SqlDataAdapter firstAdapter = new SqlDataAdapter(first);
                DataTable firstTable = new DataTable();
                firstAdapter.Fill(firstTable);

                SqlDataAdapter secondAdapter = new SqlDataAdapter(second);
                DataTable secondTable = new DataTable();
                secondAdapter.Fill(secondTable);

                SqlDataAdapter thirdAdapter = new SqlDataAdapter(third);
                DataTable thirdTable = new DataTable();
                thirdAdapter.Fill(thirdTable);

                SqlDataAdapter fourthAdapter = new SqlDataAdapter(fourth);
                DataTable fourthTable = new DataTable();
                fourthAdapter.Fill(fourthTable);

                CalculationOfDataOnTheShift(sheduleOfShift, number, firstAdapter, firstTable, 8);
                CalculationOfDataOnTheShift(sheduleOfShift, number, secondAdapter, secondTable, 32);
                CalculationOfDataOnTheShift(sheduleOfShift, number, thirdAdapter, thirdTable, 56);
                CalculationOfDataOnTheShift(sheduleOfShift, number, fourthAdapter, fourthTable, 20);

                ReleaseConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FillingInRows(ScheduleOfShift sheduleOfShift, DataRow dataRow)
        {
            try
            {
                dataRow[1] = sheduleOfShift.NameOfMonth;
                dataRow[2] = sheduleOfShift.DayOfWeek_D;
                dataRow[3] = sheduleOfShift.DayShift;
                dataRow[4] = sheduleOfShift.StartDayShift;
                dataRow[5] = sheduleOfShift.EndDayShift;
                dataRow[6] = sheduleOfShift.Day_Of_Week_N;
                dataRow[7] = sheduleOfShift.NightShift;
                dataRow[8] = sheduleOfShift.StartNightShift;
                dataRow[9] = sheduleOfShift.EndNightShift;
                dataRow[10] = sheduleOfShift.Day_Of_Week_Off;
                dataRow[11] = sheduleOfShift.DayOff;
                dataRow[12] = sheduleOfShift.StartDayOff;
                dataRow[13] = sheduleOfShift.Day_Of_Week_End;
                dataRow[14] = sheduleOfShift.EndDayOff;
                dataRow[15] = sheduleOfShift.EndDayOffTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FillingInRowsForFourthShift(ScheduleOfShift sheduleOfShift, DataRow dataRow)
        {
            try
            {
                dataRow[1] = sheduleOfShift.NameOfMonth;
                dataRow[2] = sheduleOfShift.Day_Of_Week_N;
                dataRow[3] = sheduleOfShift.NightShift;
                dataRow[4] = sheduleOfShift.StartNightShift;
                dataRow[5] = sheduleOfShift.EndNightShift;
                dataRow[6] = sheduleOfShift.Day_Of_Week_Off;
                dataRow[7] = sheduleOfShift.DayOff;
                dataRow[8] = sheduleOfShift.StartDayOff;
                dataRow[9] = sheduleOfShift.Day_Of_Week_End;
                dataRow[10] = sheduleOfShift.EndDayOff;
                dataRow[11] = sheduleOfShift.EndDayOffTime;
                dataRow[12] = sheduleOfShift.DayOfWeek_D;
                dataRow[13] = sheduleOfShift.DayShift;
                dataRow[14] = sheduleOfShift.StartDayShift;
                dataRow[15] = sheduleOfShift.EndDayShift;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CalculationLogic(ScheduleOfShift sheduleOfShift, int numberOfHours)
        {
            try
            {
                if (condition == 0)
                {
                    date = new DateTime(2021, 1, 1);
                    date = date.AddHours(numberOfHours);
                    condition++;
                }

                sheduleOfShift.NameOfMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                sheduleOfShift.DayOfWeek_D = date.DayOfWeek.ToString();
                sheduleOfShift.DayShift = date.ToLongDateString();
                sheduleOfShift.StartDayShift = date.ToShortTimeString();
                date = date.AddHours(12);
                sheduleOfShift.EndDayShift = date.ToShortTimeString();
                date = date.AddHours(24);

                sheduleOfShift.Day_Of_Week_N = date.DayOfWeek.ToString();
                sheduleOfShift.NightShift = date.ToLongDateString();
                sheduleOfShift.StartNightShift = date.ToShortTimeString();
                date = date.AddHours(12);
                sheduleOfShift.EndNightShift = date.ToShortTimeString();

                sheduleOfShift.Day_Of_Week_Off = date.DayOfWeek.ToString();
                sheduleOfShift.DayOff = date.ToLongDateString();
                sheduleOfShift.StartDayOff = date.ToShortTimeString();
                date = date.AddHours(39);

                sheduleOfShift.Day_Of_Week_End = date.DayOfWeek.ToString();
                sheduleOfShift.EndDayOff = date.ToLongDateString();
                sheduleOfShift.EndDayOffTime = date.ToShortTimeString();
                date = date.AddHours(9);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CalculationLogicForFourthShift(ScheduleOfShift sheduleOfShift,int numberOfHours)
        {
            try
            {
                if (condition == 0)
                {
                    date = new DateTime(2021, 1, 1);
                    date = date.AddHours(numberOfHours);
                    condition++;
                }

                sheduleOfShift.NameOfMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                sheduleOfShift.Day_Of_Week_N = date.DayOfWeek.ToString();
                sheduleOfShift.NightShift = date.ToLongDateString();
                sheduleOfShift.StartNightShift = date.ToShortTimeString();
                date = date.AddHours(12);
                sheduleOfShift.EndNightShift = date.ToShortTimeString();

                sheduleOfShift.Day_Of_Week_Off = date.DayOfWeek.ToString();
                sheduleOfShift.DayOff = date.ToLongDateString();
                sheduleOfShift.StartDayOff = date.ToShortTimeString();
                date = date.AddHours(39);
                sheduleOfShift.Day_Of_Week_End = date.DayOfWeek.ToString();
                sheduleOfShift.EndDayOff = date.ToLongDateString();
                sheduleOfShift.EndDayOffTime = date.ToShortTimeString();
                date = date.AddHours(9);

                sheduleOfShift.DayOfWeek_D = date.DayOfWeek.ToString();
                sheduleOfShift.DayShift = date.ToLongDateString();
                sheduleOfShift.StartDayShift = date.ToShortTimeString();
                date = date.AddHours(12);
                sheduleOfShift.EndDayShift = date.ToShortTimeString();
                date = date.AddHours(24);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CalculationOfDataOnTheShift(ScheduleOfShift sheduleOfShift, int number, SqlDataAdapter adapter, DataTable table, int numberOfHours)
        {
            try
            {
                int i = 1;

                if (numberOfHours == 20)
                {
                    number = 1218;

                    for (; i <= number; i++)
                    {
                        DataRow dataRow = table.NewRow();

                        CalculationLogicForFourthShift(sheduleOfShift, numberOfHours);
                        FillingInRowsForFourthShift(sheduleOfShift, dataRow);

                        table.Rows.Add(dataRow);

                        adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();

                        adapter.Update(table);
                    }
                }
                else
                {
                    for (; i <= number; i++)
                    {
                        DataRow dataRow = table.NewRow();

                        CalculationLogic(sheduleOfShift, numberOfHours);
                        FillingInRows(sheduleOfShift, dataRow);

                        table.Rows.Add(dataRow);

                        adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();

                        adapter.Update(table);
                    }
                }

                if (i > number)
                {
                    condition = 0;
                    date = new DateTime(2021, 1, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
