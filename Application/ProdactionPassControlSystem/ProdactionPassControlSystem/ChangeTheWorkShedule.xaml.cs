using LogicClassesLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicClassesLibrary.Entity;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using LogicClassesLibrary.BLL;
using System.Media;

namespace ProdactionPassControlSystem
{
    /// <summary>
    /// Interaction logic for ChangeTheWorkShedule.xaml
    /// </summary>
    /// 


    public partial class ChangeTheWorkShedule : Window, IAddColumnsInGrid, IChoosingTheNumberOfDay, IFillingGrid, IGetValueOfShifts,
                                                        IStringMessage, ISettingSoundParameters, ISettingLanguageParameters,
                                                        ISetOfParameters, ISetOfParameters2, ISetOfParameters3, ISetOfParameters4,
                                                        IAudioMessageAboutNotAllParametersAreFilledIn,
                                                        IAudioMessageAboutHereIsResult, IAudioMessagesAboutSave,
                                                        IAudioMessageAboutWorkerIdOrShiftDoesNotMatch, ISetOfParameters5,
                                                        IAudioMessageAboutCheckWorkerIdentificator
    {
        private WorkerDAO workerDAO;
        private InformationAboutShifts informationAboutShifts;

        private List<FieldsForChangingTheWorkShedule> inf = new List<FieldsForChangingTheWorkShedule>();
        private List<string> condition = new List<string>();
        private List<string> value = new List<string>();

        private FieldsForChangingTheWorkShedule fieldsForChangingTheWorkShedule;
        DataTable table_;

        private int id = 0;
        private int year = 0;
        private string month = "";

        private List<int> dayShift_ = new List<int>();
        private List<int> nightShift_ = new List<int>();
        private List<int> dayOff_ = new List<int>();
        private List<int> endDayOff_ = new List<int>();
        private List<int> vacation_ = new List<int>();
        private List<int> sickLeave_ = new List<int>();
        private List<int> prodactionTasks_ = new List<int>();

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;

        bool setedParameters;

        public ChangeTheWorkShedule()
        {
            InitializeComponent();

            workerDAO = new WorkerDAO();

            dataGrid.ItemsSource = workerDAO.GetTable1().DefaultView;
        }

        public void FillingGrid()
        {
            try
            {
                string sinceWhatTime = "";
                string tillWhatTime = "";
                int count = 0;

                if (day_31_F.Content != null)
                {
                    count = 31;
                }
                else if (day_30_F.Content != null)
                {
                    count = 30;
                }
                else if (day_29_F.Content != null)
                {
                    count = 29;
                }
                else if (day_28_F.Content != null)
                {
                    count = 28;
                }

                for (int i = 1; i <= count; i++)
                {
                    if (value[i - 1] == "12")
                    {
                        sinceWhatTime = "8:00:00";
                        tillWhatTime = "20:00:00";
                    }
                    if (value[i - 1] == "4")
                    {
                        sinceWhatTime = "20:00:00";
                        tillWhatTime = "8:00:00";
                    }
                    if (value[i - 1] == "8" || value[i - 1] == "D")
                    {
                        sinceWhatTime = "-";
                        tillWhatTime = "-";
                    }

                    inf.Add(new FieldsForChangingTheWorkShedule(i, condition[i - 1], sinceWhatTime, tillWhatTime, value[i - 1])
                    {
                        NumberOfDay = i,
                        Condition = condition[i - 1],
                        SinceWhatTime = sinceWhatTime,
                        TillWhatTime = tillWhatTime,
                        Value = value[i - 1]
                    });
                }

                AddColumnsInGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddColumnsInGrid()
        {
            try
            {
                changeGrid.ItemsSource = null;

                changeGrid.AutoGenerateColumns = false;

                changeGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Number Of day",
                    Binding = new Binding("NumberOfDay")
                });

                changeGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Condition",
                    Binding = new Binding("Condition")
                });

                changeGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Since what time",
                    Binding = new Binding("SinceWhatTime")
                });

                changeGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Till what time",
                    Binding = new Binding("TillWhatTime")
                });

                changeGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Value",
                    Binding = new Binding("Value")
                });

                changeGrid.ItemsSource = inf;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddInListArrayOfValue()
        {
            try
            {
                value.Add(day_1_F.Content.ToString());
                value.Add(day_2_F.Content.ToString());
                value.Add(day_3_F.Content.ToString());
                value.Add(day_4_F.Content.ToString());
                value.Add(day_5_F.Content.ToString());
                value.Add(day_6_F.Content.ToString());
                value.Add(day_7_F.Content.ToString());
                value.Add(day_8_F.Content.ToString());
                value.Add(day_9_F.Content.ToString());
                value.Add(day_10_F.Content.ToString());
                value.Add(day_11_F.Content.ToString());
                value.Add(day_12_F.Content.ToString());
                value.Add(day_13_F.Content.ToString());
                value.Add(day_14_F.Content.ToString());
                value.Add(day_15_F.Content.ToString());
                value.Add(day_16_F.Content.ToString());
                value.Add(day_17_F.Content.ToString());
                value.Add(day_18_F.Content.ToString());
                value.Add(day_19_F.Content.ToString());
                value.Add(day_20_F.Content.ToString());
                value.Add(day_21_F.Content.ToString());
                value.Add(day_22_F.Content.ToString());
                value.Add(day_23_F.Content.ToString());
                value.Add(day_24_F.Content.ToString());
                value.Add(day_25_F.Content.ToString());
                value.Add(day_26_F.Content.ToString());
                value.Add(day_27_F.Content.ToString());
                value.Add(day_28_F.Content.ToString());

                if (day_29_F.Content != null)
                    value.Add(day_29_F.Content.ToString());

                if (day_30_F.Content != null)
                    value.Add(day_30_F.Content.ToString());

                if (day_31_F.Content != null)
                    value.Add(day_31_F.Content.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddInListArrayOfCondition()
        {
            try
            {
                condition.Add(con_1.Content.ToString());
                condition.Add(con_2.Content.ToString());
                condition.Add(con_3.Content.ToString());
                condition.Add(con_4.Content.ToString());
                condition.Add(con_5.Content.ToString());
                condition.Add(con_6.Content.ToString());
                condition.Add(con_7.Content.ToString());
                condition.Add(con_8.Content.ToString());
                condition.Add(con_9.Content.ToString());
                condition.Add(con_10.Content.ToString());
                condition.Add(con_11.Content.ToString());
                condition.Add(con_12.Content.ToString());
                condition.Add(con_13.Content.ToString());
                condition.Add(con_14.Content.ToString());
                condition.Add(con_15.Content.ToString());
                condition.Add(con_16.Content.ToString());
                condition.Add(con_17.Content.ToString());
                condition.Add(con_18.Content.ToString());
                condition.Add(con_19.Content.ToString());
                condition.Add(con_20.Content.ToString());
                condition.Add(con_21.Content.ToString());
                condition.Add(con_22.Content.ToString());
                condition.Add(con_23.Content.ToString());
                condition.Add(con_24.Content.ToString());
                condition.Add(con_25.Content.ToString());
                condition.Add(con_26.Content.ToString());
                condition.Add(con_27.Content.ToString());
                condition.Add(con_28.Content.ToString());

                if (day_29_F.Content != null)
                    condition.Add(con_29.Content.ToString());

                if (day_30_F.Content != null)
                    condition.Add(con_30.Content.ToString());

                if (day_31_F.Content != null)
                    condition.Add(con_31.Content.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearDisplay()
        {
            ClearNumberOfDay();
            ClearCondition();
            ClearDays();
            changeGrid.Columns.Clear();
            inf.Clear();
            condition.Clear();
            value.Clear();

            if (table_ != null)
            {
                table_.Clear();
            }

            dayShift_.Clear();
            nightShift_.Clear();
            dayOff_.Clear();
            endDayOff_.Clear();
            vacation_.Clear();
            sickLeave_.Clear();
            prodactionTasks_.Clear();
        }

        private void FillingInListsToDisplayOnTheForm(DataTable table)
        {
            try
            {

                foreach (DataRow t in table.Rows)
                {
                    var values = t.ItemArray;

                    inf.Add(new FieldsForChangingTheWorkShedule((int)values[0], values[1].ToString(), values[2].ToString(),
                                                                                values[3].ToString(), values[4].ToString())
                    {
                        NumberOfDay = (int)values[0],
                        Condition = values[1].ToString(),
                        SinceWhatTime = values[2].ToString(),
                        TillWhatTime = values[3].ToString(),
                        Value = values[4].ToString()
                    });

                    if (values[4].ToString() == "12")
                    {
                        dayShift_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "4")
                    {
                        nightShift_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "8")
                    {
                        dayOff_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "D")
                    {
                        endDayOff_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "V")
                    {
                        vacation_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "S")
                    {
                        sickLeave_.Add((int)values[0]);
                    }
                    else if (values[4].ToString() == "P")
                    {
                        prodactionTasks_.Add((int)values[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// We take the information from the database table where the graph has already been changed
        /// for a specific worker
        /// </summary>
        /// <param name="passageControlWorkerId"></param>
        /// <param name="year_"></param>
        /// <param name="month_"></param>
        /// <param name="control"></param>
        public void DisplayingChangedShedule(int passageControlWorkerId, int year_, string month_, bool control)
        {
            try
            {
                int yeaR = 0;
                string montH = "";
                int iD = 0;

                if (control == false)
                {
                    yeaR = year;
                    montH = month;
                    iD = id;
                }
                else
                {
                    yeaR = year_;
                    montH = month_;
                    iD = passageControlWorkerId;
                }

                ClearDisplay();

                table_ = workerDAO.GetChangedInformation(yeaR, montH, iD);

                FillingInListsToDisplayOnTheForm(table_);

                AddColumnsInGrid();

                List<int> numberOfDay = new List<int>();

                informationAboutShifts = new InformationAboutShifts();
                informationAboutShifts.SelectingSaturdayOrSundayForChangeInformation(yeaR, montH, numberOfDay);

                ChoosingTheNumberOfDay(numberOfDay);
                ChoosingTheDay(dayShift_);
                ChoosingTheNight(nightShift_);
                ChoosingTheDayOff(dayOff_);
                ChoosingTheEndDayOff(endDayOff_);
                ChoosingTheVacation(vacation_);
                ChoosingTheSickLeave(sickLeave_);
                ChoosingTheProdactionTasks(prodactionTasks_);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// We take the information from the database table where the graph has never been changed
        /// for a specific worker
        /// </summary>
        private void DisplayingUnchangedShedule()
        {
            try
            {
                ClearDisplay();

                informationAboutShifts = new InformationAboutShifts();

                int shiftNumber = 0;
                if (numberOfShift.Text == "First shift")
                {
                    shiftNumber = 1;
                }
                if (numberOfShift.Text == "Second shift")
                {
                    shiftNumber = 2;
                }
                if (numberOfShift.Text == "Third shift")
                {
                    shiftNumber = 3;
                }
                if (numberOfShift.Text == "Fourth shift")
                {
                    shiftNumber = 4;
                }

                string year = "";
                int years = 0;

                switch (Year.Text)
                {
                    case "2021":
                        year = "2021";
                        years = 2021;
                        break;
                    case "2022":
                        year = "2022";
                        years = 2022;
                        break;
                    case "2023":
                        year = "2023";
                        years = 2023;
                        break;
                    case "2024":
                        year = "2024";
                        years = 2024;
                        break;
                    case "2025":
                        year = "2025";
                        years = 2025;
                        break;
                    case "2026":
                        year = "2026";
                        years = 2026;
                        break;
                    case "2027":
                        year = "2027";
                        years = 2027;
                        break;
                    case "2028":
                        year = "2028";
                        years = 2028;
                        break;
                    case "2029":
                        year = "2029";
                        years = 2029;
                        break;
                    case "2030":
                        year = "2030";
                        years = 2030;
                        break;
                    default:
                        break;
                }

                string month = "";
                switch (nameOfMonth.Text)
                {
                    case "January":
                        month = "January";
                        break;
                    case "February":
                        month = "February";
                        break;
                    case "March":
                        month = "March";
                        break;
                    case "April":
                        month = "April";
                        break;
                    case "May":
                        month = "May";
                        break;
                    case "June":
                        month = "June";
                        break;
                    case "July":
                        month = "July";
                        break;
                    case "August":
                        month = "August";
                        break;
                    case "September":
                        month = "September";
                        break;
                    case "October":
                        month = "October";
                        break;
                    case "November":
                        month = "November";
                        break;
                    case "December":
                        month = "December";
                        break;
                    default:
                        break;
                }

                List<int> numberOfDay = new List<int>();

                informationAboutShifts.SelectingSaturdayOrSundayForChangeInformation(years, month, numberOfDay);
                informationAboutShifts.ChangingAWorkerSchedule(shiftNumber, year, month);

                List<int> dayShift = informationAboutShifts.numberOfDayShift;
                List<int> nightShift = informationAboutShifts.numberOfNightShift;
                List<int> dayOff = informationAboutShifts.numberOfDayOff;
                List<int> dayOfEndDayOff = informationAboutShifts.numberOfEndDayOff;

                ChoosingTheNumberOfDay(numberOfDay);
                ChoosingTheDay(dayShift);
                ChoosingTheNight(nightShift);
                ChoosingTheDayOff(dayOff);
                ChoosingTheEndDayOff(dayOfEndDayOff);

                AddInListArrayOfValue();
                AddInListArrayOfCondition();

                FillingGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void See_Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConvertSomeValue();

                string numOfShift = numberOfShift.Text;
                bool compare = ShiftComparison(id, numOfShift);

                if (compare && setedParameters == true)
                {
                    int check = workerDAO.ChekingForChangedDataInTheDatabase(year, month, id);

                    if (check > 0)
                    {
                        DisplayingChangedShedule(0, 0, "", false);
                    }
                    else
                    {
                        DisplayingUnchangedShedule();
                    }

                    SetOfParameters2();
                }
                else if (compare == false && setedParameters == true)
                {
                    SetOfParameters4();
                }
            }
            catch
            {
                SetOfParameters5();
            }
        }

        private bool ShiftComparison(int workerId, string numberOfShift)
        {
            DataTable table = workerDAO.GetTable1();
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    var values = row.ItemArray;

                    if (numberOfShift == "First shift" && (int)values[4] == 1 && workerId == (int)values[0])
                    {
                        return true;
                    }
                    if (numberOfShift == "Second shift" && (int)values[4] == 2 && workerId == (int)values[0])
                    {
                        return true;
                    }
                    if (numberOfShift == "Third shift" && (int)values[4] == 3 && workerId == (int)values[0])
                    {
                        return true;
                    }
                    if (numberOfShift == "Fourth shift" && (int)values[4] == 4 && workerId == (int)values[0])
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        //private void Get_Information_About_Workers()
        //{
        //    dataGrid.ItemsSource = workerDAO.GetTable1().DefaultView;
        //}

        private void ConvertSomeValue()
        {
            if (workerId.Text == "" || Year.Text == "Year" || nameOfMonth.Text == "Name of month" || numberOfShift.Text == "Number of shift")
            {
                SetOfParameters();
                setedParameters = false;
            }
            else
            {
                id = Convert.ToInt32(workerId.Text);
                year = Convert.ToInt32(Year.Text);
                month = Convert.ToString(nameOfMonth.Text);

                setedParameters = true;
            }
        }

        private void Save_Changed_Information_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConvertSomeValue();

                fieldsForChangingTheWorkShedule = new FieldsForChangingTheWorkShedule();

                int check = workerDAO.ChekingForChangedDataInTheDatabase(year, month, id);

                if (check > 0)
                {
                    inf = (List<FieldsForChangingTheWorkShedule>)changeGrid.ItemsSource;

                    workerDAO.UpdateChangedShedule(fieldsForChangingTheWorkShedule, id, inf, year, month);

                    SetOfParameters3();

                    DisplayingChangedShedule(0, 0, "", false);
                }
                else
                {
                    inf = (List<FieldsForChangingTheWorkShedule>)changeGrid.ItemsSource;

                    workerDAO.AddFieldsForChanging(fieldsForChangingTheWorkShedule, id, inf, year, month);

                    SetOfParameters3();

                    DisplayingChangedShedule(0, 0, "", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PassUnlocked(int[] number)
        {
            try
            {
                for (int i = 0; i < number.Length; i++)
                {
                    switch (number[i])
                    {
                        case 1:
                            con_1.Content = "True";
                            con_1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 2:
                            con_2.Content = "True";
                            con_2.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 3:
                            con_3.Content = "True";
                            con_3.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 4:
                            con_4.Content = "True";
                            con_4.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 5:
                            con_5.Content = "True";
                            con_5.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 6:
                            con_6.Content = "True";
                            con_6.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 7:
                            con_7.Content = "True";
                            con_7.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 8:
                            con_8.Content = "True";
                            con_8.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 9:
                            con_9.Content = "True";
                            con_9.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 10:
                            con_10.Content = "True";
                            con_10.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 11:
                            con_11.Content = "True";
                            con_11.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 12:
                            con_12.Content = "True";
                            con_12.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 13:
                            con_13.Content = "True";
                            con_13.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 14:
                            con_14.Content = "True";
                            con_14.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 15:
                            con_15.Content = "True";
                            con_15.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 16:
                            con_16.Content = "True";
                            con_16.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 17:
                            con_17.Content = "True";
                            con_17.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 18:
                            con_18.Content = "True";
                            con_18.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 19:
                            con_19.Content = "True";
                            con_19.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 20:
                            con_20.Content = "True";
                            con_20.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 21:
                            con_21.Content = "True";
                            con_21.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 22:
                            con_22.Content = "True";
                            con_22.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 23:
                            con_23.Content = "True";
                            con_23.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 24:
                            con_24.Content = "True";
                            con_24.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 25:
                            con_25.Content = "True";
                            con_25.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 26:
                            con_26.Content = "True";
                            con_26.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 27:
                            con_27.Content = "True";
                            con_27.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 28:
                            con_28.Content = "True";
                            con_28.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 29:
                            con_29.Content = "True";
                            con_29.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 30:
                            con_30.Content = "True";
                            con_30.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 31:
                            con_31.Content = "True";
                            con_31.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
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

        private void PassLocked(int[] number)
        {
            try
            {
                for (int i = 0; i < number.Length; i++)
                {
                    switch (number[i])
                    {
                        case 1:
                            con_1.Content = "False";
                            con_1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 2:
                            con_2.Content = "False";
                            con_2.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 3:
                            con_3.Content = "False";
                            con_3.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 4:
                            con_4.Content = "False";
                            con_4.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 5:
                            con_5.Content = "False";
                            con_5.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 6:
                            con_6.Content = "False";
                            con_6.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 7:
                            con_7.Content = "False";
                            con_7.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 8:
                            con_8.Content = "False";
                            con_8.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 9:
                            con_9.Content = "False";
                            con_9.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 10:
                            con_10.Content = "False";
                            con_10.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 11:
                            con_11.Content = "False";
                            con_11.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 12:
                            con_12.Content = "False";
                            con_12.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 13:
                            con_13.Content = "False";
                            con_13.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 14:
                            con_14.Content = "False";
                            con_14.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 15:
                            con_15.Content = "False";
                            con_15.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 16:
                            con_16.Content = "False";
                            con_16.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 17:
                            con_17.Content = "False";
                            con_17.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 18:
                            con_18.Content = "False";
                            con_18.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 19:
                            con_19.Content = "False";
                            con_19.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 20:
                            con_20.Content = "False";
                            con_20.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 21:
                            con_21.Content = "False";
                            con_21.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 22:
                            con_22.Content = "False";
                            con_22.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 23:
                            con_23.Content = "False";
                            con_23.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 24:
                            con_24.Content = "False";
                            con_24.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 25:
                            con_25.Content = "False";
                            con_25.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 26:
                            con_26.Content = "False";
                            con_26.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 27:
                            con_27.Content = "False";
                            con_27.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 28:
                            con_28.Content = "False";
                            con_28.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 29:
                            con_29.Content = "False";
                            con_29.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 30:
                            con_30.Content = "False";
                            con_30.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            break;
                        case 31:
                            con_31.Content = "False";
                            con_31.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
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

        private void ClearCondition()
        {
            try
            {
                con_1.Content = "";
                con_1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_2.Content = "";
                con_2.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_3.Content = "";
                con_3.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_4.Content = "";
                con_4.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_5.Content = "";
                con_5.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_6.Content = "";
                con_6.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_7.Content = "";
                con_7.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_8.Content = "";
                con_8.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_9.Content = "";
                con_9.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_10.Content = "";
                con_10.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_11.Content = "";
                con_11.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_12.Content = "";
                con_12.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_13.Content = "";
                con_13.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_14.Content = "";
                con_14.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_15.Content = "";
                con_15.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_16.Content = "";
                con_16.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_17.Content = "";
                con_17.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_18.Content = "";
                con_18.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_19.Content = "";
                con_19.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_20.Content = "";
                con_20.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_21.Content = "";
                con_21.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_22.Content = "";
                con_22.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_23.Content = "";
                con_23.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_24.Content = "";
                con_24.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_25.Content = "";
                con_25.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_26.Content = "";
                con_26.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_27.Content = "";
                con_27.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_28.Content = "";
                con_28.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_29.Content = "";
                con_29.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_30.Content = "";
                con_30.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                con_31.Content = "";
                con_31.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearDays()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheProdactionTasks(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = "P";
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 2:
                            day_2_F.Content = "P";
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 3:
                            day_3_F.Content = "P";
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 4:
                            day_4_F.Content = "P";
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 5:
                            day_5_F.Content = "P";
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 6:
                            day_6_F.Content = "P";
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 7:
                            day_7_F.Content = "P";
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 8:
                            day_8_F.Content = "P";
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 9:
                            day_9_F.Content = "P";
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 10:
                            day_10_F.Content = "P";
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 11:
                            day_11_F.Content = "P";
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 12:
                            day_12_F.Content = "P";
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 13:
                            day_13_F.Content = "P";
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 14:
                            day_14_F.Content = "P";
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 15:
                            day_15_F.Content = "P";
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 16:
                            day_16_F.Content = "P";
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 17:
                            day_17_F.Content = "P";
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 18:
                            day_18_F.Content = "P";
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 19:
                            day_19_F.Content = "P";
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 20:
                            day_20_F.Content = "P";
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 21:
                            day_21_F.Content = "P";
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 22:
                            day_22_F.Content = "P";
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 23:
                            day_23_F.Content = "P";
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 24:
                            day_24_F.Content = "P";
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 25:
                            day_25_F.Content = "P";
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 26:
                            day_26_F.Content = "P";
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 27:
                            day_27_F.Content = "P";
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 28:
                            day_28_F.Content = "P";
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 29:
                            day_29_F.Content = "P";
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 30:
                            day_30_F.Content = "P";
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        case 31:
                            day_31_F.Content = "P";
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(128, 0, 0));
                            break;
                        default:
                            break;
                    }
                }
                PassUnlocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheSickLeave(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = "S";
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 2:
                            day_2_F.Content = "S";
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 3:
                            day_3_F.Content = "S";
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 4:
                            day_4_F.Content = "S";
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 5:
                            day_5_F.Content = "S";
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 6:
                            day_6_F.Content = "S";
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 7:
                            day_7_F.Content = "S";
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 8:
                            day_8_F.Content = "S";
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 9:
                            day_9_F.Content = "S";
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 10:
                            day_10_F.Content = "S";
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 11:
                            day_11_F.Content = "S";
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 12:
                            day_12_F.Content = "S";
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 13:
                            day_13_F.Content = "S";
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 14:
                            day_14_F.Content = "S";
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 15:
                            day_15_F.Content = "S";
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 16:
                            day_16_F.Content = "S";
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 17:
                            day_17_F.Content = "S";
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 18:
                            day_18_F.Content = "S";
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 19:
                            day_19_F.Content = "S";
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 20:
                            day_20_F.Content = "S";
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 21:
                            day_21_F.Content = "S";
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 22:
                            day_22_F.Content = "S";
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 23:
                            day_23_F.Content = "S";
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 24:
                            day_24_F.Content = "S";
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 25:
                            day_25_F.Content = "S";
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 26:
                            day_26_F.Content = "S";
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 27:
                            day_27_F.Content = "S";
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 28:
                            day_28_F.Content = "S";
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 29:
                            day_29_F.Content = "S";
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 30:
                            day_30_F.Content = "S";
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        case 31:
                            day_31_F.Content = "S";
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                            break;
                        default:
                            break;
                    }
                }
                PassLocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheVacation(List<int> number)
        {
            try
            {
                int[] num = number.ToArray();

                for (int i = 0; i < num.Length; i++)
                {
                    switch (num[i])
                    {
                        case 1:
                            day_1_F.Content = "V";
                            day_1_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 2:
                            day_2_F.Content = "V";
                            day_2_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 3:
                            day_3_F.Content = "V";
                            day_3_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 4:
                            day_4_F.Content = "V";
                            day_4_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 5:
                            day_5_F.Content = "V";
                            day_5_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 6:
                            day_6_F.Content = "V";
                            day_6_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 7:
                            day_7_F.Content = "V";
                            day_7_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 8:
                            day_8_F.Content = "V";
                            day_8_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 9:
                            day_9_F.Content = "V";
                            day_9_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 10:
                            day_10_F.Content = "V";
                            day_10_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 11:
                            day_11_F.Content = "V";
                            day_11_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 12:
                            day_12_F.Content = "V";
                            day_12_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 13:
                            day_13_F.Content = "V";
                            day_13_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 14:
                            day_14_F.Content = "V";
                            day_14_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 15:
                            day_15_F.Content = "V";
                            day_15_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 16:
                            day_16_F.Content = "V";
                            day_16_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 17:
                            day_17_F.Content = "V";
                            day_17_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 18:
                            day_18_F.Content = "V";
                            day_18_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 19:
                            day_19_F.Content = "V";
                            day_19_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 20:
                            day_20_F.Content = "V";
                            day_20_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 21:
                            day_21_F.Content = "V";
                            day_21_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 22:
                            day_22_F.Content = "V";
                            day_22_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 23:
                            day_23_F.Content = "V";
                            day_23_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 24:
                            day_24_F.Content = "V";
                            day_24_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 25:
                            day_25_F.Content = "V";
                            day_25_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 26:
                            day_26_F.Content = "V";
                            day_26_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 27:
                            day_27_F.Content = "V";
                            day_27_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 28:
                            day_28_F.Content = "V";
                            day_28_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 29:
                            day_29_F.Content = "V";
                            day_29_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 30:
                            day_30_F.Content = "V";
                            day_30_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        case 31:
                            day_31_F.Content = "V";
                            day_31_F.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            break;
                        default:
                            break;
                    }
                }
                PassLocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDay(List<int> number)
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
                PassUnlocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheNight(List<int> number)
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
                PassUnlocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheDayOff(List<int> number)
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
                PassLocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChoosingTheEndDayOff(List<int> number)
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
                PassLocked(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetValueOfShifts(int dayNumber)
        {
            string valueOfFirstShift = "";

            try
            {
                switch (dayNumber)
                {
                    case 1:
                        valueOfFirstShift = day_1_F.Content.ToString();
                        break;

                    case 2:
                        valueOfFirstShift = day_2_F.Content.ToString();
                        break;

                    case 3:
                        valueOfFirstShift = day_3_F.Content.ToString();
                        break;

                    case 4:
                        valueOfFirstShift = day_4_F.Content.ToString();
                        break;
                    case 5:
                        valueOfFirstShift = day_5_F.Content.ToString();
                        break;
                    case 6:
                        valueOfFirstShift = day_6_F.Content.ToString();
                        break;
                    case 7:
                        valueOfFirstShift = day_7_F.Content.ToString();
                        break;
                    case 8:
                        valueOfFirstShift = day_8_F.Content.ToString();
                        break;
                    case 9:
                        valueOfFirstShift = day_9_F.Content.ToString();
                        break;
                    case 10:
                        valueOfFirstShift = day_10_F.Content.ToString();
                        break;
                    case 11:
                        valueOfFirstShift = day_11_F.Content.ToString();
                        break;
                    case 12:
                        valueOfFirstShift = day_12_F.Content.ToString();
                        break;
                    case 13:
                        valueOfFirstShift = day_13_F.Content.ToString();
                        break;
                    case 14:
                        valueOfFirstShift = day_14_F.Content.ToString();
                        break;
                    case 15:
                        valueOfFirstShift = day_15_F.Content.ToString();
                        break;
                    case 16:
                        valueOfFirstShift = day_16_F.Content.ToString();
                        break;
                    case 17:
                        valueOfFirstShift = day_17_F.Content.ToString();
                        break;
                    case 18:
                        valueOfFirstShift = day_18_F.Content.ToString();
                        break;
                    case 19:
                        valueOfFirstShift = day_19_F.Content.ToString();
                        break;
                    case 20:
                        valueOfFirstShift = day_20_F.Content.ToString();
                        break;
                    case 21:
                        valueOfFirstShift = day_21_F.Content.ToString();
                        break;
                    case 22:
                        valueOfFirstShift = day_22_F.Content.ToString();
                        break;
                    case 23:
                        valueOfFirstShift = day_23_F.Content.ToString();
                        break;
                    case 24:
                        valueOfFirstShift = day_24_F.Content.ToString();
                        break;
                    case 25:
                        valueOfFirstShift = day_25_F.Content.ToString();
                        break;
                    case 26:
                        valueOfFirstShift = day_26_F.Content.ToString();
                        break;
                    case 27:
                        valueOfFirstShift = day_27_F.Content.ToString();
                        break;
                    case 28:
                        valueOfFirstShift = day_28_F.Content.ToString();
                        break;
                    case 29:
                        valueOfFirstShift = day_29_F.Content.ToString();
                        break;
                    case 30:
                        valueOfFirstShift = day_30_F.Content.ToString();
                        break;
                    case 31:
                        valueOfFirstShift = day_31_F.Content.ToString();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return valueOfFirstShift;
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

        public void SoundMesaageAboutNotAllParametersAreFilledIn(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        public void SoundMessageAboutSave(SoundPlayer player, bool soundState, string langugeState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.saved);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.сохранено);
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

        /// <summary>
        /// A set of instructions in case not all fields on the form are filled in
        /// </summary>
        public void SetOfParameters()
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

        /// <summary>
        /// A set of instructions in case to successful search
        /// </summary>
        public void SetOfParameters2()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutHereIsResult(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Here is the result!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Вот результат!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A set of instructions in case of successful saving of information
        /// </summary>
        public void SetOfParameters3()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutSave(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Saved!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Сохранено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A set of instructions in case the entered data does not match
        /// </summary>
        public void SetOfParameters4()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutWorkerIdOrShiftDoesNotMatch(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Worker id or number of shift doesn't match!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Идентификатор работника или номер смены не совпадают!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SoundMessageAboutWorkerIdOrShiftDoesNotMatch(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.doesn_t_match);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.не_совпадают);
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

        /// <summary>
        /// A set of instructions in case of data entry error
        /// </summary>
        public void SetOfParameters5()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutCheckWorkerIdentificator(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Please, check the worker identificator, you may have made a mistake there!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Пожалуйста, проверьте идентификатор работника, возможно, вы там ошиблись!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SoundMessageAboutCheckWorkerIdentificator(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.check_worker_id);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.проверить_идентификатор_работника);
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
