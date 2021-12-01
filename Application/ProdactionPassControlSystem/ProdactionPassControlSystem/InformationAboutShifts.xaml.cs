using System;
using System.Collections.Generic;

using System.Windows;
using LogicClassesLibrary.Entity;
using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using System.Drawing.Imaging;
using System.Media;

namespace ProdactionPassControlSystem
{
    /// <summary>
    /// Interaction logic for InformationAboutShifts.xaml
    /// </summary>
    public partial class InformationAboutShifts : Window, IGetInformationAboutShifts,IFillingWithData,IHighlightingSaturdaysAndSundays,
                                                          IGetSpecificDay,IChoosingDayFirstShift,IChoosingDaySecondShift,IChoosingDayThirdShift,
                                                          IChoosingDayFourthShift,IChoosingTheNumberOfDay, IGetArrayOfValuesOfShifts,
                                                          IAudioMessageAboutNotAllParametersAreFilledIn, ISettingSoundParameters,
                                                          ISettingLanguageParameters, IStringMessage, ISetOfParameters, ISetOfParameters2,
                                                          IAudioMessageAboutHereIsResult
    {
        private BaseDAO dao;
        private ScheduleOfShift scheduleOf = new ScheduleOfShift();

        private int numOfDayOfMonth = 0;

        private string dayShift = "";
        private string nightShift = "";
        private string dayOff = "";
        private string endDayOff = "";

        private string firstCommandText = "";
        private string secondCommandText = "";
        private string thirdCommandText = "";
        private string fourthCommandText = "";

        public List<int> numberOfDayShift;
        public List<int> numberOfNightShift;
        public List<int> numberOfDayOff;
        public List<int> numberOfEndDayOff;
        private List<int> numberOfDay;

        private bool soundState;
        private string langaugeState;

        private System.Media.SoundPlayer player;

        private string[] arrayOfRequestsFirst_2021;
        private string[] arrayOfRequestsFirst_2022;
        private string[] arrayOfRequestsFirst_2023;
        private string[] arrayOfRequestsFirst_2024;
        private string[] arrayOfRequestsFirst_2025;
        private string[] arrayOfRequestsFirst_2026;
        private string[] arrayOfRequestsFirst_2027;
        private string[] arrayOfRequestsFirst_2028;
        private string[] arrayOfRequestsFirst_2029;
        private string[] arrayOfRequestsFirst_2030;

        private string[] arrayOfRequestsSecond_2021;
        private string[] arrayOfRequestsSecond_2022;
        private string[] arrayOfRequestsSecond_2023;
        private string[] arrayOfRequestsSecond_2024;
        private string[] arrayOfRequestsSecond_2025;
        private string[] arrayOfRequestsSecond_2026;
        private string[] arrayOfRequestsSecond_2027;
        private string[] arrayOfRequestsSecond_2028;
        private string[] arrayOfRequestsSecond_2029;
        private string[] arrayOfRequestsSecond_2030;

        private string[] arrayOfRequestsThird_2021;
        private string[] arrayOfRequestsThird_2022;
        private string[] arrayOfRequestsThird_2023;
        private string[] arrayOfRequestsThird_2024;
        private string[] arrayOfRequestsThird_2025;
        private string[] arrayOfRequestsThird_2026;
        private string[] arrayOfRequestsThird_2027;
        private string[] arrayOfRequestsThird_2028;
        private string[] arrayOfRequestsThird_2029;
        private string[] arrayOfRequestsThird_2030;

        private string[] arrayOfRequestsFourth_2021;
        private string[] arrayOfRequestsFourth_2022;
        private string[] arrayOfRequestsFourth_2023;
        private string[] arrayOfRequestsFourth_2024;
        private string[] arrayOfRequestsFourth_2025;
        private string[] arrayOfRequestsFourth_2026;
        private string[] arrayOfRequestsFourth_2027;
        private string[] arrayOfRequestsFourth_2028;
        private string[] arrayOfRequestsFourth_2029;
        private string[] arrayOfRequestsFourth_2030;

        public InformationAboutShifts()
        {
            dao = new BaseDAO();
            scheduleOf = new ScheduleOfShift();

            dayShift = "";
            nightShift = "";
            dayOff = "";
            endDayOff = "";

            numberOfDayShift = new List<int>();
            numberOfNightShift = new List<int>();
            numberOfDayOff = new List<int>();
            numberOfEndDayOff = new List<int>();
            numberOfDay = new List<int>();

            arrayOfRequestsFirst_2021 = new string[12];
            arrayOfRequestsFirst_2022 = new string[12];
            arrayOfRequestsFirst_2023 = new string[12];
            arrayOfRequestsFirst_2024 = new string[12];
            arrayOfRequestsFirst_2025 = new string[12];
            arrayOfRequestsFirst_2026 = new string[12];
            arrayOfRequestsFirst_2027 = new string[12];
            arrayOfRequestsFirst_2028 = new string[12];
            arrayOfRequestsFirst_2029 = new string[12];
            arrayOfRequestsFirst_2030 = new string[12];

            arrayOfRequestsSecond_2021 = new string[12];
            arrayOfRequestsSecond_2022 = new string[12];
            arrayOfRequestsSecond_2023 = new string[12];
            arrayOfRequestsSecond_2024 = new string[12];
            arrayOfRequestsSecond_2025 = new string[12];
            arrayOfRequestsSecond_2026 = new string[12];
            arrayOfRequestsSecond_2027 = new string[12];
            arrayOfRequestsSecond_2028 = new string[12];
            arrayOfRequestsSecond_2029 = new string[12];
            arrayOfRequestsSecond_2030 = new string[12];

            arrayOfRequestsThird_2021 = new string[12];
            arrayOfRequestsThird_2022 = new string[12];
            arrayOfRequestsThird_2023 = new string[12];
            arrayOfRequestsThird_2024 = new string[12];
            arrayOfRequestsThird_2025 = new string[12];
            arrayOfRequestsThird_2026 = new string[12];
            arrayOfRequestsThird_2027 = new string[12];
            arrayOfRequestsThird_2028 = new string[12];
            arrayOfRequestsThird_2029 = new string[12];
            arrayOfRequestsThird_2030 = new string[12];

            arrayOfRequestsFourth_2021 = new string[12];
            arrayOfRequestsFourth_2022 = new string[12];
            arrayOfRequestsFourth_2023 = new string[12];
            arrayOfRequestsFourth_2024 = new string[12];
            arrayOfRequestsFourth_2025 = new string[12];
            arrayOfRequestsFourth_2026 = new string[12];
            arrayOfRequestsFourth_2027 = new string[12];
            arrayOfRequestsFourth_2028 = new string[12];
            arrayOfRequestsFourth_2029 = new string[12];
            arrayOfRequestsFourth_2030 = new string[12];

            InitializeComponent();
        }

        public void SelectingYear(string numberOfYear, bool changingInformation)
        {
            try
            {
                string year_ = "";

                if (changingInformation == true)
                {
                    year_ = numberOfYear;
                }
                else
                {
                    year_ = year.Text;
                }

                switch (year_)
                {
                    case "2021":
                        Requests_2021_First();
                        Requests_2021_Second();
                        Requests_2021_Third();
                        Requests_2021_Fourth();
                        break;
                    case "2022":
                        Requests_2022_First();
                        Requests_2022_Second();
                        Requests_2022_Third();
                        Requests_2022_Fourth();
                        break;
                    case "2023":
                        Requests_2023_First();
                        Requests_2023_Second();
                        Requests_2023_Third();
                        Requests_2023_Fourth();
                        break;
                    case "2024":
                        Requests_2024_First();
                        Requests_2024_Second();
                        Requests_2024_Third();
                        Requests_2024_Fourth();
                        break;
                    case "2025":
                        Requests_2025_First();
                        Requests_2025_Second();
                        Requests_2025_Third();
                        Requests_2025_Fourth();
                        break;
                    case "2026":
                        Requests_2026_First();
                        Requests_2026_Second();
                        Requests_2026_Third();
                        Requests_2026_Fourth();
                        break;
                    case "2027":
                        Requests_2027_First();
                        Requests_2027_Second();
                        Requests_2027_Third();
                        Requests_2027_Fourth();
                        break;
                    case "2028":
                        Requests_2028_First();
                        Requests_2028_Second();
                        Requests_2028_Third();
                        Requests_2028_Fourth();
                        break;
                    case "2029":
                        Requests_2029_First();
                        Requests_2029_Second();
                        Requests_2029_Third();
                        Requests_2029_Fourth();
                        break;
                    case "2030":
                        Requests_2030_First();
                        Requests_2030_Second();
                        Requests_2030_Third();
                        Requests_2030_Fourth();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SelectingMonth(string nameOfYear, string nameOfMonths, bool changingInformation)
        {
            try
            {
                string month = "";
                string year_ = "";

                if (changingInformation == true)
                {
                    month = nameOfMonths;
                    year_ = nameOfYear;
                }
                else
                {
                    month = nameOfMonth.Text;
                    year_ = year.Text;
                }

                switch (month)
                {
                    case "January":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[0];
                                secondCommandText = arrayOfRequestsSecond_2021[0];
                                thirdCommandText = arrayOfRequestsThird_2021[0];
                                fourthCommandText = arrayOfRequestsFourth_2021[0];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[0];
                                secondCommandText = arrayOfRequestsSecond_2022[0];
                                thirdCommandText = arrayOfRequestsThird_2022[0];
                                fourthCommandText = arrayOfRequestsFourth_2022[0];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[0];
                                secondCommandText = arrayOfRequestsSecond_2023[0];
                                thirdCommandText = arrayOfRequestsThird_2023[0];
                                fourthCommandText = arrayOfRequestsFourth_2023[0];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[0];
                                secondCommandText = arrayOfRequestsSecond_2024[0];
                                thirdCommandText = arrayOfRequestsThird_2024[0];
                                fourthCommandText = arrayOfRequestsFourth_2024[0];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[0];
                                secondCommandText = arrayOfRequestsSecond_2025[0];
                                thirdCommandText = arrayOfRequestsThird_2025[0];
                                fourthCommandText = arrayOfRequestsFourth_2025[0];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[0];
                                secondCommandText = arrayOfRequestsSecond_2026[0];
                                thirdCommandText = arrayOfRequestsThird_2026[0];
                                fourthCommandText = arrayOfRequestsFourth_2026[0];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[0];
                                secondCommandText = arrayOfRequestsSecond_2027[0];
                                thirdCommandText = arrayOfRequestsThird_2027[0];
                                fourthCommandText = arrayOfRequestsFourth_2027[0];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[0];
                                secondCommandText = arrayOfRequestsSecond_2028[0];
                                thirdCommandText = arrayOfRequestsThird_2028[0];
                                fourthCommandText = arrayOfRequestsFourth_2028[0];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[0];
                                secondCommandText = arrayOfRequestsSecond_2029[0];
                                thirdCommandText = arrayOfRequestsThird_2029[0];
                                fourthCommandText = arrayOfRequestsFourth_2029[0];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[0];
                                secondCommandText = arrayOfRequestsSecond_2030[0];
                                thirdCommandText = arrayOfRequestsThird_2030[0];
                                fourthCommandText = arrayOfRequestsFourth_2030[0];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "February":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[1];
                                secondCommandText = arrayOfRequestsSecond_2021[1];
                                thirdCommandText = arrayOfRequestsThird_2021[1];
                                fourthCommandText = arrayOfRequestsFourth_2021[1];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[1];
                                secondCommandText = arrayOfRequestsSecond_2022[1];
                                thirdCommandText = arrayOfRequestsThird_2022[1];
                                fourthCommandText = arrayOfRequestsFourth_2022[1];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[1];
                                secondCommandText = arrayOfRequestsSecond_2023[1];
                                thirdCommandText = arrayOfRequestsThird_2023[1];
                                fourthCommandText = arrayOfRequestsFourth_2023[1];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[1];
                                secondCommandText = arrayOfRequestsSecond_2024[1];
                                thirdCommandText = arrayOfRequestsThird_2024[1];
                                fourthCommandText = arrayOfRequestsFourth_2024[1];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[1];
                                secondCommandText = arrayOfRequestsSecond_2025[1];
                                thirdCommandText = arrayOfRequestsThird_2025[1];
                                fourthCommandText = arrayOfRequestsFourth_2025[1];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[1];
                                secondCommandText = arrayOfRequestsSecond_2026[1];
                                thirdCommandText = arrayOfRequestsThird_2026[1];
                                fourthCommandText = arrayOfRequestsFourth_2026[1];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[1];
                                secondCommandText = arrayOfRequestsSecond_2027[1];
                                thirdCommandText = arrayOfRequestsThird_2027[1];
                                fourthCommandText = arrayOfRequestsFourth_2027[1];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[1];
                                secondCommandText = arrayOfRequestsSecond_2028[1];
                                thirdCommandText = arrayOfRequestsThird_2028[1];
                                fourthCommandText = arrayOfRequestsFourth_2028[1];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[1];
                                secondCommandText = arrayOfRequestsSecond_2029[1];
                                thirdCommandText = arrayOfRequestsThird_2029[1];
                                fourthCommandText = arrayOfRequestsFourth_2029[1];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[1];
                                secondCommandText = arrayOfRequestsSecond_2030[1];
                                thirdCommandText = arrayOfRequestsThird_2030[1];
                                fourthCommandText = arrayOfRequestsFourth_2030[1];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "March":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[2];
                                secondCommandText = arrayOfRequestsSecond_2021[2];
                                thirdCommandText = arrayOfRequestsThird_2021[2];
                                fourthCommandText = arrayOfRequestsFourth_2021[2];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[2];
                                secondCommandText = arrayOfRequestsSecond_2022[2];
                                thirdCommandText = arrayOfRequestsThird_2022[2];
                                fourthCommandText = arrayOfRequestsFourth_2022[2];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[2];
                                secondCommandText = arrayOfRequestsSecond_2023[2];
                                thirdCommandText = arrayOfRequestsThird_2023[2];
                                fourthCommandText = arrayOfRequestsFourth_2023[2];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[2];
                                secondCommandText = arrayOfRequestsSecond_2024[2];
                                thirdCommandText = arrayOfRequestsThird_2024[2];
                                fourthCommandText = arrayOfRequestsFourth_2024[2];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[2];
                                secondCommandText = arrayOfRequestsSecond_2025[2];
                                thirdCommandText = arrayOfRequestsThird_2025[2];
                                fourthCommandText = arrayOfRequestsFourth_2025[2];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[2];
                                secondCommandText = arrayOfRequestsSecond_2026[2];
                                thirdCommandText = arrayOfRequestsThird_2026[2];
                                fourthCommandText = arrayOfRequestsFourth_2026[2];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[2];
                                secondCommandText = arrayOfRequestsSecond_2027[2];
                                thirdCommandText = arrayOfRequestsThird_2027[2];
                                fourthCommandText = arrayOfRequestsFourth_2027[2];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[2];
                                secondCommandText = arrayOfRequestsSecond_2028[2];
                                thirdCommandText = arrayOfRequestsThird_2028[2];
                                fourthCommandText = arrayOfRequestsFourth_2028[2];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[2];
                                secondCommandText = arrayOfRequestsSecond_2029[2];
                                thirdCommandText = arrayOfRequestsThird_2029[2];
                                fourthCommandText = arrayOfRequestsFourth_2029[2];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[2];
                                secondCommandText = arrayOfRequestsSecond_2030[2];
                                thirdCommandText = arrayOfRequestsThird_2030[2];
                                fourthCommandText = arrayOfRequestsFourth_2030[2];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "April":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[3];
                                secondCommandText = arrayOfRequestsSecond_2021[3];
                                thirdCommandText = arrayOfRequestsThird_2021[3];
                                fourthCommandText = arrayOfRequestsFourth_2021[3];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[3];
                                secondCommandText = arrayOfRequestsSecond_2022[3];
                                thirdCommandText = arrayOfRequestsThird_2022[3];
                                fourthCommandText = arrayOfRequestsFourth_2022[3];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[3];
                                secondCommandText = arrayOfRequestsSecond_2023[3];
                                thirdCommandText = arrayOfRequestsThird_2023[3];
                                fourthCommandText = arrayOfRequestsFourth_2023[3];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[3];
                                secondCommandText = arrayOfRequestsSecond_2024[3];
                                thirdCommandText = arrayOfRequestsThird_2024[3];
                                fourthCommandText = arrayOfRequestsFourth_2024[3];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[3];
                                secondCommandText = arrayOfRequestsSecond_2025[3];
                                thirdCommandText = arrayOfRequestsThird_2025[3];
                                fourthCommandText = arrayOfRequestsFourth_2025[3];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[3];
                                secondCommandText = arrayOfRequestsSecond_2026[3];
                                thirdCommandText = arrayOfRequestsThird_2026[3];
                                fourthCommandText = arrayOfRequestsFourth_2026[3];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[3];
                                secondCommandText = arrayOfRequestsSecond_2027[3];
                                thirdCommandText = arrayOfRequestsThird_2027[3];
                                fourthCommandText = arrayOfRequestsFourth_2027[3];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[3];
                                secondCommandText = arrayOfRequestsSecond_2028[3];
                                thirdCommandText = arrayOfRequestsThird_2028[3];
                                fourthCommandText = arrayOfRequestsFourth_2028[3];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[3];
                                secondCommandText = arrayOfRequestsSecond_2029[3];
                                thirdCommandText = arrayOfRequestsThird_2029[3];
                                fourthCommandText = arrayOfRequestsFourth_2029[3];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[3];
                                secondCommandText = arrayOfRequestsSecond_2030[3];
                                thirdCommandText = arrayOfRequestsThird_2030[3];
                                fourthCommandText = arrayOfRequestsFourth_2030[3];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "May":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[4];
                                secondCommandText = arrayOfRequestsSecond_2021[4];
                                thirdCommandText = arrayOfRequestsThird_2021[4];
                                fourthCommandText = arrayOfRequestsFourth_2021[4];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[4];
                                secondCommandText = arrayOfRequestsSecond_2022[4];
                                thirdCommandText = arrayOfRequestsThird_2022[4];
                                fourthCommandText = arrayOfRequestsFourth_2022[4];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[4];
                                secondCommandText = arrayOfRequestsSecond_2023[4];
                                thirdCommandText = arrayOfRequestsThird_2023[4];
                                fourthCommandText = arrayOfRequestsFourth_2023[4];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[4];
                                secondCommandText = arrayOfRequestsSecond_2024[4];
                                thirdCommandText = arrayOfRequestsThird_2024[4];
                                fourthCommandText = arrayOfRequestsFourth_2024[4];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[4];
                                secondCommandText = arrayOfRequestsSecond_2025[4];
                                thirdCommandText = arrayOfRequestsThird_2025[4];
                                fourthCommandText = arrayOfRequestsFourth_2025[4];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[4];
                                secondCommandText = arrayOfRequestsSecond_2026[4];
                                thirdCommandText = arrayOfRequestsThird_2026[4];
                                fourthCommandText = arrayOfRequestsFourth_2026[4];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[4];
                                secondCommandText = arrayOfRequestsSecond_2027[4];
                                thirdCommandText = arrayOfRequestsThird_2027[4];
                                fourthCommandText = arrayOfRequestsFourth_2027[4];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[4];
                                secondCommandText = arrayOfRequestsSecond_2028[4];
                                thirdCommandText = arrayOfRequestsThird_2028[4];
                                fourthCommandText = arrayOfRequestsFourth_2028[4];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[4];
                                secondCommandText = arrayOfRequestsSecond_2029[4];
                                thirdCommandText = arrayOfRequestsThird_2029[4];
                                fourthCommandText = arrayOfRequestsFourth_2029[4];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[4];
                                secondCommandText = arrayOfRequestsSecond_2030[4];
                                thirdCommandText = arrayOfRequestsThird_2030[4];
                                fourthCommandText = arrayOfRequestsFourth_2030[4];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "June":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[5];
                                secondCommandText = arrayOfRequestsSecond_2021[5];
                                thirdCommandText = arrayOfRequestsThird_2021[5];
                                fourthCommandText = arrayOfRequestsFourth_2021[5];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[5];
                                secondCommandText = arrayOfRequestsSecond_2022[5];
                                thirdCommandText = arrayOfRequestsThird_2022[5];
                                fourthCommandText = arrayOfRequestsFourth_2022[5];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[5];
                                secondCommandText = arrayOfRequestsSecond_2023[5];
                                thirdCommandText = arrayOfRequestsThird_2023[5];
                                fourthCommandText = arrayOfRequestsFourth_2023[5];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[5];
                                secondCommandText = arrayOfRequestsSecond_2024[5];
                                thirdCommandText = arrayOfRequestsThird_2024[5];
                                fourthCommandText = arrayOfRequestsFourth_2024[5];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[5];
                                secondCommandText = arrayOfRequestsSecond_2025[5];
                                thirdCommandText = arrayOfRequestsThird_2025[5];
                                fourthCommandText = arrayOfRequestsFourth_2025[5];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[5];
                                secondCommandText = arrayOfRequestsSecond_2026[5];
                                thirdCommandText = arrayOfRequestsThird_2026[5];
                                fourthCommandText = arrayOfRequestsFourth_2026[5];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[5];
                                secondCommandText = arrayOfRequestsSecond_2027[5];
                                thirdCommandText = arrayOfRequestsThird_2027[5];
                                fourthCommandText = arrayOfRequestsFourth_2027[5];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[5];
                                secondCommandText = arrayOfRequestsSecond_2028[5];
                                thirdCommandText = arrayOfRequestsThird_2028[5];
                                fourthCommandText = arrayOfRequestsFourth_2028[5];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[5];
                                secondCommandText = arrayOfRequestsSecond_2029[5];
                                thirdCommandText = arrayOfRequestsThird_2029[5];
                                fourthCommandText = arrayOfRequestsFourth_2029[5];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[5];
                                secondCommandText = arrayOfRequestsSecond_2030[5];
                                thirdCommandText = arrayOfRequestsThird_2030[5];
                                fourthCommandText = arrayOfRequestsFourth_2030[5];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "July":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[6];
                                secondCommandText = arrayOfRequestsSecond_2021[6];
                                thirdCommandText = arrayOfRequestsThird_2021[6];
                                fourthCommandText = arrayOfRequestsFourth_2021[6];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[6];
                                secondCommandText = arrayOfRequestsSecond_2022[6];
                                thirdCommandText = arrayOfRequestsThird_2022[6];
                                fourthCommandText = arrayOfRequestsFourth_2022[6];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[6];
                                secondCommandText = arrayOfRequestsSecond_2023[6];
                                thirdCommandText = arrayOfRequestsThird_2023[6];
                                fourthCommandText = arrayOfRequestsFourth_2023[6];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[6];
                                secondCommandText = arrayOfRequestsSecond_2024[6];
                                thirdCommandText = arrayOfRequestsThird_2024[6];
                                fourthCommandText = arrayOfRequestsFourth_2024[6];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[6];
                                secondCommandText = arrayOfRequestsSecond_2025[6];
                                thirdCommandText = arrayOfRequestsThird_2025[6];
                                fourthCommandText = arrayOfRequestsFourth_2025[6];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[6];
                                secondCommandText = arrayOfRequestsSecond_2026[6];
                                thirdCommandText = arrayOfRequestsThird_2026[6];
                                fourthCommandText = arrayOfRequestsFourth_2026[6];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[6];
                                secondCommandText = arrayOfRequestsSecond_2027[6];
                                thirdCommandText = arrayOfRequestsThird_2027[6];
                                fourthCommandText = arrayOfRequestsFourth_2027[6];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[6];
                                secondCommandText = arrayOfRequestsSecond_2028[6];
                                thirdCommandText = arrayOfRequestsThird_2028[6];
                                fourthCommandText = arrayOfRequestsFourth_2028[6];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[6];
                                secondCommandText = arrayOfRequestsSecond_2029[6];
                                thirdCommandText = arrayOfRequestsThird_2029[6];
                                fourthCommandText = arrayOfRequestsFourth_2029[6];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[6];
                                secondCommandText = arrayOfRequestsSecond_2030[6];
                                thirdCommandText = arrayOfRequestsThird_2030[6];
                                fourthCommandText = arrayOfRequestsFourth_2030[6];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "August":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[7];
                                secondCommandText = arrayOfRequestsSecond_2021[7];
                                thirdCommandText = arrayOfRequestsThird_2021[7];
                                fourthCommandText = arrayOfRequestsFourth_2021[7];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[7];
                                secondCommandText = arrayOfRequestsSecond_2022[7];
                                thirdCommandText = arrayOfRequestsThird_2022[7];
                                fourthCommandText = arrayOfRequestsFourth_2022[7];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[7];
                                secondCommandText = arrayOfRequestsSecond_2023[7];
                                thirdCommandText = arrayOfRequestsThird_2023[7];
                                fourthCommandText = arrayOfRequestsFourth_2023[7];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[7];
                                secondCommandText = arrayOfRequestsSecond_2024[7];
                                thirdCommandText = arrayOfRequestsThird_2024[7];
                                fourthCommandText = arrayOfRequestsFourth_2024[7];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[7];
                                secondCommandText = arrayOfRequestsSecond_2025[7];
                                thirdCommandText = arrayOfRequestsThird_2025[7];
                                fourthCommandText = arrayOfRequestsFourth_2025[7];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[7];
                                secondCommandText = arrayOfRequestsSecond_2026[7];
                                thirdCommandText = arrayOfRequestsThird_2026[7];
                                fourthCommandText = arrayOfRequestsFourth_2026[7];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[7];
                                secondCommandText = arrayOfRequestsSecond_2027[7];
                                thirdCommandText = arrayOfRequestsThird_2027[7];
                                fourthCommandText = arrayOfRequestsFourth_2027[7];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[7];
                                secondCommandText = arrayOfRequestsSecond_2028[7];
                                thirdCommandText = arrayOfRequestsThird_2028[7];
                                fourthCommandText = arrayOfRequestsFourth_2028[7];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[7];
                                secondCommandText = arrayOfRequestsSecond_2029[7];
                                thirdCommandText = arrayOfRequestsThird_2029[7];
                                fourthCommandText = arrayOfRequestsFourth_2029[7];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[7];
                                secondCommandText = arrayOfRequestsSecond_2030[7];
                                thirdCommandText = arrayOfRequestsThird_2030[7];
                                fourthCommandText = arrayOfRequestsFourth_2030[7];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "September":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[8];
                                secondCommandText = arrayOfRequestsSecond_2021[8];
                                thirdCommandText = arrayOfRequestsThird_2021[8];
                                fourthCommandText = arrayOfRequestsFourth_2021[8];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[8];
                                secondCommandText = arrayOfRequestsSecond_2022[8];
                                thirdCommandText = arrayOfRequestsThird_2022[8];
                                fourthCommandText = arrayOfRequestsFourth_2022[8];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[8];
                                secondCommandText = arrayOfRequestsSecond_2023[8];
                                thirdCommandText = arrayOfRequestsThird_2023[8];
                                fourthCommandText = arrayOfRequestsFourth_2023[8];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[8];
                                secondCommandText = arrayOfRequestsSecond_2024[8];
                                thirdCommandText = arrayOfRequestsThird_2024[8];
                                fourthCommandText = arrayOfRequestsFourth_2024[8];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[8];
                                secondCommandText = arrayOfRequestsSecond_2025[8];
                                thirdCommandText = arrayOfRequestsThird_2025[8];
                                fourthCommandText = arrayOfRequestsFourth_2025[8];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[8];
                                secondCommandText = arrayOfRequestsSecond_2026[8];
                                thirdCommandText = arrayOfRequestsThird_2026[8];
                                fourthCommandText = arrayOfRequestsFourth_2026[8];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[8];
                                secondCommandText = arrayOfRequestsSecond_2027[8];
                                thirdCommandText = arrayOfRequestsThird_2027[8];
                                fourthCommandText = arrayOfRequestsFourth_2027[8];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[8];
                                secondCommandText = arrayOfRequestsSecond_2028[8];
                                thirdCommandText = arrayOfRequestsThird_2028[8];
                                fourthCommandText = arrayOfRequestsFourth_2028[8];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[8];
                                secondCommandText = arrayOfRequestsSecond_2029[8];
                                thirdCommandText = arrayOfRequestsThird_2029[8];
                                fourthCommandText = arrayOfRequestsFourth_2029[8];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[8];
                                secondCommandText = arrayOfRequestsSecond_2030[8];
                                thirdCommandText = arrayOfRequestsThird_2030[8];
                                fourthCommandText = arrayOfRequestsFourth_2030[8];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "October":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[9];
                                secondCommandText = arrayOfRequestsSecond_2021[9];
                                thirdCommandText = arrayOfRequestsThird_2021[9];
                                fourthCommandText = arrayOfRequestsFourth_2021[9];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[9];
                                secondCommandText = arrayOfRequestsSecond_2022[9];
                                thirdCommandText = arrayOfRequestsThird_2022[9];
                                fourthCommandText = arrayOfRequestsFourth_2022[9];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[9];
                                secondCommandText = arrayOfRequestsSecond_2023[9];
                                thirdCommandText = arrayOfRequestsThird_2023[9];
                                fourthCommandText = arrayOfRequestsFourth_2023[9];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[9];
                                secondCommandText = arrayOfRequestsSecond_2024[9];
                                thirdCommandText = arrayOfRequestsThird_2024[9];
                                fourthCommandText = arrayOfRequestsFourth_2024[9];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[9];
                                secondCommandText = arrayOfRequestsSecond_2025[9];
                                thirdCommandText = arrayOfRequestsThird_2025[9];
                                fourthCommandText = arrayOfRequestsFourth_2025[9];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[9];
                                secondCommandText = arrayOfRequestsSecond_2026[9];
                                thirdCommandText = arrayOfRequestsThird_2026[9];
                                fourthCommandText = arrayOfRequestsFourth_2026[9];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[9];
                                secondCommandText = arrayOfRequestsSecond_2027[9];
                                thirdCommandText = arrayOfRequestsThird_2027[9];
                                fourthCommandText = arrayOfRequestsFourth_2027[9];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[9];
                                secondCommandText = arrayOfRequestsSecond_2028[9];
                                thirdCommandText = arrayOfRequestsThird_2028[9];
                                fourthCommandText = arrayOfRequestsFourth_2028[9];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[9];
                                secondCommandText = arrayOfRequestsSecond_2029[9];
                                thirdCommandText = arrayOfRequestsThird_2029[9];
                                fourthCommandText = arrayOfRequestsFourth_2029[9];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[9];
                                secondCommandText = arrayOfRequestsSecond_2030[9];
                                thirdCommandText = arrayOfRequestsThird_2030[9];
                                fourthCommandText = arrayOfRequestsFourth_2030[9];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "November":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[10];
                                secondCommandText = arrayOfRequestsSecond_2021[10];
                                thirdCommandText = arrayOfRequestsThird_2021[10];
                                fourthCommandText = arrayOfRequestsFourth_2021[10];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[10];
                                secondCommandText = arrayOfRequestsSecond_2022[10];
                                thirdCommandText = arrayOfRequestsThird_2022[10];
                                fourthCommandText = arrayOfRequestsFourth_2022[10];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[10];
                                secondCommandText = arrayOfRequestsSecond_2023[10];
                                thirdCommandText = arrayOfRequestsThird_2023[10];
                                fourthCommandText = arrayOfRequestsFourth_2023[10];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[10];
                                secondCommandText = arrayOfRequestsSecond_2024[10];
                                thirdCommandText = arrayOfRequestsThird_2024[10];
                                fourthCommandText = arrayOfRequestsFourth_2024[10];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[10];
                                secondCommandText = arrayOfRequestsSecond_2025[10];
                                thirdCommandText = arrayOfRequestsThird_2025[10];
                                fourthCommandText = arrayOfRequestsFourth_2025[10];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[10];
                                secondCommandText = arrayOfRequestsSecond_2026[10];
                                thirdCommandText = arrayOfRequestsThird_2026[10];
                                fourthCommandText = arrayOfRequestsFourth_2026[10];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[10];
                                secondCommandText = arrayOfRequestsSecond_2027[10];
                                thirdCommandText = arrayOfRequestsThird_2027[10];
                                fourthCommandText = arrayOfRequestsFourth_2027[10];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[10];
                                secondCommandText = arrayOfRequestsSecond_2028[10];
                                thirdCommandText = arrayOfRequestsThird_2028[10];
                                fourthCommandText = arrayOfRequestsFourth_2028[10];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[10];
                                secondCommandText = arrayOfRequestsSecond_2029[10];
                                thirdCommandText = arrayOfRequestsThird_2029[10];
                                fourthCommandText = arrayOfRequestsFourth_2029[10];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[10];
                                secondCommandText = arrayOfRequestsSecond_2030[10];
                                thirdCommandText = arrayOfRequestsThird_2030[10];
                                fourthCommandText = arrayOfRequestsFourth_2030[10];
                                break;
                            default:
                                break;
                        }
                        break;

                    case "December":

                        switch (year_)
                        {
                            case "2021":
                                firstCommandText = arrayOfRequestsFirst_2021[11];
                                secondCommandText = arrayOfRequestsSecond_2021[11];
                                thirdCommandText = arrayOfRequestsThird_2021[11];
                                fourthCommandText = arrayOfRequestsFourth_2021[11];
                                break;
                            case "2022":
                                firstCommandText = arrayOfRequestsFirst_2022[11];
                                secondCommandText = arrayOfRequestsSecond_2022[11];
                                thirdCommandText = arrayOfRequestsThird_2022[11];
                                fourthCommandText = arrayOfRequestsFourth_2022[11];
                                break;
                            case "2023":
                                firstCommandText = arrayOfRequestsFirst_2023[11];
                                secondCommandText = arrayOfRequestsSecond_2023[11];
                                thirdCommandText = arrayOfRequestsThird_2023[11];
                                fourthCommandText = arrayOfRequestsFourth_2023[11];
                                break;
                            case "2024":
                                firstCommandText = arrayOfRequestsFirst_2024[11];
                                secondCommandText = arrayOfRequestsSecond_2024[11];
                                thirdCommandText = arrayOfRequestsThird_2024[11];
                                fourthCommandText = arrayOfRequestsFourth_2024[11];
                                break;
                            case "2025":
                                firstCommandText = arrayOfRequestsFirst_2025[11];
                                secondCommandText = arrayOfRequestsSecond_2025[11];
                                thirdCommandText = arrayOfRequestsThird_2025[11];
                                fourthCommandText = arrayOfRequestsFourth_2025[11];
                                break;
                            case "2026":
                                firstCommandText = arrayOfRequestsFirst_2026[11];
                                secondCommandText = arrayOfRequestsSecond_2026[11];
                                thirdCommandText = arrayOfRequestsThird_2026[11];
                                fourthCommandText = arrayOfRequestsFourth_2026[11];
                                break;
                            case "2027":
                                firstCommandText = arrayOfRequestsFirst_2027[11];
                                secondCommandText = arrayOfRequestsSecond_2027[11];
                                thirdCommandText = arrayOfRequestsThird_2027[11];
                                fourthCommandText = arrayOfRequestsFourth_2027[11];
                                break;
                            case "2028":
                                firstCommandText = arrayOfRequestsFirst_2028[11];
                                secondCommandText = arrayOfRequestsSecond_2028[11];
                                thirdCommandText = arrayOfRequestsThird_2028[11];
                                fourthCommandText = arrayOfRequestsFourth_2028[11];
                                break;
                            case "2029":
                                firstCommandText = arrayOfRequestsFirst_2029[11];
                                secondCommandText = arrayOfRequestsSecond_2029[11];
                                thirdCommandText = arrayOfRequestsThird_2029[11];
                                fourthCommandText = arrayOfRequestsFourth_2029[11];
                                break;
                            case "2030":
                                firstCommandText = arrayOfRequestsFirst_2030[11];
                                secondCommandText = arrayOfRequestsSecond_2030[11];
                                thirdCommandText = arrayOfRequestsThird_2030[11];
                                fourthCommandText = arrayOfRequestsFourth_2030[11];
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Requests_2021_First()
        {
            arrayOfRequestsFirst_2021[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-01-01' and '2021-01-31'";/////
            arrayOfRequestsFirst_2021[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-02-01' and '2021-02-28'";
            arrayOfRequestsFirst_2021[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-03-01' and '2021-03-31'";
            arrayOfRequestsFirst_2021[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-03-30' and '2021-04-30'";///
            arrayOfRequestsFirst_2021[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-05-01' and '2021-05-31'";
            arrayOfRequestsFirst_2021[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-05-29' and '2021-06-29'";
            arrayOfRequestsFirst_2021[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-06-30' and '2021-07-31'";
            arrayOfRequestsFirst_2021[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-08-01' and '2021-08-31'";
            arrayOfRequestsFirst_2021[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-09-01' and '2021-09-29'";
            arrayOfRequestsFirst_2021[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-09-30' and '2021-10-31'";
            arrayOfRequestsFirst_2021[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-10-28' and '2021-11-28'";
            arrayOfRequestsFirst_2021[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-11-29' and '2021-12-30'";
        }

        private void Requests_2022_First()
        {
            arrayOfRequestsFirst_2022[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2021-12-31' and '2022-01-31'";
            arrayOfRequestsFirst_2022[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-01-31' and '2022-02-28'";
            arrayOfRequestsFirst_2022[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-02-28' and '2022-03-31'";
            arrayOfRequestsFirst_2022[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-04-01' and '2022-04-29'";
            arrayOfRequestsFirst_2022[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-04-30' and '2022-05-31'";
            arrayOfRequestsFirst_2022[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-05-31' and '2022-06-28'";
            arrayOfRequestsFirst_2022[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-06-29' and '2022-07-30'";
            arrayOfRequestsFirst_2022[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-07-31' and '2022-08-31'";
            arrayOfRequestsFirst_2022[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-09-01' and '2022-09-28'";
            arrayOfRequestsFirst_2022[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-09-29' and '2022-10-30'";
            arrayOfRequestsFirst_2022[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-10-27' and '2022-11-27'";
            arrayOfRequestsFirst_2022[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-11-29' and '2022-12-30'";
        }

        private void Requests_2023_First()
        {
            arrayOfRequestsFirst_2023[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2022-12-29' and '2023-01-30'";
            arrayOfRequestsFirst_2023[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-02-01' and '2023-02-28'";
            arrayOfRequestsFirst_2023[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-02-28' and '2023-03-31'";
            arrayOfRequestsFirst_2023[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-04-01' and '2023-04-28'";
            arrayOfRequestsFirst_2023[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-04-29' and '2023-05-30'";
            arrayOfRequestsFirst_2023[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-05-27' and '2023-06-27'";
            arrayOfRequestsFirst_2023[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-06-30' and '2023-07-31'";
            arrayOfRequestsFirst_2023[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-07-29' and '2023-08-30'";
            arrayOfRequestsFirst_2023[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-08-27' and '2023-09-27'";
            arrayOfRequestsFirst_2023[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-09-30' and '2023-10-31'";
            arrayOfRequestsFirst_2023[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-10-30' and '2023-11-30'";
            arrayOfRequestsFirst_2023[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2023-11-29' and '2023-12-30'";
        }

        private void Requests_2024_First()
        {
            arrayOfRequestsFirst_2024[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-01-01' and '2024-01-30'";
            arrayOfRequestsFirst_2024[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-02-01' and '2024-02-29'";
            arrayOfRequestsFirst_2024[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-02-28' and '2024-03-31'";
            arrayOfRequestsFirst_2024[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-03-29' and '2024-04-30'";
            arrayOfRequestsFirst_2024[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-04-29' and '2024-05-30'";
            arrayOfRequestsFirst_2024[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-05-27' and '2024-06-27'";
            arrayOfRequestsFirst_2024[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-06-30' and '2024-07-31'";
            arrayOfRequestsFirst_2024[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-07-29' and '2024-08-30'";
            arrayOfRequestsFirst_2024[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-08-27' and '2024-09-27'";
            arrayOfRequestsFirst_2024[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-09-30' and '2024-10-31'";
            arrayOfRequestsFirst_2024[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-10-30' and '2024-11-28'";
            arrayOfRequestsFirst_2024[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-11-29' and '2024-12-30'";
        }

        private void Requests_2025_First()
        {
            arrayOfRequestsFirst_2025[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2024-12-31' and '2025-01-30'";
            arrayOfRequestsFirst_2025[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-01-30' and '2025-02-28'";
            arrayOfRequestsFirst_2025[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-02-28' and '2025-03-31'";
            arrayOfRequestsFirst_2025[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-03-29' and '2025-04-29'";
            arrayOfRequestsFirst_2025[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-04-29' and '2025-05-30'";
            arrayOfRequestsFirst_2025[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-06-01' and '2025-06-28'";
            arrayOfRequestsFirst_2025[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-06-28' and '2025-07-30'";
            arrayOfRequestsFirst_2025[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-07-29' and '2025-08-30'";
            arrayOfRequestsFirst_2025[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-09-01' and '2025-09-28'";
            arrayOfRequestsFirst_2025[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-09-28' and '2025-10-30'";
            arrayOfRequestsFirst_2025[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-10-30' and '2025-11-30'";
            arrayOfRequestsFirst_2025[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-11-29' and '2025-12-30'";
        }

        private void Requests_2026_First()
        {
            arrayOfRequestsFirst_2026[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2025-12-29' and '2026-01-30'";
            arrayOfRequestsFirst_2026[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-01-30' and '2026-02-28'";
            arrayOfRequestsFirst_2026[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-02-28' and '2026-03-31'";
            arrayOfRequestsFirst_2026[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-04-01' and '2026-04-28'";
            arrayOfRequestsFirst_2026[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-04-29' and '2026-05-30'";
            arrayOfRequestsFirst_2026[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-05-29' and '2026-06-28'";
            arrayOfRequestsFirst_2026[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-07-02' and '2026-07-31'";
            arrayOfRequestsFirst_2026[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-07-29' and '2026-08-30'";
            arrayOfRequestsFirst_2026[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-08-29' and '2026-09-30'";
            arrayOfRequestsFirst_2026[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-10-02' and '2026-10-31'";
            arrayOfRequestsFirst_2026[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-10-30' and '2026-11-27'";
            arrayOfRequestsFirst_2026[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2026-11-29' and '2026-12-30'";
        }

        private void Requests_2027_First()
        {
            arrayOfRequestsFirst_2027[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-01-01' and '2027-01-30'";
            arrayOfRequestsFirst_2027[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-01-29' and '2027-02-28'";
            arrayOfRequestsFirst_2027[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-02-27' and '2027-03-30'";
            arrayOfRequestsFirst_2027[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-03-29' and '2027-04-28'";
            arrayOfRequestsFirst_2027[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-04-29' and '2027-05-30'";
            arrayOfRequestsFirst_2027[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-05-29' and '2027-06-30'";
            arrayOfRequestsFirst_2027[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-07-01' and '2027-07-31'";
            arrayOfRequestsFirst_2027[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-08-02' and '2027-08-30'";
            arrayOfRequestsFirst_2027[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-08-29' and '2027-09-30'";
            arrayOfRequestsFirst_2027[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-10-01' and '2027-10-31'";
            arrayOfRequestsFirst_2027[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-11-01' and '2027-11-29'";
            arrayOfRequestsFirst_2027[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2027-11-29' and '2027-12-30'";
        }

        private void Requests_2028_First()
        {
            arrayOfRequestsFirst_2028[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-01-01' and '2028-01-30'";
            arrayOfRequestsFirst_2028[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-02-01' and '2028-02-26'";
            arrayOfRequestsFirst_2028[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-02-27' and '2028-03-30'";
            arrayOfRequestsFirst_2028[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-03-29' and '2028-04-28'";
            arrayOfRequestsFirst_2028[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-04-29' and '2028-05-30'";
            arrayOfRequestsFirst_2028[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-05-29' and '2028-06-30'";
            arrayOfRequestsFirst_2028[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-07-01' and '2028-07-31'";
            arrayOfRequestsFirst_2028[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-07-29' and '2028-08-31'";
            arrayOfRequestsFirst_2028[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-08-29' and '2028-09-30'";
            arrayOfRequestsFirst_2028[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-10-01' and '2028-10-31'";
            arrayOfRequestsFirst_2028[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-10-29' and '2028-11-30'";
            arrayOfRequestsFirst_2028[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-11-29' and '2028-12-30'";
        }

        private void Requests_2029_First()
        {
            arrayOfRequestsFirst_2029[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2028-12-29' and '2029-01-30'";
            arrayOfRequestsFirst_2029[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-02-01' and '2029-02-28'";
            arrayOfRequestsFirst_2029[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-02-27' and '2029-03-30'";
            arrayOfRequestsFirst_2029[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-03-29' and '2029-04-28'";
            arrayOfRequestsFirst_2029[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-04-29' and '2029-05-30'";
            arrayOfRequestsFirst_2029[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-05-29' and '2029-06-28'";
            arrayOfRequestsFirst_2029[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-07-01' and '2029-07-31'";
            arrayOfRequestsFirst_2029[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-07-29' and '2029-08-31'";
            arrayOfRequestsFirst_2029[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-08-29' and '2029-09-30'";
            arrayOfRequestsFirst_2029[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-10-01' and '2029-10-30'";
            arrayOfRequestsFirst_2029[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-10-29' and '2029-11-30'";
            arrayOfRequestsFirst_2029[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-11-29' and '2029-12-30'";
        }

        private void Requests_2030_First()
        {
            arrayOfRequestsFirst_2030[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2029-12-29' and '2030-01-30'";
            arrayOfRequestsFirst_2030[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-02-01' and '2030-02-28'";
            arrayOfRequestsFirst_2030[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-02-27' and '2030-03-30'";
            arrayOfRequestsFirst_2030[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-03-29' and '2030-04-28'";
            arrayOfRequestsFirst_2030[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-04-29' and '2030-05-30'";
            arrayOfRequestsFirst_2030[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-05-29' and '2030-06-28'";
            arrayOfRequestsFirst_2030[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-07-01' and '2030-07-31'";
            arrayOfRequestsFirst_2030[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-07-29' and '2030-08-31'";
            arrayOfRequestsFirst_2030[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-08-29' and '2030-09-30'";
            arrayOfRequestsFirst_2030[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-10-01' and '2030-10-30'";
            arrayOfRequestsFirst_2030[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-11-02' and '2030-11-30'";
            arrayOfRequestsFirst_2030[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM First_Shift WHERE Day_Shift between '2030-11-29' and '2030-12-30'";
        }

        private void Requests_2021_Second()
        {
            arrayOfRequestsSecond_2021[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-01-01' and '2021-01-31'";///
            arrayOfRequestsSecond_2021[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-02-01' and '2021-02-28'";
            arrayOfRequestsSecond_2021[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-02-27' and '2021-03-31'";
            arrayOfRequestsSecond_2021[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-03-27' and '2021-04-27'";
            arrayOfRequestsSecond_2021[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-05-01' and '2021-05-31'";
            arrayOfRequestsSecond_2021[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-05-29' and '2021-06-29'";
            arrayOfRequestsSecond_2021[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-07-01' and '2021-07-31'";
            arrayOfRequestsSecond_2021[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-08-01' and '2021-08-31'";
            arrayOfRequestsSecond_2021[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-08-27' and '2021-09-30'";
            arrayOfRequestsSecond_2021[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-09-30' and '2021-10-31'";
            arrayOfRequestsSecond_2021[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-10-28' and '2021-11-28'";
            arrayOfRequestsSecond_2021[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2021-11-29' and '2021-12-31'";
        }

        private void Requests_2022_Second()
        {
            arrayOfRequestsSecond_2022[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-01-01' and '2022-02-01'";
            arrayOfRequestsSecond_2022[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-01-31' and '2022-03-01'";
            arrayOfRequestsSecond_2022[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-02-28' and '2022-03-31'";
            arrayOfRequestsSecond_2022[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-03-29' and '2022-04-30'";
            arrayOfRequestsSecond_2022[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-05-01' and '2022-06-01'";
            arrayOfRequestsSecond_2022[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-05-31' and '2022-06-29'";
            arrayOfRequestsSecond_2022[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-06-30' and '2022-07-31'";
            arrayOfRequestsSecond_2022[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-08-01' and '2022-09-01'";
            arrayOfRequestsSecond_2022[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-08-29' and '2022-09-29'";
            arrayOfRequestsSecond_2022[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-09-30' and '2022-10-31'";
            arrayOfRequestsSecond_2022[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-10-28' and '2022-11-28'";
            arrayOfRequestsSecond_2022[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-11-29' and '2022-12-30'";
        }

        private void Requests_2023_Second()
        {
            arrayOfRequestsSecond_2023[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2022-12-29' and '2023-01-31'";
            arrayOfRequestsSecond_2023[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-02-01' and '2023-02-28'";
            arrayOfRequestsSecond_2023[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-02-28' and '2023-03-31'";
            arrayOfRequestsSecond_2023[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-04-01' and '2023-04-29'";
            arrayOfRequestsSecond_2023[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-04-29' and '2023-05-31'";
            arrayOfRequestsSecond_2023[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-05-31' and '2023-06-28'";
            arrayOfRequestsSecond_2023[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-06-29' and '2023-07-30'";
            arrayOfRequestsSecond_2023[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-07-31' and '2023-08-31'";
            arrayOfRequestsSecond_2023[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-08-31' and '2023-09-28'";
            arrayOfRequestsSecond_2023[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-09-29' and '2023-10-30'";
            arrayOfRequestsSecond_2023[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-10-29' and '2023-11-30'";
            arrayOfRequestsSecond_2023[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-11-29' and '2023-12-30'";
        }

        private void Requests_2024_Second()
        {
            arrayOfRequestsSecond_2024[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2023-12-29' and '2024-01-30'";
            arrayOfRequestsSecond_2024[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-01-30' and '2024-02-27'";
            arrayOfRequestsSecond_2024[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-02-28' and '2024-03-31'";
            arrayOfRequestsSecond_2024[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-03-29' and '2024-04-30'";
            arrayOfRequestsSecond_2024[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-04-29' and '2024-05-31'";
            arrayOfRequestsSecond_2024[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-05-29' and '2024-06-29'";
            arrayOfRequestsSecond_2024[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-06-29' and '2024-07-30'";
            arrayOfRequestsSecond_2024[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-07-31' and '2024-08-31'";
            arrayOfRequestsSecond_2024[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-08-29' and '2024-09-28'";
            arrayOfRequestsSecond_2024[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-09-29' and '2024-10-30'";
            arrayOfRequestsSecond_2024[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-11-01' and '2024-11-29'";
            arrayOfRequestsSecond_2024[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2024-11-29' and '2024-12-30'";
        }

        private void Requests_2025_Second()
        {
            arrayOfRequestsSecond_2025[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-01-01' and '2025-01-30'";
            arrayOfRequestsSecond_2025[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-01-30' and '2025-02-28'";
            arrayOfRequestsSecond_2025[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-02-28' and '2025-03-31'";
            arrayOfRequestsSecond_2025[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-03-29' and '2025-04-27'";
            arrayOfRequestsSecond_2025[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-04-29' and '2025-05-31'";
            arrayOfRequestsSecond_2025[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-06-02' and '2025-06-30'";
            arrayOfRequestsSecond_2025[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-06-29' and '2025-07-30'";
            arrayOfRequestsSecond_2025[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-07-31' and '2025-08-31'";
            arrayOfRequestsSecond_2025[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-08-29' and '2025-09-28'";
            arrayOfRequestsSecond_2025[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-09-29' and '2025-10-30'";
            arrayOfRequestsSecond_2025[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-11-01' and '2025-11-28'";
            arrayOfRequestsSecond_2025[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-11-29' and '2025-12-30'";
        }

        private void Requests_2026_Second()
        {
            arrayOfRequestsSecond_2026[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2025-12-29' and '2026-01-30'";
            arrayOfRequestsSecond_2026[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-01-30' and '2026-02-28'";
            arrayOfRequestsSecond_2026[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-02-28' and '2026-03-31'";
            arrayOfRequestsSecond_2026[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-03-29' and '2026-04-27'";
            arrayOfRequestsSecond_2026[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-04-29' and '2026-05-31'";
            arrayOfRequestsSecond_2026[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-05-29' and '2026-06-28'";
            arrayOfRequestsSecond_2026[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-06-29' and '2026-07-30'";
            arrayOfRequestsSecond_2026[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-07-31' and '2026-08-31'";
            arrayOfRequestsSecond_2026[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-08-29' and '2026-09-28'";
            arrayOfRequestsSecond_2026[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-09-29' and '2026-10-30'";
            arrayOfRequestsSecond_2026[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-10-29' and '2026-11-28'";
            arrayOfRequestsSecond_2026[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-11-29' and '2026-12-30'";
        }

        private void Requests_2027_Second()
        {
            arrayOfRequestsSecond_2027[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2026-12-29' and '2027-01-30'";
            arrayOfRequestsSecond_2027[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-01-30' and '2027-02-27'";
            arrayOfRequestsSecond_2027[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-02-28' and '2027-03-31'";
            arrayOfRequestsSecond_2027[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-04-01' and '2027-04-29'";
            arrayOfRequestsSecond_2027[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-04-29' and '2027-05-31'";
            arrayOfRequestsSecond_2027[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-05-29' and '2027-06-30'";
            arrayOfRequestsSecond_2027[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-06-29' and '2027-07-30'";
            arrayOfRequestsSecond_2027[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-07-31' and '2027-08-31'";
            arrayOfRequestsSecond_2027[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-08-29' and '2027-09-28'";
            arrayOfRequestsSecond_2027[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-09-29' and '2027-10-30'";
            arrayOfRequestsSecond_2027[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-10-29' and '2027-11-30'";
            arrayOfRequestsSecond_2027[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2027-11-29' and '2027-12-30'";
        }

        private void Requests_2028_Second()
        {
            arrayOfRequestsSecond_2028[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-01-01' and '2028-01-30'";
            arrayOfRequestsSecond_2028[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-02-03' and '2028-02-29'";
            arrayOfRequestsSecond_2028[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-02-28' and '2028-03-31'";
            arrayOfRequestsSecond_2028[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-04-01' and '2028-04-29'";
            arrayOfRequestsSecond_2028[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-04-29' and '2028-05-31'";
            arrayOfRequestsSecond_2028[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-05-29' and '2028-06-28'";
            arrayOfRequestsSecond_2028[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-06-29' and '2028-07-30'";
            arrayOfRequestsSecond_2028[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-07-29' and '2028-08-31'";
            arrayOfRequestsSecond_2028[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-08-29' and '2028-09-28'";
            arrayOfRequestsSecond_2028[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-09-29' and '2028-10-30'";
            arrayOfRequestsSecond_2028[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-10-29' and '2028-11-30'";
            arrayOfRequestsSecond_2028[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-11-29' and '2028-12-30'";
        }

        private void Requests_2029_Second()
        {
            arrayOfRequestsSecond_2029[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2028-12-29' and '2029-01-30'";
            arrayOfRequestsSecond_2029[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-02-01' and '2029-02-28'";
            arrayOfRequestsSecond_2029[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-02-28' and '2029-03-31'";
            arrayOfRequestsSecond_2029[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-04-01' and '2029-04-29'";
            arrayOfRequestsSecond_2029[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-04-29' and '2029-05-31'";
            arrayOfRequestsSecond_2029[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-05-29' and '2029-06-30'";
            arrayOfRequestsSecond_2029[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-06-29' and '2029-07-30'";
            arrayOfRequestsSecond_2029[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-07-31' and '2029-08-31'";
            arrayOfRequestsSecond_2029[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-08-29' and '2029-09-28'";
            arrayOfRequestsSecond_2029[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-09-29' and '2029-10-30'";
            arrayOfRequestsSecond_2029[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-10-29' and '2029-11-30'";
            arrayOfRequestsSecond_2029[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-11-29' and '2029-12-30'";
        }

        private void Requests_2030_Second()
        {
            arrayOfRequestsSecond_2030[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2029-12-29' and '2030-01-30'";
            arrayOfRequestsSecond_2030[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-02-01' and '2030-02-28'";
            arrayOfRequestsSecond_2030[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-02-28' and '2030-03-31'";
            arrayOfRequestsSecond_2030[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-04-01' and '2030-04-29'";
            arrayOfRequestsSecond_2030[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-04-29' and '2030-05-31'";
            arrayOfRequestsSecond_2030[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-05-29' and '2030-06-30'";
            arrayOfRequestsSecond_2030[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-06-29' and '2030-07-30'";
            arrayOfRequestsSecond_2030[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-07-31' and '2030-08-31'";
            arrayOfRequestsSecond_2030[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-08-29' and '2030-09-28'";
            arrayOfRequestsSecond_2030[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-09-29' and '2030-10-30'";
            arrayOfRequestsSecond_2030[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-10-29' and '2030-11-30'";
            arrayOfRequestsSecond_2030[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Second_Shift WHERE Day_Shift between '2030-11-29' and '2030-12-30'";
        }

        private void Requests_2021_Third()
        {
            arrayOfRequestsThird_2021[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-01-01' and '2021-01-31'";
            arrayOfRequestsThird_2021[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-02-01' and '2021-02-28'";
            arrayOfRequestsThird_2021[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-02-28' and '2021-03-31'";
            arrayOfRequestsThird_2021[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-03-27' and '2021-04-27'";
            arrayOfRequestsThird_2021[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-05-01' and '2021-05-31'";
            arrayOfRequestsThird_2021[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-05-31' and '2021-06-29'";
            arrayOfRequestsThird_2021[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-07-01' and '2021-07-31'";
            arrayOfRequestsThird_2021[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-08-01' and '2021-08-31'";
            arrayOfRequestsThird_2021[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-08-31' and '2021-09-27'";
            arrayOfRequestsThird_2021[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-09-30' and '2021-10-31'";
            arrayOfRequestsThird_2021[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-11-01' and '2021-11-30'";
            arrayOfRequestsThird_2021[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2021-12-01' and '2021-12-31'";
        }

        private void Requests_2022_Third()
        {
            arrayOfRequestsThird_2022[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-01-01' and '2022-02-01'";
            arrayOfRequestsThird_2022[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-02-01' and '2022-02-28'";
            arrayOfRequestsThird_2022[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-03-01' and '2022-03-28'";
            arrayOfRequestsThird_2022[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-03-28' and '2022-04-30'";
            arrayOfRequestsThird_2022[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-05-01' and '2022-06-01'";
            arrayOfRequestsThird_2022[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-05-29' and '2022-06-29'";
            arrayOfRequestsThird_2022[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-06-30' and '2022-07-31'";
            arrayOfRequestsThird_2022[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-08-01' and '2022-09-01'";
            arrayOfRequestsThird_2022[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-08-29' and '2022-09-30'";
            arrayOfRequestsThird_2022[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-09-30' and '2022-10-31'";
            arrayOfRequestsThird_2022[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-10-29' and '2022-11-29'";
            arrayOfRequestsThird_2022[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-11-29' and '2022-12-30'";
        }

        private void Requests_2023_Third()
        {
            arrayOfRequestsThird_2023[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2022-12-29' and '2023-01-31'";
            arrayOfRequestsThird_2023[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-02-01' and '2023-02-28'";
            arrayOfRequestsThird_2023[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-02-28' and '2023-03-31'";
            arrayOfRequestsThird_2023[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-03-29' and '2023-04-29'";
            arrayOfRequestsThird_2023[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-04-29' and '2023-05-31'";
            arrayOfRequestsThird_2023[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-05-31' and '2023-06-29'";
            arrayOfRequestsThird_2023[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-06-29' and '2023-07-30'";
            arrayOfRequestsThird_2023[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-07-31' and '2023-08-31'";
            arrayOfRequestsThird_2023[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-08-31' and '2023-09-28'";
            arrayOfRequestsThird_2023[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-09-29' and '2023-10-30'";
            arrayOfRequestsThird_2023[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-10-29' and '2023-11-30'";
            arrayOfRequestsThird_2023[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-11-29' and '2023-12-30'";
        }

        private void Requests_2024_Third()
        {
            arrayOfRequestsThird_2024[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2023-12-29' and '2024-01-31'";
            arrayOfRequestsThird_2024[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-02-01' and '2024-02-29'";
            arrayOfRequestsThird_2024[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-02-28' and '2024-03-31'";
            arrayOfRequestsThird_2024[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-03-29' and '2024-04-29'";
            arrayOfRequestsThird_2024[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-04-29' and '2024-05-30'";
            arrayOfRequestsThird_2024[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-05-31' and '2024-06-29'";
            arrayOfRequestsThird_2024[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-06-29' and '2024-07-30'";
            arrayOfRequestsThird_2024[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-08-01' and '2024-08-30'";
            arrayOfRequestsThird_2024[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-08-31' and '2024-09-28'";
            arrayOfRequestsThird_2024[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-09-29' and '2024-10-30'";
            arrayOfRequestsThird_2024[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-10-29' and '2024-11-30'";
            arrayOfRequestsThird_2024[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2024-11-29' and '2024-12-30'";
        }

        private void Requests_2025_Third()
        {
            arrayOfRequestsThird_2025[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-01-01' and '2025-01-31'";
            arrayOfRequestsThird_2025[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-02-01' and '2025-02-28'";
            arrayOfRequestsThird_2025[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-02-28' and '2025-03-31'";
            arrayOfRequestsThird_2025[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-03-29' and '2025-04-29'";
            arrayOfRequestsThird_2025[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-04-29' and '2025-05-30'";
            arrayOfRequestsThird_2025[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-05-31' and '2025-06-29'";
            arrayOfRequestsThird_2025[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-06-29' and '2025-07-30'";
            arrayOfRequestsThird_2025[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-08-01' and '2025-08-30'";
            arrayOfRequestsThird_2025[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-08-31' and '2025-09-28'";
            arrayOfRequestsThird_2025[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-09-29' and '2025-10-30'";
            arrayOfRequestsThird_2025[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-10-29' and '2025-11-30'";
            arrayOfRequestsThird_2025[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2025-11-29' and '2025-12-30'";
        }

        private void Requests_2026_Third()
        {
            arrayOfRequestsThird_2026[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-01-01' and '2026-01-31'";
            arrayOfRequestsThird_2026[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-02-01' and '2026-02-28'";
            arrayOfRequestsThird_2026[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-02-28' and '2026-03-31'";
            arrayOfRequestsThird_2026[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-03-29' and '2026-04-29'";
            arrayOfRequestsThird_2026[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-04-29' and '2026-05-30'";
            arrayOfRequestsThird_2026[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-05-31' and '2026-06-29'";
            arrayOfRequestsThird_2026[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-06-29' and '2026-07-30'";
            arrayOfRequestsThird_2026[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-08-01' and '2026-08-30'";
            arrayOfRequestsThird_2026[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-08-31' and '2026-09-28'";
            arrayOfRequestsThird_2026[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-09-29' and '2026-10-30'";
            arrayOfRequestsThird_2026[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-10-29' and '2026-11-30'";
            arrayOfRequestsThird_2026[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-11-29' and '2026-12-30'";
        }

        private void Requests_2027_Third()
        {
            arrayOfRequestsThird_2027[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2026-12-29' and '2027-01-31'";
            arrayOfRequestsThird_2027[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-02-01' and '2027-02-28'";
            arrayOfRequestsThird_2027[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-02-28' and '2027-03-31'";
            arrayOfRequestsThird_2027[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-03-29' and '2027-04-29'";
            arrayOfRequestsThird_2027[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-04-29' and '2027-05-30'";
            arrayOfRequestsThird_2027[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-05-31' and '2027-06-29'";
            arrayOfRequestsThird_2027[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-06-29' and '2027-07-30'";
            arrayOfRequestsThird_2027[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-07-29' and '2027-08-30'";
            arrayOfRequestsThird_2027[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-08-31' and '2027-09-28'";
            arrayOfRequestsThird_2027[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-09-29' and '2027-10-30'";
            arrayOfRequestsThird_2027[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-10-29' and '2027-11-28'";
            arrayOfRequestsThird_2027[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-11-29' and '2027-12-30'";
        }

        private void Requests_2028_Third()
        {
            arrayOfRequestsThird_2028[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2027-12-29' and '2028-01-30'";
            arrayOfRequestsThird_2028[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-02-01' and '2028-02-29'";
            arrayOfRequestsThird_2028[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-02-28' and '2028-03-31'";
            arrayOfRequestsThird_2028[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-03-29' and '2028-04-29'";
            arrayOfRequestsThird_2028[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-04-29' and '2028-05-30'";
            arrayOfRequestsThird_2028[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-05-31' and '2028-06-29'";
            arrayOfRequestsThird_2028[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-06-29' and '2028-07-30'";
            arrayOfRequestsThird_2028[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-08-02' and '2028-08-30'";
            arrayOfRequestsThird_2028[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-08-31' and '2028-09-28'";
            arrayOfRequestsThird_2028[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-09-29' and '2028-10-30'";
            arrayOfRequestsThird_2028[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-10-29' and '2028-11-28'";
            arrayOfRequestsThird_2028[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-11-29' and '2028-12-30'";
        }

        private void Requests_2029_Third()
        {
            arrayOfRequestsThird_2029[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2028-12-29' and '2029-01-30'";
            arrayOfRequestsThird_2029[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-02-01' and '2029-02-28'";
            arrayOfRequestsThird_2029[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-02-28' and '2029-03-31'";
            arrayOfRequestsThird_2029[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-03-29' and '2029-04-29'";
            arrayOfRequestsThird_2029[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-04-29' and '2029-05-30'";
            arrayOfRequestsThird_2029[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-05-31' and '2029-06-29'";
            arrayOfRequestsThird_2029[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-06-29' and '2029-07-30'";
            arrayOfRequestsThird_2029[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-08-01' and '2029-08-30'";
            arrayOfRequestsThird_2029[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-08-31' and '2029-09-28'";
            arrayOfRequestsThird_2029[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-09-29' and '2029-10-30'";
            arrayOfRequestsThird_2029[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-10-29' and '2029-11-28'";
            arrayOfRequestsThird_2029[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-12-01' and '2030-01-03'";
        }

        private void Requests_2030_Third()
        {
            arrayOfRequestsThird_2030[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2029-12-29' and '2030-01-30'";
            arrayOfRequestsThird_2030[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-02-01' and '2030-02-28'";
            arrayOfRequestsThird_2030[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-02-28' and '2030-03-31'";
            arrayOfRequestsThird_2030[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-03-29' and '2030-04-29'";
            arrayOfRequestsThird_2030[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-04-29' and '2030-05-30'";
            arrayOfRequestsThird_2030[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-05-31' and '2030-06-29'";
            arrayOfRequestsThird_2030[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-06-29' and '2030-07-30'";
            arrayOfRequestsThird_2030[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-07-29' and '2030-08-30'";
            arrayOfRequestsThird_2030[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-08-31' and '2030-09-28'";
            arrayOfRequestsThird_2030[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-09-29' and '2030-10-30'";
            arrayOfRequestsThird_2030[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-10-29' and '2030-11-28'";
            arrayOfRequestsThird_2030[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Third_Shift WHERE Day_Shift between '2030-11-29' and '2030-12-30'";
        }

        private void Requests_2021_Fourth()
        {
            arrayOfRequestsFourth_2021[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-01-01' and '2021-02-01'";///
            arrayOfRequestsFourth_2021[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-02-02' and '2021-03-01'";
            arrayOfRequestsFourth_2021[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-03-01' and '2021-04-02'";
            arrayOfRequestsFourth_2021[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-04-01' and '2021-04-30'";
            arrayOfRequestsFourth_2021[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-05-01' and '2021-06-01'";
            arrayOfRequestsFourth_2021[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-06-01' and '2021-06-30'";
            arrayOfRequestsFourth_2021[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-07-01' and '2021-07-31'";
            arrayOfRequestsFourth_2021[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-08-01' and '2021-09-01'";
            arrayOfRequestsFourth_2021[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-09-01' and '2021-09-30'";
            arrayOfRequestsFourth_2021[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-09-30' and '2021-10-31'";
            arrayOfRequestsFourth_2021[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-11-01' and '2021-12-02'";
            arrayOfRequestsFourth_2021[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2021-12-01' and '2021-12-31'";
        }

        private void Requests_2022_Fourth()
        {
            arrayOfRequestsFourth_2022[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-01-01' and '2022-02-01'";
            arrayOfRequestsFourth_2022[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-02-01' and '2022-02-28'";
            arrayOfRequestsFourth_2022[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-03-01' and '2022-04-01'";
            arrayOfRequestsFourth_2022[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-04-01' and '2022-04-30'";
            arrayOfRequestsFourth_2022[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-05-01' and '2022-06-01'";
            arrayOfRequestsFourth_2022[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-05-28' and '2022-06-30'";
            arrayOfRequestsFourth_2022[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-06-30' and '2022-07-31'";
            arrayOfRequestsFourth_2022[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-08-01' and '2022-09-01'";
            arrayOfRequestsFourth_2022[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-09-01' and '2022-09-30'";
            arrayOfRequestsFourth_2022[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-09-30' and '2022-10-31'";
            arrayOfRequestsFourth_2022[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-11-01' and '2022-12-02'";
            arrayOfRequestsFourth_2022[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2022-12-01' and '2022-12-31'";
        }

        private void Requests_2023_Fourth()
        {
            arrayOfRequestsFourth_2023[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-01-01' and '2023-01-31'";
            arrayOfRequestsFourth_2023[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-02-01' and '2023-02-28'";
            arrayOfRequestsFourth_2023[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-02-28' and '2023-03-31'";
            arrayOfRequestsFourth_2023[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-03-29' and '2023-04-29'";
            arrayOfRequestsFourth_2023[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-04-29' and '2023-05-31'";
            arrayOfRequestsFourth_2023[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-06-01' and '2023-07-01'";
            arrayOfRequestsFourth_2023[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-06-29' and '2023-07-30'";
            arrayOfRequestsFourth_2023[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-07-31' and '2023-08-31'";
            arrayOfRequestsFourth_2023[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-09-01' and '2023-10-01'";
            arrayOfRequestsFourth_2023[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-09-29' and '2023-10-30'";
            arrayOfRequestsFourth_2023[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-11-01' and '2023-11-30'";
            arrayOfRequestsFourth_2023[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2023-11-29' and '2023-12-30'";
        }

        private void Requests_2024_Fourth()
        {
            arrayOfRequestsFourth_2024[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-01-01' and '2024-01-31'";
            arrayOfRequestsFourth_2024[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-02-01' and '2024-03-01'";
            arrayOfRequestsFourth_2024[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-02-28' and '2024-03-31'";
            arrayOfRequestsFourth_2024[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-04-01' and '2024-04-30'";
            arrayOfRequestsFourth_2024[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-05-01' and '2024-06-01'";
            arrayOfRequestsFourth_2024[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-06-01' and '2024-07-01'";
            arrayOfRequestsFourth_2024[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-07-01' and '2024-07-31'";
            arrayOfRequestsFourth_2024[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-08-01' and '2024-09-01'";
            arrayOfRequestsFourth_2024[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-09-02' and '2024-10-01'";
            arrayOfRequestsFourth_2024[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-10-01' and '2024-11-01'";
            arrayOfRequestsFourth_2024[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-11-01' and '2024-12-03'";
            arrayOfRequestsFourth_2024[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2024-11-29' and '2024-12-30'";
        }

        private void Requests_2025_Fourth()
        {
            arrayOfRequestsFourth_2025[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-01-01' and '2025-01-31'";
            arrayOfRequestsFourth_2025[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-02-01' and '2025-03-01'";
            arrayOfRequestsFourth_2025[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-03-01' and '2025-04-01'";
            arrayOfRequestsFourth_2025[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-04-01' and '2025-04-30'";
            arrayOfRequestsFourth_2025[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-05-01' and '2025-06-01'";
            arrayOfRequestsFourth_2025[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-06-01' and '2025-07-01'";
            arrayOfRequestsFourth_2025[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-07-01' and '2025-07-31'";
            arrayOfRequestsFourth_2025[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-08-01' and '2025-09-01'";
            arrayOfRequestsFourth_2025[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-09-02' and '2025-10-01'";
            arrayOfRequestsFourth_2025[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-10-01' and '2025-11-01'";
            arrayOfRequestsFourth_2025[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-11-01' and '2025-12-03'";
            arrayOfRequestsFourth_2025[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2025-12-02' and '2026-01-02'";
        }

        private void Requests_2026_Fourth()
        {
            arrayOfRequestsFourth_2026[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-01-01' and '2026-01-31'";
            arrayOfRequestsFourth_2026[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-02-01' and '2026-02-28'";
            arrayOfRequestsFourth_2026[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-03-01' and '2026-04-01'";
            arrayOfRequestsFourth_2026[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-04-01' and '2026-05-02'";
            arrayOfRequestsFourth_2026[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-05-01' and '2026-06-01'";
            arrayOfRequestsFourth_2026[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-06-01' and '2026-07-01'";
            arrayOfRequestsFourth_2026[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-07-01' and '2026-07-31'";
            arrayOfRequestsFourth_2026[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-08-01' and '2026-09-01'";
            arrayOfRequestsFourth_2026[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-09-02' and '2026-10-01'";
            arrayOfRequestsFourth_2026[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-10-01' and '2026-11-01'";
            arrayOfRequestsFourth_2026[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-11-01' and '2026-12-03'";
            arrayOfRequestsFourth_2026[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2026-12-01' and '2027-01-02'";
        }

        private void Requests_2027_Fourth()
        {
            arrayOfRequestsFourth_2027[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-01-02' and '2027-02-02'";
            arrayOfRequestsFourth_2027[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-02-01' and '2027-03-02'";
            arrayOfRequestsFourth_2027[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-03-01' and '2027-04-01'";
            arrayOfRequestsFourth_2027[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-04-01' and '2027-05-02'";
            arrayOfRequestsFourth_2027[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-05-01' and '2027-06-01'";
            arrayOfRequestsFourth_2027[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-06-01' and '2027-07-01'";
            arrayOfRequestsFourth_2027[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-07-01' and '2027-08-02'";
            arrayOfRequestsFourth_2027[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-08-01' and '2027-09-01'";
            arrayOfRequestsFourth_2027[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-09-02' and '2027-10-01'";
            arrayOfRequestsFourth_2027[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-10-01' and '2027-11-01'";
            arrayOfRequestsFourth_2027[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-11-01' and '2027-12-03'";
            arrayOfRequestsFourth_2027[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2027-12-01' and '2027-12-31'";
        }

        private void Requests_2028_Fourth()
        {
            arrayOfRequestsFourth_2028[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-01-02' and '2028-02-02'";
            arrayOfRequestsFourth_2028[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-02-01' and '2028-03-02'";
            arrayOfRequestsFourth_2028[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-03-01' and '2028-04-01'";
            arrayOfRequestsFourth_2028[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-04-01' and '2028-05-02'";
            arrayOfRequestsFourth_2028[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-05-01' and '2028-06-01'";
            arrayOfRequestsFourth_2028[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-06-01' and '2028-07-03'";
            arrayOfRequestsFourth_2028[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-07-01' and '2028-08-02'";
            arrayOfRequestsFourth_2028[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-08-01' and '2028-09-01'";
            arrayOfRequestsFourth_2028[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-09-02' and '2028-10-03'";
            arrayOfRequestsFourth_2028[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-10-01' and '2028-11-01'";
            arrayOfRequestsFourth_2028[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-11-01' and '2028-12-03'";
            arrayOfRequestsFourth_2028[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2028-12-01' and '2028-12-31'";
        }

        private void Requests_2029_Fourth()
        {
            arrayOfRequestsFourth_2029[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-01-02' and '2029-02-02'";
            arrayOfRequestsFourth_2029[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-02-01' and '2029-02-28'";
            arrayOfRequestsFourth_2029[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-03-01' and '2029-04-01'";
            arrayOfRequestsFourth_2029[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-04-01' and '2029-05-02'";
            arrayOfRequestsFourth_2029[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-05-01' and '2029-06-01'";
            arrayOfRequestsFourth_2029[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-06-01' and '2029-07-03'";
            arrayOfRequestsFourth_2029[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-07-01' and '2029-08-02'";
            arrayOfRequestsFourth_2029[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-08-01' and '2029-09-01'";
            arrayOfRequestsFourth_2029[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-09-02' and '2029-10-03'";
            arrayOfRequestsFourth_2029[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-10-01' and '2029-11-01'";
            arrayOfRequestsFourth_2029[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-11-01' and '2029-12-03'";
            arrayOfRequestsFourth_2029[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-12-01' and '2029-12-31'";
        }

        private void Requests_2030_Fourth()
        {
            arrayOfRequestsFourth_2030[0] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2029-12-29' and '2030-01-30'";
            arrayOfRequestsFourth_2030[1] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-02-01' and '2030-03-03'";
            arrayOfRequestsFourth_2030[2] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-02-27' and '2030-03-30'";
            arrayOfRequestsFourth_2030[3] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-03-29' and '2030-04-28'";
            arrayOfRequestsFourth_2030[4] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-04-29' and '2030-05-30'";
            arrayOfRequestsFourth_2030[5] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-05-29' and '2030-06-28'";
            arrayOfRequestsFourth_2030[6] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-07-01' and '2030-07-31'";
            arrayOfRequestsFourth_2030[7] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-07-29' and '2030-08-31'";
            arrayOfRequestsFourth_2030[8] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-08-29' and '2030-09-30'";
            arrayOfRequestsFourth_2030[9] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-10-01' and '2030-10-30'";
            arrayOfRequestsFourth_2030[10] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-10-29' and '2030-11-30'";
            arrayOfRequestsFourth_2030[11] = "SELECT Day_Shift,Night_Shift,Day_Off,End_Day_Off FROM Fourth_Shift WHERE Day_Shift between '2030-11-29' and '2030-12-30'";
        }

        public List<int> SelectingSaturdayOrSundayForChangeInformation(int years, string months, List<int> numOfDay)
        {
            try
            {
                int year = years;

                string month = months;

                int numberOfMonth = 0;

                switch (month)
                {
                    case "January":
                        numberOfMonth = 1;
                        break;
                    case "February":
                        numberOfMonth = 2;
                        break;
                    case "March":
                        numberOfMonth = 3;
                        break;
                    case "April":
                        numberOfMonth = 4;
                        break;
                    case "May":
                        numberOfMonth = 5;
                        break;
                    case "June":
                        numberOfMonth = 6;
                        break;
                    case "July":
                        numberOfMonth = 7;
                        break;
                    case "August":
                        numberOfMonth = 8;
                        break;
                    case "September":
                        numberOfMonth = 9;
                        break;
                    case "October":
                        numberOfMonth = 10;
                        break;
                    case "November":
                        numberOfMonth = 11;
                        break;
                    case "December":
                        numberOfMonth = 12;
                        break;
                    default:
                        break;
                }

                DateTime date = new DateTime(year, numberOfMonth, 1);

                numOfDayOfMonth = DateTime.DaysInMonth(year, numberOfMonth);

                for (int i = 0; i < numOfDayOfMonth; i++)
                {
                    if (date.DayOfWeek.Equals(DayOfWeek.Saturday) || date.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        numOfDay.Add(i + 1);
                    }

                    date = date.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return numOfDay;
        }

        public void SelectingSaturdayOrSunday()
        {
            int year_ = 0;

            string month = "";

            int numberOfMonth = 0;

            year_ = Convert.ToInt32(year.Text);
            month = nameOfMonth.Text;

            switch (month)
            {
                case "January":
                    numberOfMonth = 1;
                    break;
                case "February":
                    numberOfMonth = 2;
                    break;
                case "March":
                    numberOfMonth = 3;
                    break;
                case "April":
                    numberOfMonth = 4;
                    break;
                case "May":
                    numberOfMonth = 5;
                    break;
                case "June":
                    numberOfMonth = 6;
                    break;
                case "July":
                    numberOfMonth = 7;
                    break;
                case "August":
                    numberOfMonth = 8;
                    break;
                case "September":
                    numberOfMonth = 9;
                    break;
                case "October":
                    numberOfMonth = 10;
                    break;
                case "November":
                    numberOfMonth = 11;
                    break;
                case "December":
                    numberOfMonth = 12;
                    break;
                default:
                    break;
            }

            DateTime date = new DateTime(year_, numberOfMonth, 1);

            numOfDayOfMonth = DateTime.DaysInMonth(year_, numberOfMonth);

            for (int i = 0; i < numOfDayOfMonth; i++)
            {
                if (date.DayOfWeek.Equals(DayOfWeek.Saturday) || date.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    numberOfDay.Add(i + 1);
                }

                date = date.AddDays(1);
            }
        }

        public void GetInformationAboutShift(ScheduleOfShift scheduleOfShift, bool passageContol, int numOfDays)
        {
            ClearTheDaysOfFirst();
            ClearTheDaysOfSecond();
            ClearTheDaysOfThird();
            ClearTheDaysOfFourth();

            if (passageContol == false)
            {
                SelectingYear("", false);
                SelectingMonth("", "", false);
                SelectingSaturdayOrSunday();
            }

            SqlCommand first = new SqlCommand();
            first.CommandText = firstCommandText;

            SqlCommand second = new SqlCommand();
            second.CommandText = secondCommandText;

            SqlCommand third = new SqlCommand();
            third.CommandText = thirdCommandText;

            SqlCommand fourth = new SqlCommand();
            fourth.CommandText = fourthCommandText;

            IDbConnection connection = dao.GetConnection();

            first.Connection = (SqlConnection)connection;
            second.Connection = (SqlConnection)connection;
            third.Connection = (SqlConnection)connection;
            fourth.Connection = (SqlConnection)connection;

            SqlDataAdapter adapterFirst = new SqlDataAdapter(first);
            DataTable tableFirst = new DataTable();
            adapterFirst.Fill(tableFirst);

            SqlDataAdapter adapterSecond = new SqlDataAdapter(second);
            DataTable tableSecond = new DataTable();
            adapterSecond.Fill(tableSecond);

            SqlDataAdapter adapterThird = new SqlDataAdapter(third);
            DataTable tableThird = new DataTable();
            adapterThird.Fill(tableThird);

            SqlDataAdapter adapterFourth = new SqlDataAdapter(fourth);
            DataTable tableFourth = new DataTable();
            adapterFourth.Fill(tableFourth);

            ClearNumberOfDay();

            ChoosingTheNumberOfDay(numberOfDay);
            ClearData2();

            if (passageContol == true)
            {
                FillingInWithData(tableFirst, numOfDays, true);
            }
            else
            {
                FillingInWithData(tableFirst, 0, false);
            }
            ChoosingTheDayFirst(numberOfDayShift);
            ChoosingTheNightFirst(numberOfNightShift);
            ChoosingTheDayOffFirst(numberOfDayOff);
            ChoosingTheEndDayOffFirst(numberOfEndDayOff);
            ClearData();

            if (passageContol == true)
            {
                FillingInWithData(tableSecond, numOfDays, true);
            }
            else
            {
                FillingInWithData(tableSecond, 0, false);
            }
            ChoosingTheDaySecond(numberOfDayShift);
            ChoosingTheNightSecond(numberOfNightShift);
            ChoosingTheDayOffSecond(numberOfDayOff);
            ChoosingTheEndDayOffSecond(numberOfEndDayOff);
            ClearData();

            if (passageContol == true)
            {
                FillingInWithData(tableThird, numOfDays, true);
            }
            else
            {
                FillingInWithData(tableThird, 0, false);
            }
            ChoosingTheDayThird(numberOfDayShift);
            ChoosingTheNightThird(numberOfNightShift);
            ChoosingTheDayOffThird(numberOfDayOff);
            ChoosingTheEndDayOffThird(numberOfEndDayOff);
            ClearData();

            if (passageContol == true)
            {
                FillingInWithData(tableFourth, numOfDays, true);
            }
            else
            {
                FillingInWithData(tableFourth, 0, false);
            }
            ChoosingTheDayFourth(numberOfDayShift);
            ChoosingTheNightFourth(numberOfNightShift);
            ChoosingTheDayOffFourth(numberOfDayOff);
            ChoosingTheEndDayOffFourth(numberOfEndDayOff);
            ClearData();

            dao.ReleaseConnection(connection);
        }

        public void ChangingAWorkerSchedule(int shiftNumber, string year, string month)
        {
            try
            {
                SelectingYear(year, true);
                SelectingMonth(year, month, true);

                switch (shiftNumber)
                {
                    case 1:
                        SqlCommand first = new SqlCommand();
                        first.CommandText = firstCommandText;

                        IDbConnection connection = dao.GetConnection();

                        first.Connection = (SqlConnection)connection;

                        SqlDataAdapter adapterFirst = new SqlDataAdapter(first);
                        DataTable tableFirst = new DataTable();
                        adapterFirst.Fill(tableFirst);

                        FillingInWithData(tableFirst, 0, false);
                        ChoosingTheDayFirst(numberOfDayShift);
                        ChoosingTheNightFirst(numberOfNightShift);
                        ChoosingTheDayOffFirst(numberOfDayOff);
                        ChoosingTheEndDayOffFirst(numberOfEndDayOff);

                        dao.ReleaseConnection(connection);
                        break;
                    case 2:
                        SqlCommand second = new SqlCommand();
                        second.CommandText = secondCommandText;

                        IDbConnection connectionS = dao.GetConnection();

                        second.Connection = (SqlConnection)connectionS;

                        SqlDataAdapter adapterSecond = new SqlDataAdapter(second);
                        DataTable tableSecond = new DataTable();
                        adapterSecond.Fill(tableSecond);

                        FillingInWithData(tableSecond, 0, false);
                        ChoosingTheDaySecond(numberOfDayShift);
                        ChoosingTheNightSecond(numberOfNightShift);
                        ChoosingTheDayOffSecond(numberOfDayOff);
                        ChoosingTheEndDayOffSecond(numberOfEndDayOff);

                        dao.ReleaseConnection(connectionS);
                        break;
                    case 3:
                        SqlCommand third = new SqlCommand();
                        third.CommandText = thirdCommandText;

                        IDbConnection connectionTh = dao.GetConnection();

                        third.Connection = (SqlConnection)connectionTh;

                        SqlDataAdapter adapterThird = new SqlDataAdapter(third);
                        DataTable tableThird = new DataTable();
                        adapterThird.Fill(tableThird);

                        FillingInWithData(tableThird, 0, false);
                        ChoosingTheDayThird(numberOfDayShift);
                        ChoosingTheNightThird(numberOfNightShift);
                        ChoosingTheDayOffThird(numberOfDayOff);
                        ChoosingTheEndDayOffThird(numberOfEndDayOff);

                        dao.ReleaseConnection(connectionTh);
                        break;
                    case 4:
                        SqlCommand fourth = new SqlCommand();
                        fourth.CommandText = fourthCommandText;

                        IDbConnection connectionF = dao.GetConnection();

                        fourth.Connection = (SqlConnection)connectionF;

                        SqlDataAdapter adapterFourth = new SqlDataAdapter(fourth);
                        DataTable tableFourth = new DataTable();
                        adapterFourth.Fill(tableFourth);

                        FillingInWithData(tableFourth, 0, false);
                        ChoosingTheDayFourth(numberOfDayShift);
                        ChoosingTheNightFourth(numberOfNightShift);
                        ChoosingTheDayOffFourth(numberOfDayOff);
                        ChoosingTheEndDayOffFourth(numberOfEndDayOff);

                        dao.ReleaseConnection(connectionF);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            dayShift = "";
            nightShift = "";
            dayOff = "";
            endDayOff = "";

            numberOfDayShift.Clear();
            numberOfNightShift.Clear();
            numberOfDayOff.Clear();
            numberOfEndDayOff.Clear();
        }

        private void ClearData2()
        {
            numberOfDay.Clear();
        }

        public void FillingInWithData(DataTable table, int numOfDays, bool passageControl)
        {
            try
            {
                int number = 0;//the number of iterations depending on the number of days in the month

                if (passageControl == true)
                {
                    if (numOfDays == 31 || numOfDays == 28 || numOfDays == 29)
                    {
                        number = 7;
                    }
                    if (numOfDays == 30)
                    {
                        number = 8;
                    }
                }
                else
                {
                    if (numOfDayOfMonth == 31 || numOfDayOfMonth == 28 || numOfDayOfMonth == 29)
                    {
                        number = 7;
                    }
                    if (numOfDayOfMonth == 30)
                    {
                        number = 8;
                    }
                }

                foreach (DataRow row in table.Rows)
                {
                    number--;
                    dayShift = row[0].ToString();
                    GetSpecificDayShiftNumber();
                    int day = Convert.ToInt32(dayShift);
                    numberOfDayShift.Add(day);

                    nightShift = row[1].ToString();
                    GetSpecificNightShiftNumber();
                    int night = Convert.ToInt32(nightShift);
                    numberOfNightShift.Add(night);

                    dayOff = row[2].ToString();
                    GetSpecificStartDayOffNumber();
                    int dayOff1 = Convert.ToInt32(dayOff);
                    numberOfDayOff.Add(dayOff1);

                    if (number >= 0)
                    {
                        endDayOff = row[3].ToString();
                        GetSpecificEndDayOffNumber();
                        int endDayOff1 = Convert.ToInt32(endDayOff);
                        numberOfEndDayOff.Add(endDayOff1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetSpecificDayShiftNumber()
        {
            try
            {
                dayShift = dayShift.Substring(0, dayShift.Length - 16);

                if (dayShift.Equals("01") || dayShift.Equals("02") || dayShift.Equals("03") || dayShift.Equals("04")
                    || dayShift.Equals("05") || dayShift.Equals("06") || dayShift.Equals("07") || dayShift.Equals("08")
                    || dayShift.Equals("09"))
                {
                    dayShift = dayShift.Substring(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetSpecificNightShiftNumber()
        {
            try
            {
                nightShift = nightShift.Substring(0, nightShift.Length - 16);

                if (nightShift.Equals("01") || nightShift.Equals("02") || nightShift.Equals("03") || nightShift.Equals("04")
                    || nightShift.Equals("05") || nightShift.Equals("06") || nightShift.Equals("07") || nightShift.Equals("08")
                    || nightShift.Equals("09"))
                {
                    nightShift = nightShift.Substring(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetSpecificStartDayOffNumber()
        {
            try
            {
                dayOff = dayOff.Substring(0, dayOff.Length - 16);

                if (dayOff.Equals("01") || dayOff.Equals("02") || dayOff.Equals("03") || dayOff.Equals("04")
                    || dayOff.Equals("05") || dayOff.Equals("06") || dayOff.Equals("07") || dayOff.Equals("08")
                    || dayOff.Equals("09"))
                {
                    dayOff = dayOff.Substring(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetSpecificEndDayOffNumber()
        {
            try
            {
                endDayOff = endDayOff.Substring(0, endDayOff.Length - 16);

                if (endDayOff.Equals("01") || endDayOff.Equals("02") || endDayOff.Equals("03") || endDayOff.Equals("04")
                    || endDayOff.Equals("05") || endDayOff.Equals("06") || endDayOff.Equals("07") || endDayOff.Equals("08")
                    || endDayOff.Equals("09"))
                {
                    endDayOff = endDayOff.Substring(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayFirst(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = 12;
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_F.Content = 12;
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_F.Content = 12;
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_F.Content = 12;
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_F.Content = 12;
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_F.Content = 12;
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_F.Content = 12;
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_F.Content = 12;
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_F.Content = 12;
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_F.Content = 12;
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_F.Content = 12;
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_F.Content = 12;
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_F.Content = 12;
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_F.Content = 12;
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_F.Content = 12;
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_F.Content = 12;
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_F.Content = 12;
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_F.Content = 12;
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_F.Content = 12;
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_F.Content = 12;
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_F.Content = 12;
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_F.Content = 12;
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_F.Content = 12;
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_F.Content = 12;
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_F.Content = 12;
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_F.Content = 12;
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_F.Content = 12;
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_F.Content = 12;
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_F.Content = 12;
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_F.Content = 12;
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_F.Content = 12;
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheNightFirst(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = 4;
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_F.Content = 4;
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_F.Content = 4;
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_F.Content = 4;
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_F.Content = 4;
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_F.Content = 4;
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_F.Content = 4;
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_F.Content = 4;
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_F.Content = 4;
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_F.Content = 4;
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_F.Content = 4;
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_F.Content = 4;
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_F.Content = 4;
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_F.Content = 4;
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_F.Content = 4;
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_F.Content = 4;
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_F.Content = 4;
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_F.Content = 4;
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_F.Content = 4;
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_F.Content = 4;
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_F.Content = 4;
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_F.Content = 4;
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_F.Content = 4;
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_F.Content = 4;
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_F.Content = 4;
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_F.Content = 4;
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_F.Content = 4;
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_F.Content = 4;
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_F.Content = 4;
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_F.Content = 4;
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_F.Content = 4;
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayOffFirst(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = 8;
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_F.Content = 8;
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_F.Content = 8;
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_F.Content = 8;
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_F.Content = 8;
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_F.Content = 8;
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_F.Content = 8;
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_F.Content = 8;
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_F.Content = 8;
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_F.Content = 8;
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_F.Content = 8;
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_F.Content = 8;
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_F.Content = 8;
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_F.Content = 8;
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_F.Content = 8;
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_F.Content = 8;
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_F.Content = 8;
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_F.Content = 8;
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_F.Content = 8;
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_F.Content = 8;
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_F.Content = 8;
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_F.Content = 8;
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_F.Content = 8;
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_F.Content = 8;
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_F.Content = 8;
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_F.Content = 8;
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_F.Content = 8;
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_F.Content = 8;
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_F.Content = 8;
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_F.Content = 8;
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_F.Content = 8;
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheEndDayOffFirst(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = "D";
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_F.Content = "D";
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_F.Content = "D";
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_F.Content = "D";
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_F.Content = "D";
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_F.Content = "D";
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_F.Content = "D";
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_F.Content = "D";
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_F.Content = "D";
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_F.Content = "D";
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_F.Content = "D";
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_F.Content = "D";
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_F.Content = "D";
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_F.Content = "D";
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_F.Content = "D";
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_F.Content = "D";
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_F.Content = "D";
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_F.Content = "D";
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_F.Content = "D";
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_F.Content = "D";
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_F.Content = "D";
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_F.Content = "D";
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_F.Content = "D";
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_F.Content = "D";
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_F.Content = "D";
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_F.Content = "D";
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_F.Content = "D";
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_F.Content = "D";
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_F.Content = "D";
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_F.Content = "D";
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_F.Content = "D";
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTheDaysOfFirst()
        {
            day_1_F.Content = "";
            day_1_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_2_F.Content = "";
            day_2_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_3_F.Content = "";
            day_3_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_4_F.Content = "";
            day_4_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_5_F.Content = "";
            day_5_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_6_F.Content = "";
            day_6_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_7_F.Content = "";
            day_7_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_8_F.Content = "";
            day_8_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_9_F.Content = "";
            day_9_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_10_F.Content = "";
            day_10_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_11_F.Content = "";
            day_11_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_12_F.Content = "";
            day_12_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_13_F.Content = "";
            day_13_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_14_F.Content = "";
            day_14_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_15_F.Content = "";
            day_15_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_16_F.Content = "";
            day_16_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_17_F.Content = "";
            day_17_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_18_F.Content = "";
            day_18_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_19_F.Content = "";
            day_19_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_20_F.Content = "";
            day_20_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_21_F.Content = "";
            day_21_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_22_F.Content = "";
            day_22_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_23_F.Content = "";
            day_23_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_24_F.Content = "";
            day_24_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_25_F.Content = "";
            day_25_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_26_F.Content = "";
            day_26_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_27_F.Content = "";
            day_27_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_28_F.Content = "";
            day_28_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_29_F.Content = "";
            day_29_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_30_F.Content = "";
            day_30_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_31_F.Content = "";
            day_31_F.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public void ChoosingTheDaySecond(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_S.Content = 12;
                            day_1_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_S.Content = 12;
                            day_2_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_S.Content = 12;
                            day_3_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_S.Content = 12;
                            day_4_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_S.Content = 12;
                            day_5_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_S.Content = 12;
                            day_6_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_S.Content = 12;
                            day_7_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_S.Content = 12;
                            day_8_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_S.Content = 12;
                            day_9_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_S.Content = 12;
                            day_10_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_S.Content = 12;
                            day_11_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_S.Content = 12;
                            day_12_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_S.Content = 12;
                            day_13_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_S.Content = 12;
                            day_14_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_S.Content = 12;
                            day_15_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_S.Content = 12;
                            day_16_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_S.Content = 12;
                            day_17_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_S.Content = 12;
                            day_18_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_S.Content = 12;
                            day_19_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_S.Content = 12;
                            day_20_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_S.Content = 12;
                            day_21_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_S.Content = 12;
                            day_22_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_S.Content = 12;
                            day_23_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_S.Content = 12;
                            day_24_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_S.Content = 12;
                            day_25_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_S.Content = 12;
                            day_26_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_S.Content = 12;
                            day_27_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_S.Content = 12;
                            day_28_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_S.Content = 12;
                            day_29_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_S.Content = 12;
                            day_30_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_S.Content = 12;
                            day_31_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheNightSecond(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_S.Content = 4;
                            day_1_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_S.Content = 4;
                            day_2_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_S.Content = 4;
                            day_3_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_S.Content = 4;
                            day_4_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_S.Content = 4;
                            day_5_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_S.Content = 4;
                            day_6_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_S.Content = 4;
                            day_7_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_S.Content = 4;
                            day_8_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_S.Content = 4;
                            day_9_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_S.Content = 4;
                            day_10_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_S.Content = 4;
                            day_11_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_S.Content = 4;
                            day_12_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_S.Content = 4;
                            day_13_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_S.Content = 4;
                            day_14_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_S.Content = 4;
                            day_15_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_S.Content = 4;
                            day_16_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_S.Content = 4;
                            day_17_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_S.Content = 4;
                            day_18_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_S.Content = 4;
                            day_19_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_S.Content = 4;
                            day_20_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_S.Content = 4;
                            day_21_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_S.Content = 4;
                            day_22_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_S.Content = 4;
                            day_23_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_S.Content = 4;
                            day_24_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_S.Content = 4;
                            day_25_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_S.Content = 4;
                            day_26_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_S.Content = 4;
                            day_27_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_S.Content = 4;
                            day_28_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_S.Content = 4;
                            day_29_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_S.Content = 4;
                            day_30_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_S.Content = 4;
                            day_31_S.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayOffSecond(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_S.Content = 8;
                            day_1_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_S.Content = 8;
                            day_2_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_S.Content = 8;
                            day_3_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_S.Content = 8;
                            day_4_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_S.Content = 8;
                            day_5_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_S.Content = 8;
                            day_6_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_S.Content = 8;
                            day_7_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_S.Content = 8;
                            day_8_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_S.Content = 8;
                            day_9_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_S.Content = 8;
                            day_10_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_S.Content = 8;
                            day_11_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_S.Content = 8;
                            day_12_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_S.Content = 8;
                            day_13_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_S.Content = 8;
                            day_14_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_S.Content = 8;
                            day_15_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_S.Content = 8;
                            day_16_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_S.Content = 8;
                            day_17_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_S.Content = 8;
                            day_18_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_S.Content = 8;
                            day_19_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_S.Content = 8;
                            day_20_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_S.Content = 8;
                            day_21_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_S.Content = 8;
                            day_22_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_S.Content = 8;
                            day_23_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_S.Content = 8;
                            day_24_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_S.Content = 8;
                            day_25_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_S.Content = 8;
                            day_26_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_S.Content = 8;
                            day_27_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_S.Content = 8;
                            day_28_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_S.Content = 8;
                            day_29_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_S.Content = 8;
                            day_30_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_S.Content = 8;
                            day_31_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheEndDayOffSecond(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_S.Content = "D";
                            day_1_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_S.Content = "D";
                            day_2_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_S.Content = "D";
                            day_3_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_S.Content = "D";
                            day_4_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_S.Content = "D";
                            day_5_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_S.Content = "D";
                            day_6_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_S.Content = "D";
                            day_7_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_S.Content = "D";
                            day_8_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_S.Content = "D";
                            day_9_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_S.Content = "D";
                            day_10_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_S.Content = "D";
                            day_11_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_S.Content = "D";
                            day_12_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_S.Content = "D";
                            day_13_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_S.Content = "D";
                            day_14_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_S.Content = "D";
                            day_15_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_S.Content = "D";
                            day_16_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_S.Content = "D";
                            day_17_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_S.Content = "D";
                            day_18_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_S.Content = "D";
                            day_19_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_S.Content = "D";
                            day_20_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_S.Content = "D";
                            day_21_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_S.Content = "D";
                            day_22_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_S.Content = "D";
                            day_23_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_S.Content = "D";
                            day_24_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_S.Content = "D";
                            day_25_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_S.Content = "D";
                            day_26_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_S.Content = "D";
                            day_27_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_S.Content = "D";
                            day_28_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_S.Content = "D";
                            day_29_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_S.Content = "D";
                            day_30_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_S.Content = "D";
                            day_31_S.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTheDaysOfSecond()
        {
            day_1_S.Content = "";
            day_1_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_2_S.Content = "";
            day_2_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_3_S.Content = "";
            day_3_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_4_S.Content = "";
            day_4_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_5_S.Content = "";
            day_5_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_6_S.Content = "";
            day_6_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_7_S.Content = "";
            day_7_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_8_S.Content = "";
            day_8_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_9_S.Content = "";
            day_9_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_10_S.Content = "";
            day_10_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_11_S.Content = "";
            day_11_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_12_S.Content = "";
            day_12_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_13_S.Content = "";
            day_13_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_14_S.Content = "";
            day_14_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_15_S.Content = "";
            day_15_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_16_S.Content = "";
            day_16_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_17_S.Content = "";
            day_17_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_18_S.Content = "";
            day_18_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_19_S.Content = "";
            day_19_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_20_S.Content = "";
            day_20_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_21_S.Content = "";
            day_21_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_22_S.Content = "";
            day_22_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_23_S.Content = "";
            day_23_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_24_S.Content = "";
            day_24_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_25_S.Content = "";
            day_25_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_26_S.Content = "";
            day_26_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_27_S.Content = "";
            day_27_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_28_S.Content = "";
            day_28_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_29_S.Content = "";
            day_29_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_30_S.Content = "";
            day_30_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_31_S.Content = "";
            day_31_S.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public void ChoosingTheDayThird(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_T.Content = 12;
                            day_1_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_T.Content = 12;
                            day_2_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_T.Content = 12;
                            day_3_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_T.Content = 12;
                            day_4_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_T.Content = 12;
                            day_5_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_T.Content = 12;
                            day_6_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_T.Content = 12;
                            day_7_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_T.Content = 12;
                            day_8_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_T.Content = 12;
                            day_9_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_T.Content = 12;
                            day_10_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_T.Content = 12;
                            day_11_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_T.Content = 12;
                            day_12_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_T.Content = 12;
                            day_13_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_T.Content = 12;
                            day_14_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_T.Content = 12;
                            day_15_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_T.Content = 12;
                            day_16_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_T.Content = 12;
                            day_17_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_T.Content = 12;
                            day_18_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_T.Content = 12;
                            day_19_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_T.Content = 12;
                            day_20_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_T.Content = 12;
                            day_21_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_T.Content = 12;
                            day_22_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_T.Content = 12;
                            day_23_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_T.Content = 12;
                            day_24_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_T.Content = 12;
                            day_25_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_T.Content = 12;
                            day_26_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_T.Content = 12;
                            day_27_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_T.Content = 12;
                            day_28_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_T.Content = 12;
                            day_29_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_T.Content = 12;
                            day_30_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_T.Content = 12;
                            day_31_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void ChoosingTheNightThird(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_T.Content = 4;
                            day_1_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_T.Content = 4;
                            day_2_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_T.Content = 4;
                            day_3_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_T.Content = 4;
                            day_4_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_T.Content = 4;
                            day_5_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_T.Content = 4;
                            day_6_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_T.Content = 4;
                            day_7_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_T.Content = 4;
                            day_8_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_T.Content = 4;
                            day_9_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_T.Content = 4;
                            day_10_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_T.Content = 4;
                            day_11_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_T.Content = 4;
                            day_12_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_T.Content = 4;
                            day_13_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_T.Content = 4;
                            day_14_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_T.Content = 4;
                            day_15_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_T.Content = 4;
                            day_16_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_T.Content = 4;
                            day_17_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_T.Content = 4;
                            day_18_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_T.Content = 4;
                            day_19_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_T.Content = 4;
                            day_20_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_T.Content = 4;
                            day_21_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_T.Content = 4;
                            day_22_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_T.Content = 4;
                            day_23_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_T.Content = 4;
                            day_24_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_T.Content = 4;
                            day_25_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_T.Content = 4;
                            day_26_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_T.Content = 4;
                            day_27_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_T.Content = 4;
                            day_28_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_T.Content = 4;
                            day_29_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_T.Content = 4;
                            day_30_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_T.Content = 4;
                            day_31_T.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayOffThird(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_T.Content = 8;
                            day_1_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_T.Content = 8;
                            day_2_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_T.Content = 8;
                            day_3_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_T.Content = 8;
                            day_4_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_T.Content = 8;
                            day_5_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_T.Content = 8;
                            day_6_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_T.Content = 8;
                            day_7_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_T.Content = 8;
                            day_8_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_T.Content = 8;
                            day_9_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_T.Content = 8;
                            day_10_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_T.Content = 8;
                            day_11_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_T.Content = 8;
                            day_12_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_T.Content = 8;
                            day_13_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_T.Content = 8;
                            day_14_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_T.Content = 8;
                            day_15_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_T.Content = 8;
                            day_16_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_T.Content = 8;
                            day_17_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_T.Content = 8;
                            day_18_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_T.Content = 8;
                            day_19_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_T.Content = 8;
                            day_20_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_T.Content = 8;
                            day_21_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_T.Content = 8;
                            day_22_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_T.Content = 8;
                            day_23_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_T.Content = 8;
                            day_24_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_T.Content = 8;
                            day_25_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_T.Content = 8;
                            day_26_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_T.Content = 8;
                            day_27_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_T.Content = 8;
                            day_28_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_T.Content = 8;
                            day_29_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_T.Content = 8;
                            day_30_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_T.Content = 8;
                            day_31_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheEndDayOffThird(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_T.Content = "D";
                            day_1_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_T.Content = "D";
                            day_2_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_T.Content = "D";
                            day_3_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_T.Content = "D";
                            day_4_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_T.Content = "D";
                            day_5_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_T.Content = "D";
                            day_6_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_T.Content = "D";
                            day_7_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_T.Content = "D";
                            day_8_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_T.Content = "D";
                            day_9_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_T.Content = "D";
                            day_10_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_T.Content = "D";
                            day_11_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_T.Content = "D";
                            day_12_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_T.Content = "D";
                            day_13_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_T.Content = "D";
                            day_14_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_T.Content = "D";
                            day_15_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_T.Content = "D";
                            day_16_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_T.Content = "D";
                            day_17_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_T.Content = "D";
                            day_18_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_T.Content = "D";
                            day_19_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_T.Content = "D";
                            day_20_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_T.Content = "D";
                            day_21_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_T.Content = "D";
                            day_22_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_T.Content = "D";
                            day_23_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_T.Content = "D";
                            day_24_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_T.Content = "D";
                            day_25_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_T.Content = "D";
                            day_26_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_T.Content = "D";
                            day_27_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_T.Content = "D";
                            day_28_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_T.Content = "D";
                            day_29_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_T.Content = "D";
                            day_30_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_T.Content = "D";
                            day_31_T.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTheDaysOfThird()
        {
            day_1_T.Content = "";
            day_1_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_2_T.Content = "";
            day_2_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_3_T.Content = "";
            day_3_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_4_T.Content = "";
            day_4_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_5_T.Content = "";
            day_5_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_6_T.Content = "";
            day_6_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_7_T.Content = "";
            day_7_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_8_T.Content = "";
            day_8_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_9_T.Content = "";
            day_9_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_10_T.Content = "";
            day_10_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_11_T.Content = "";
            day_11_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_12_T.Content = "";
            day_12_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_13_T.Content = "";
            day_13_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_14_T.Content = "";
            day_14_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_15_T.Content = "";
            day_15_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_16_T.Content = "";
            day_16_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_17_T.Content = "";
            day_17_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_18_T.Content = "";
            day_18_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_19_T.Content = "";
            day_19_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_20_T.Content = "";
            day_20_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_21_T.Content = "";
            day_21_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_22_T.Content = "";
            day_22_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_23_T.Content = "";
            day_23_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_24_T.Content = "";
            day_24_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_25_T.Content = "";
            day_25_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_26_T.Content = "";
            day_26_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_27_T.Content = "";
            day_27_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_28_T.Content = "";
            day_28_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_29_T.Content = "";
            day_29_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_30_T.Content = "";
            day_30_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_31_T.Content = "";
            day_31_T.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }
        public void ChoosingTheDayFourth(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_Fo.Content = 12;
                            day_1_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_Fo.Content = 12;
                            day_2_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_Fo.Content = 12;
                            day_3_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_Fo.Content = 12;
                            day_4_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_Fo.Content = 12;
                            day_5_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_Fo.Content = 12;
                            day_6_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_Fo.Content = 12;
                            day_7_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_Fo.Content = 12;
                            day_8_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_Fo.Content = 12;
                            day_9_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_Fo.Content = 12;
                            day_10_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_Fo.Content = 12;
                            day_11_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_Fo.Content = 12;
                            day_12_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_Fo.Content = 12;
                            day_13_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_Fo.Content = 12;
                            day_14_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_Fo.Content = 12;
                            day_15_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_Fo.Content = 12;
                            day_16_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_Fo.Content = 12;
                            day_17_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_Fo.Content = 12;
                            day_18_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_Fo.Content = 12;
                            day_19_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_Fo.Content = 12;
                            day_20_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_Fo.Content = 12;
                            day_21_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_Fo.Content = 12;
                            day_22_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_Fo.Content = 12;
                            day_23_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_Fo.Content = 12;
                            day_24_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_Fo.Content = 12;
                            day_25_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_Fo.Content = 12;
                            day_26_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_Fo.Content = 12;
                            day_27_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_Fo.Content = 12;
                            day_28_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_Fo.Content = 12;
                            day_29_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_Fo.Content = 12;
                            day_30_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_Fo.Content = 12;
                            day_31_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheNightFourth(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_Fo.Content = 4;
                            day_1_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 2:
                            day_2_Fo.Content = 4;
                            day_2_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 3:
                            day_3_Fo.Content = 4;
                            day_3_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 4:
                            day_4_Fo.Content = 4;
                            day_4_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 5:
                            day_5_Fo.Content = 4;
                            day_5_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 6:
                            day_6_Fo.Content = 4;
                            day_6_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 7:
                            day_7_Fo.Content = 4;
                            day_7_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 8:
                            day_8_Fo.Content = 4;
                            day_8_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 9:
                            day_9_Fo.Content = 4;
                            day_9_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 10:
                            day_10_Fo.Content = 4;
                            day_10_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 11:
                            day_11_Fo.Content = 4;
                            day_11_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 12:
                            day_12_Fo.Content = 4;
                            day_12_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 13:
                            day_13_Fo.Content = 4;
                            day_13_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 14:
                            day_14_Fo.Content = 4;
                            day_14_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 15:
                            day_15_Fo.Content = 4;
                            day_15_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 16:
                            day_16_Fo.Content = 4;
                            day_16_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 17:
                            day_17_Fo.Content = 4;
                            day_17_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 18:
                            day_18_Fo.Content = 4;
                            day_18_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 19:
                            day_19_Fo.Content = 4;
                            day_19_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 20:
                            day_20_Fo.Content = 4;
                            day_20_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 21:
                            day_21_Fo.Content = 4;
                            day_21_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 22:
                            day_22_Fo.Content = 4;
                            day_22_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 23:
                            day_23_Fo.Content = 4;
                            day_23_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 24:
                            day_24_Fo.Content = 4;
                            day_24_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 25:
                            day_25_Fo.Content = 4;
                            day_25_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 26:
                            day_26_Fo.Content = 4;
                            day_26_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 27:
                            day_27_Fo.Content = 4;
                            day_27_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 28:
                            day_28_Fo.Content = 4;
                            day_28_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 29:
                            day_29_Fo.Content = 4;
                            day_29_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 30:
                            day_30_Fo.Content = 4;
                            day_30_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        case 31:
                            day_31_Fo.Content = 4;
                            day_31_Fo.Background = new SolidColorBrush(Color.FromRgb(255, 0, 7));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayOffFourth(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_Fo.Content = 8;
                            day_1_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_Fo.Content = 8;
                            day_2_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_Fo.Content = 8;
                            day_3_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_Fo.Content = 8;
                            day_4_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_Fo.Content = 8;
                            day_5_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_Fo.Content = 8;
                            day_6_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_Fo.Content = 8;
                            day_7_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_Fo.Content = 8;
                            day_8_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_Fo.Content = 8;
                            day_9_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_Fo.Content = 8;
                            day_10_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_Fo.Content = 8;
                            day_11_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_Fo.Content = 8;
                            day_12_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_Fo.Content = 8;
                            day_13_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_Fo.Content = 8;
                            day_14_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_Fo.Content = 8;
                            day_15_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_Fo.Content = 8;
                            day_16_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_Fo.Content = 8;
                            day_17_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_Fo.Content = 8;
                            day_18_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_Fo.Content = 8;
                            day_19_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_Fo.Content = 8;
                            day_20_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_Fo.Content = 8;
                            day_21_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_Fo.Content = 8;
                            day_22_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_Fo.Content = 8;
                            day_23_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_Fo.Content = 8;
                            day_24_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_Fo.Content = 8;
                            day_25_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_Fo.Content = 8;
                            day_26_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_Fo.Content = 8;
                            day_27_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_Fo.Content = 8;
                            day_28_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_Fo.Content = 8;
                            day_29_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_Fo.Content = 8;
                            day_30_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_Fo.Content = 8;
                            day_31_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheEndDayOffFourth(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_Fo.Content = "D";
                            day_1_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 2:
                            day_2_Fo.Content = "D";
                            day_2_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 3:
                            day_3_Fo.Content = "D";
                            day_3_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 4:
                            day_4_Fo.Content = "D";
                            day_4_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 5:
                            day_5_Fo.Content = "D";
                            day_5_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 6:
                            day_6_Fo.Content = "D";
                            day_6_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 7:
                            day_7_Fo.Content = "D";
                            day_7_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 8:
                            day_8_Fo.Content = "D";
                            day_8_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 9:
                            day_9_Fo.Content = "D";
                            day_9_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 10:
                            day_10_Fo.Content = "D";
                            day_10_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 11:
                            day_11_Fo.Content = "D";
                            day_11_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 12:
                            day_12_Fo.Content = "D";
                            day_12_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 13:
                            day_13_Fo.Content = "D";
                            day_13_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 14:
                            day_14_Fo.Content = "D";
                            day_14_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 15:
                            day_15_Fo.Content = "D";
                            day_15_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 16:
                            day_16_Fo.Content = "D";
                            day_16_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 17:
                            day_17_Fo.Content = "D";
                            day_17_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 18:
                            day_18_Fo.Content = "D";
                            day_18_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 19:
                            day_19_Fo.Content = "D";
                            day_19_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 20:
                            day_20_Fo.Content = "D";
                            day_20_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 21:
                            day_21_Fo.Content = "D";
                            day_21_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 22:
                            day_22_Fo.Content = "D";
                            day_22_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 23:
                            day_23_Fo.Content = "D";
                            day_23_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 24:
                            day_24_Fo.Content = "D";
                            day_24_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 25:
                            day_25_Fo.Content = "D";
                            day_25_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 26:
                            day_26_Fo.Content = "D";
                            day_26_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 27:
                            day_27_Fo.Content = "D";
                            day_27_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 28:
                            day_28_Fo.Content = "D";
                            day_28_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 29:
                            day_29_Fo.Content = "D";
                            day_29_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 30:
                            day_30_Fo.Content = "D";
                            day_30_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        case 31:
                            day_31_Fo.Content = "D";
                            day_31_Fo.Background = new SolidColorBrush(Color.FromRgb(139, 255, 1));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTheDaysOfFourth()
        {
            day_1_Fo.Content = "";
            day_1_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_2_Fo.Content = "";
            day_2_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_3_Fo.Content = "";
            day_3_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_4_Fo.Content = "";
            day_4_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_5_Fo.Content = "";
            day_5_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_6_Fo.Content = "";
            day_6_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_7_Fo.Content = "";
            day_7_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_8_Fo.Content = "";
            day_8_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_9_Fo.Content = "";
            day_9_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_10_Fo.Content = "";
            day_10_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_11_Fo.Content = "";
            day_11_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_12_Fo.Content = "";
            day_12_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_13_Fo.Content = "";
            day_13_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_14_Fo.Content = "";
            day_14_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_15_Fo.Content = "";
            day_15_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_16_Fo.Content = "";
            day_16_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_17_Fo.Content = "";
            day_17_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_18_Fo.Content = "";
            day_18_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_19_Fo.Content = "";
            day_19_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_20_Fo.Content = "";
            day_20_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_21_Fo.Content = "";
            day_21_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_22_Fo.Content = "";
            day_22_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_23_Fo.Content = "";
            day_23_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_24_Fo.Content = "";
            day_24_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_25_Fo.Content = "";
            day_25_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_26_Fo.Content = "";
            day_26_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_27_Fo.Content = "";
            day_27_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_28_Fo.Content = "";
            day_28_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_29_Fo.Content = "";
            day_29_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_30_Fo.Content = "";
            day_30_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            day_31_Fo.Content = "";
            day_31_Fo.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public void ChoosingTheNumberOfDay(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            num_1.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 2:
                            num_2.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 3:
                            num_3.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 4:
                            num_4.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 5:
                            num_5.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 6:
                            num_6.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 7:
                            num_7.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 8:
                            num_8.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 9:
                            num_9.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 10:
                            num_10.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 11:
                            num_11.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 12:
                            num_12.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 13:
                            num_13.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 14:
                            num_14.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 15:
                            num_15.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 16:
                            num_16.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 17:
                            num_17.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 18:
                            num_18.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 19:
                            num_19.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 20:
                            num_20.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 21:
                            num_21.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 22:
                            num_22.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 23:
                            num_23.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 24:
                            num_24.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 25:
                            num_25.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 26:
                            num_26.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 27:
                            num_27.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 28:
                            num_28.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 29:
                            num_29.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 30:
                            num_30.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        case 31:
                            num_31.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearNumberOfDay()
        {
            num_1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_2.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_3.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_4.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_5.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_6.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_7.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_8.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_9.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_10.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_11.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_12.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_13.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_14.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_15.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_16.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_17.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_18.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_19.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_20.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_21.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_22.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_23.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_24.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_25.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_26.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_27.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_28.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_29.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_30.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            num_31.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private void See_Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetInformationAboutShift(scheduleOf, false, 0);

                SetOfParameters();
            }
            catch
            {
                SetOfParameters2();
            }
        }

        public List<string> GetArrayOfValuesOfShifts(int dayNumber)
        {
            List<string> arrayOfValuesOfShifts = new List<string>();

            try
            {
                string valueOfFirstShift = "";
                string valueOfSecondShift = "";
                string valueOfThirdShift = "";
                string valueOfFourthShift = "";

                switch (dayNumber)
                {
                    case 1:
                        valueOfFirstShift = day_1_F.Content.ToString();
                        valueOfSecondShift = day_1_S.Content.ToString();
                        valueOfThirdShift = day_1_T.Content.ToString();
                        valueOfFourthShift = day_1_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;

                    case 2:
                        valueOfFirstShift = day_2_F.Content.ToString();
                        valueOfSecondShift = day_2_S.Content.ToString();
                        valueOfThirdShift = day_2_T.Content.ToString();
                        valueOfFourthShift = day_2_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;

                    case 3:
                        valueOfFirstShift = day_3_F.Content.ToString();
                        valueOfSecondShift = day_3_S.Content.ToString();
                        valueOfThirdShift = day_3_T.Content.ToString();
                        valueOfFourthShift = day_3_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;

                    case 4:
                        valueOfFirstShift = day_4_F.Content.ToString();
                        valueOfSecondShift = day_4_S.Content.ToString();
                        valueOfThirdShift = day_4_T.Content.ToString();
                        valueOfFourthShift = day_4_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 5:
                        valueOfFirstShift = day_5_F.Content.ToString();
                        valueOfSecondShift = day_5_S.Content.ToString();
                        valueOfThirdShift = day_5_T.Content.ToString();
                        valueOfFourthShift = day_5_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 6:
                        valueOfFirstShift = day_6_F.Content.ToString();
                        valueOfSecondShift = day_6_S.Content.ToString();
                        valueOfThirdShift = day_6_T.Content.ToString();
                        valueOfFourthShift = day_6_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 7:
                        valueOfFirstShift = day_7_F.Content.ToString();
                        valueOfSecondShift = day_7_S.Content.ToString();
                        valueOfThirdShift = day_7_T.Content.ToString();
                        valueOfFourthShift = day_7_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 8:
                        valueOfFirstShift = day_8_F.Content.ToString();
                        valueOfSecondShift = day_8_S.Content.ToString();
                        valueOfThirdShift = day_8_T.Content.ToString();
                        valueOfFourthShift = day_8_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 9:
                        valueOfFirstShift = day_9_F.Content.ToString();
                        valueOfSecondShift = day_9_S.Content.ToString();
                        valueOfThirdShift = day_9_T.Content.ToString();
                        valueOfFourthShift = day_9_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 10:
                        valueOfFirstShift = day_10_F.Content.ToString();
                        valueOfSecondShift = day_10_S.Content.ToString();
                        valueOfThirdShift = day_10_T.Content.ToString();
                        valueOfFourthShift = day_10_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 11:
                        valueOfFirstShift = day_11_F.Content.ToString();
                        valueOfSecondShift = day_11_S.Content.ToString();
                        valueOfThirdShift = day_11_T.Content.ToString();
                        valueOfFourthShift = day_11_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 12:
                        valueOfFirstShift = day_12_F.Content.ToString();
                        valueOfSecondShift = day_12_S.Content.ToString();
                        valueOfThirdShift = day_12_T.Content.ToString();
                        valueOfFourthShift = day_12_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 13:
                        valueOfFirstShift = day_13_F.Content.ToString();
                        valueOfSecondShift = day_13_S.Content.ToString();
                        valueOfThirdShift = day_13_T.Content.ToString();
                        valueOfFourthShift = day_13_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 14:
                        valueOfFirstShift = day_14_F.Content.ToString();
                        valueOfSecondShift = day_14_S.Content.ToString();
                        valueOfThirdShift = day_14_T.Content.ToString();
                        valueOfFourthShift = day_14_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 15:
                        valueOfFirstShift = day_15_F.Content.ToString();
                        valueOfSecondShift = day_15_S.Content.ToString();
                        valueOfThirdShift = day_15_T.Content.ToString();
                        valueOfFourthShift = day_15_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 16:
                        valueOfFirstShift = day_16_F.Content.ToString();
                        valueOfSecondShift = day_16_S.Content.ToString();
                        valueOfThirdShift = day_16_T.Content.ToString();
                        valueOfFourthShift = day_16_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 17:
                        valueOfFirstShift = day_17_F.Content.ToString();
                        valueOfSecondShift = day_17_S.Content.ToString();
                        valueOfThirdShift = day_17_T.Content.ToString();
                        valueOfFourthShift = day_17_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 18:
                        valueOfFirstShift = day_18_F.Content.ToString();
                        valueOfSecondShift = day_18_S.Content.ToString();
                        valueOfThirdShift = day_18_T.Content.ToString();
                        valueOfFourthShift = day_18_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 19:
                        valueOfFirstShift = day_19_F.Content.ToString();
                        valueOfSecondShift = day_19_S.Content.ToString();
                        valueOfThirdShift = day_19_T.Content.ToString();
                        valueOfFourthShift = day_19_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 20:
                        valueOfFirstShift = day_20_F.Content.ToString();
                        valueOfSecondShift = day_20_S.Content.ToString();
                        valueOfThirdShift = day_20_T.Content.ToString();
                        valueOfFourthShift = day_20_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 21:
                        valueOfFirstShift = day_21_F.Content.ToString();
                        valueOfSecondShift = day_21_S.Content.ToString();
                        valueOfThirdShift = day_21_T.Content.ToString();
                        valueOfFourthShift = day_21_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 22:
                        valueOfFirstShift = day_22_F.Content.ToString();
                        valueOfSecondShift = day_22_S.Content.ToString();
                        valueOfThirdShift = day_22_T.Content.ToString();
                        valueOfFourthShift = day_22_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 23:
                        valueOfFirstShift = day_23_F.Content.ToString();
                        valueOfSecondShift = day_23_S.Content.ToString();
                        valueOfThirdShift = day_23_T.Content.ToString();
                        valueOfFourthShift = day_23_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 24:
                        valueOfFirstShift = day_24_F.Content.ToString();
                        valueOfSecondShift = day_24_S.Content.ToString();
                        valueOfThirdShift = day_24_T.Content.ToString();
                        valueOfFourthShift = day_24_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 25:
                        valueOfFirstShift = day_25_F.Content.ToString();
                        valueOfSecondShift = day_25_S.Content.ToString();
                        valueOfThirdShift = day_25_T.Content.ToString();
                        valueOfFourthShift = day_25_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 26:
                        valueOfFirstShift = day_26_F.Content.ToString();
                        valueOfSecondShift = day_26_S.Content.ToString();
                        valueOfThirdShift = day_26_T.Content.ToString();
                        valueOfFourthShift = day_26_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 27:
                        valueOfFirstShift = day_27_F.Content.ToString();
                        valueOfSecondShift = day_27_S.Content.ToString();
                        valueOfThirdShift = day_27_T.Content.ToString();
                        valueOfFourthShift = day_27_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 28:
                        valueOfFirstShift = day_28_F.Content.ToString();
                        valueOfSecondShift = day_28_S.Content.ToString();
                        valueOfThirdShift = day_28_T.Content.ToString();
                        valueOfFourthShift = day_28_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 29:
                        valueOfFirstShift = day_29_F.Content.ToString();
                        valueOfSecondShift = day_29_S.Content.ToString();
                        valueOfThirdShift = day_29_T.Content.ToString();
                        valueOfFourthShift = day_29_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 30:
                        valueOfFirstShift = day_30_F.Content.ToString();
                        valueOfSecondShift = day_30_S.Content.ToString();
                        valueOfThirdShift = day_30_T.Content.ToString();
                        valueOfFourthShift = day_30_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;
                    case 31:
                        valueOfFirstShift = day_31_F.Content.ToString();
                        valueOfSecondShift = day_31_S.Content.ToString();
                        valueOfThirdShift = day_31_T.Content.ToString();
                        valueOfFourthShift = day_31_Fo.Content.ToString();

                        arrayOfValuesOfShifts.Add(valueOfFirstShift);
                        arrayOfValuesOfShifts.Add(valueOfSecondShift);
                        arrayOfValuesOfShifts.Add(valueOfThirdShift);
                        arrayOfValuesOfShifts.Add(valueOfFourthShift);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            return arrayOfValuesOfShifts;
        }

        public void SoundMesaageAboutNotAllParametersAreFilledIn(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (year.Text == "Year" || nameOfMonth.Text == "Name of month")
                    {
                        if (langaugeState == "eng")
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.not_all_parameters_are_filled_in);
                            player.Play();
                            player.Dispose();
                        }
                        else
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.не_все_параметры_выставлены);
                            player.Play();
                            player.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void SettingSoundParameters()
        {
            try
            {
                if (sound.Content == FindResource("soundOn"))
                {
                    soundState = true;
                }
                else
                    soundState = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SettingLanguageParameters()
        {
            try
            {
                if (language.Content == FindResource("eng"))
                {
                    langaugeState = "eng";
                }
                else
                    langaugeState = "ru";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Turning_The_Sound_On_And_Off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sound.Content == FindResource("soundOn"))
                {
                    sound.Content = FindResource("soundOf");
                    sound.ToolTip = "Sound of";
                }
                else
                {
                    sound.Content = FindResource("soundOn");
                    sound.ToolTip = "Sound on";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Turning_The_Language_RU_ENG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (language.Content == FindResource("eng"))
                {
                    language.Content = FindResource("ru");
                    language.ToolTip = "Russian for sound";
                }
                else
                {
                    language.Content = FindResource("eng");
                    language.ToolTip = "English for sound";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StringMessageInEnglish(string message)
        {
            try
            {
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StringMessageInRussian(string message)
        {
            try
            {
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A set of instructions in case to successful search
        /// </summary>
        public void SetOfParameters()
        {
            try
            {

                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutHereIsResult(player, soundState, langaugeState);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A set of instructions in case not all fields on the form are filled in
        /// </summary>
        public void SetOfParameters2()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMesaageAboutNotAllParametersAreFilledIn(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Not all parameters are filled in!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Не все параметры выставлены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SoundMessageAboutHereIsResult(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.here_is_the_result);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.вот_результат);
                        player.Play();
                        player.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
