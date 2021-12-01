using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
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

namespace ProdactionPassControlSystem
{
    /// <summary>
    /// Interaction logic for PassageControl.xaml
    /// </summary>
    public partial class PassageControl : Window, IAddColumnsInGrid, IStringMessage, ISettingSoundParameters, ISettingLanguageParameters,
                                                  ISetOfParameters, ISetOfParameters2, IAudioMessageAboutNotAllParametersAreFilledIn,
                                                  IAudioMessageAboutHereIsResult, ISetOfParameters3, ISetOfParameters4,
                                                  IAudioMessageAboutEnterWorkerId, IAudioMessageAboutThereIsNoWorkerWithSuchIdentificator,
                                                  IAudioMessageAboutTimeIsNotSet, ISetOfParameters5
    {
        private List<string> listOfValuesFromUnChangedInformation;
        private List<string> listOfValuesFromChangedInformation;
        private List<ControlOfTheUseOfThePass> inf;

        private List<int> IdFromChangedInformation;
        private List<int> IdFromUnChangedInformation;

        private DataTable tableFromChangedInformation;
        private DataTable tableFromUnChangedInformation;
        DataTable valuesOfTime;

        private string year_;
        private string month_;
        private int dayNumber_ ;

        private WorkerDAO workerDAO;

        private List<string> sinceWhatTime_P;
        private List<string> tillWhatTime_P;
        private string sinceWhatTime = "";
        private string tillWhatTime = "";
        private string condition = "";
        private string theResultOfUsingThePass = "";

        private int hours;
        private int minutes;
        private int seconds;

        private int indexForP;
        private int indexForP2 = 0;

        private Random random;

        private StringBuilder builder;

        private string timeOfUse = "";

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;

        public PassageControl()
        {
            sinceWhatTime_P = new List<string>();

            tillWhatTime_P = new List<string>();

            IdFromChangedInformation = new List<int>();

            IdFromUnChangedInformation = new List<int>();

            listOfValuesFromUnChangedInformation = new List<string>();

            listOfValuesFromChangedInformation = new List<string>();

            inf = new List<ControlOfTheUseOfThePass>();

            workerDAO = new WorkerDAO();

            random = new Random();

            builder = new StringBuilder(8);

            InitializeComponent();
        }

        private void TimeCheck(string sinceWhatTime, string tillWhatTime, string value)
        {
            int sinceTime = 0;
            int tillTime = 0;

            try
            {
                if (value == "12")
                {
                    sinceWhatTime = CroppingAString(sinceWhatTime);
                    tillWhatTime = CroppingAString(tillWhatTime);

                    sinceTime = ConvertStringToInt(sinceWhatTime);
                    tillTime = ConvertStringToInt(tillWhatTime);

                    if (hours < sinceTime || hours > tillTime)
                    {
                        condition = "False";
                        theResultOfUsingThePass = "-";
                    }
                    else if (hours >= sinceTime && hours < tillTime && minutes > 0)
                    {
                        condition = "True";
                        theResultOfUsingThePass = "+";
                    }
                    else if (hours == tillTime && minutes == 0)
                    {
                        condition = "True";
                        theResultOfUsingThePass = "+";
                    }
                    else if (hours == tillTime && minutes > 0)
                    {
                        condition = "False";
                        theResultOfUsingThePass = "-";
                    }
                }
                
                if (value == "4")
                {
                    sinceWhatTime = CroppingAString(sinceWhatTime);
                    tillWhatTime = CroppingAString(tillWhatTime);

                    sinceTime = ConvertStringToInt(sinceWhatTime);
                    tillTime = ConvertStringToInt(tillWhatTime);

                    if (hours < sinceTime && hours > tillTime)
                    {
                        condition = "False";
                        theResultOfUsingThePass = "-";
                    }
                    else if (hours >= sinceTime && hours <= 23 && minutes > 0)
                    {
                        condition = "True";
                        theResultOfUsingThePass = "+";
                    }
                    else if (hours < tillTime)
                    {
                        condition = "True";
                        theResultOfUsingThePass = "+";
                    }
                    else if (hours == tillTime && minutes == 0)
                    {
                        condition = "True";
                        theResultOfUsingThePass = "+";
                    }
                    else if (hours == tillTime && minutes > 0)
                    {
                        condition = "False";
                        theResultOfUsingThePass = "-";
                    }
                   
                }
                if (value == "8" || value == "D" || value == "V" || value == "S")
                {
                    condition = "False";
                    theResultOfUsingThePass = "-";
                }
                if (value == "P")
                {
                    sinceWhatTime = CroppingAString(sinceWhatTime);
                    tillWhatTime = CroppingAString(tillWhatTime);

                    sinceTime = ConvertStringToInt(sinceWhatTime);
                    tillTime = ConvertStringToInt(tillWhatTime);

                    if (sinceTime >= 8 && tillTime <= 20)
                    {
                        if (hours < sinceTime || hours > tillTime)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                        else if (hours >= sinceTime && hours < tillTime && minutes > 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours == tillTime && minutes == 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours == tillTime && minutes > 0)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                    }

                    if (sinceTime >= 20 && tillTime <= 8)
                    {
                        if (hours < sinceTime && hours > tillTime)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                        else if (hours >= sinceTime && hours <= 23 && minutes > 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours < tillTime)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours == tillTime && minutes == 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours == tillTime && minutes > 0)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                    }

                    if (sinceTime > 0 && sinceTime < 8 && tillTime <= 8)
                    {
                        if (hours >= sinceTime && sinceTime < 8 && hours < 8 && minutes == 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours > 8)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                        else if (hours == tillTime && minutes == 0)
                        {
                            condition = "True";
                            theResultOfUsingThePass = "+";
                        }
                        else if (hours == tillTime && minutes > 0)
                        {
                            condition = "False";
                            theResultOfUsingThePass = "-";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AssigningValues(string value)
        {
            try
            {
                switch (value)
                {
                    case "12":
                        sinceWhatTime = "8:00:00";
                        tillWhatTime = "20:00:00";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "4":
                        sinceWhatTime = "20:00:00";
                        tillWhatTime = "8:00:00";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "8":
                        sinceWhatTime = "-";
                        tillWhatTime = "-";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "D":
                        sinceWhatTime = "-";
                        tillWhatTime = "-";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "V":
                        sinceWhatTime = "-";
                        tillWhatTime = "-";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "S":
                        sinceWhatTime = "-";
                        tillWhatTime = "-";
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
                        break;
                    case "P":
                        if (indexForP2 <= indexForP)
                        {
                            sinceWhatTime = sinceWhatTime_P[indexForP2];
                            tillWhatTime = tillWhatTime_P[indexForP2];
                            indexForP2++;
                        }
                        TimeCheck(sinceWhatTime, tillWhatTime, value);
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

        private void RemoveUnnecessaryWorkerId()
        {
            try
            {
                if (IdFromChangedInformation.Count != 0)
                {
                    for (int i = 0; i < IdFromChangedInformation.Count; i++)
                    {
                        for (int j = 0; j < IdFromUnChangedInformation.Count; j++)
                        {
                            if (IdFromUnChangedInformation[j] == IdFromChangedInformation[i])
                            {
                                IdFromUnChangedInformation.Remove(IdFromChangedInformation[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillingArrayWithWorkesIdFromUnChangedInformation()
        {
            try
            {
                tableFromUnChangedInformation = workerDAO.GetWorkerIdAndNumberOfShift();

                foreach (DataRow row in tableFromUnChangedInformation.Rows)
                {
                    IdFromUnChangedInformation.Add((int)row[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FillingInListFromUnchangedInformation()
        {
            try
            {
                passageControlGrid.Columns.Clear();
                IdFromUnChangedInformation.Clear();

                string[] arrayOfValues = { "12", "4", "8", "D" };
                int[] arrayOfShifNumbers = { 1, 2, 3, 4 };

                timeOfUse = builder.ToString();

                FillingArrayWithWorkesIdFromUnChangedInformation();

                RemoveUnnecessaryWorkerId();

                int count = 0;
                
                foreach (DataRow row in tableFromUnChangedInformation.Rows)
                {
                    for (int i = 0; i < IdFromUnChangedInformation.Count; i++)
                    {
                        if (IdFromUnChangedInformation[i] == (int)row[0])
                        {
                            if (count < IdFromUnChangedInformation.Count)
                            {
                                for (int j = 0; j < arrayOfShifNumbers.Length; j++)
                                {
                                    if ((int)row[1] == arrayOfShifNumbers[j])
                                    {
                                        AssigningValues(listOfValuesFromUnChangedInformation[j]);

                                        inf.Add(new ControlOfTheUseOfThePass(year_, month_, (int)row[0], timeOfUse, theResultOfUsingThePass, dayNumber_, condition, sinceWhatTime,
                                                                             tillWhatTime, listOfValuesFromUnChangedInformation[j])
                                        {
                                            Year = year_,
                                            Month = month_,
                                            WorkerId = IdFromUnChangedInformation[count],
                                            TimeOfUseOfThePass = timeOfUse,
                                            TheResultOfUsingThePass = theResultOfUsingThePass
                                        });
                                    }
                                }
                            }
                            count++;
                        }
                    }
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
                passageControlGrid.ItemsSource = null;

                passageControlGrid.AutoGenerateColumns = false;

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Year",
                    Binding = new Binding("Year")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Month",
                    Binding = new Binding("Month")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Number Of day",
                    Binding = new Binding("NumberOfDay")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Condition",
                    Binding = new Binding("Condition")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Since what time",
                    Binding = new Binding("SinceWhatTime")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Till what time",
                    Binding = new Binding("TillWhatTime")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Value",
                    Binding = new Binding("Value")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Worker id",
                    Binding = new Binding("WorkerId")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Time of use of the pass",
                    Binding = new Binding("TimeOfUseOfThePass")
                });

                passageControlGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "The result of using the pass",
                    Binding = new Binding("TheResultOfUsingThePass")
                });

                passageControlGrid.ItemsSource = inf;

                Save_Result();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_Result()
        {
            ControlOfTheUseOfThePass controlOfTheUseOfThePass = new ControlOfTheUseOfThePass();

            workerDAO.Add_Data_Of_The_Use_Of_A_Pass_By_A_Worker(controlOfTheUseOfThePass, inf, time.Text);
        }

        private int DeterminingTheMonthNumber(string month)
        {
            int numberOfMonth = 0;

            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return numberOfMonth;
        }

        private void ConverInputValue()
        {
            year_ = Year.Text.ToString();
            month_ = nameOfMonth.Text.ToString();
            dayNumber_ = Convert.ToInt32(dayNumber.Text);
        }
        private void GetTheValuesOfShifts()
        {
            try
            {
                ConverInputValue();

                InformationAboutShifts informationAboutShifts = new InformationAboutShifts();

                ScheduleOfShift scheduleOf = new ScheduleOfShift();

                informationAboutShifts.SelectingYear(year_, true);
                informationAboutShifts.SelectingMonth(year_, month_, true);

                int numOfMonth = DeterminingTheMonthNumber(month_);

                int numberOfDaysOfMonth = DateTime.DaysInMonth(Convert.ToInt32(year_), numOfMonth);

                informationAboutShifts.GetInformationAboutShift(scheduleOf, true, numberOfDaysOfMonth);

                listOfValuesFromUnChangedInformation = informationAboutShifts.GetArrayOfValuesOfShifts(dayNumber_);

                FillingInListFromUnchangedInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CroppingAString(string sinceWhatTime)
        {
            return sinceWhatTime.Substring(0, sinceWhatTime.Length - 6);
        }

        private int ConvertStringToInt(string value)
        {
            return Convert.ToInt32(value);
        }

        private StringBuilder RandomTime()
        {
            try
            {
                hours = 0;
                minutes = 0;
                seconds = 0;

                builder.Clear();

                hours = random.Next(1, 24);
                minutes = random.Next(1, 60);
                seconds = random.Next(1, 60);

                if (hours < 10)
                    builder.Append("0");

                builder.Append(hours);
                builder.Append(":");

                if (minutes < 10)
                    builder.Append("0");

                builder.Append(minutes);
                builder.Append(":");

                if (seconds < 10)
                    builder.Append("0");

                builder.Append(seconds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return builder;
        }

        private void See_Result_1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (time.Text == "Time" || time.Text == "")
                {
                    SetOfParameters5();
                }
                else
                {
                    ConverInputValue();

                    inf.Clear();

                    sinceWhatTime_P.Clear();
                    tillWhatTime_P.Clear();

                    indexForP = 0;
                    indexForP2 = 0;

                    tableFromChangedInformation = workerDAO.GetWorkerIdFromChangedInformation(Convert.ToInt32(year_), month_);

                    if (tableFromChangedInformation.Rows.Count != 0)
                    {
                        GetTheValuesOfShiftsFromChangedInformation();
                    }

                    GetTheValuesOfShifts();

                    SetOfParameters2();
                }
            }
            catch
            {
                SetOfParameters();
            }
        }

        private void See_Details_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = workerDAO.AvailabilityOfASpecificWorkerId(Convert.ToInt32(workerId.Text));

                if (count > 0)
                {
                    SetOfParameters2();

                    int id = Convert.ToInt32(workerId.Text);
                    FormForChangingWorkerInformation formForChangingWorkerInformation = new FormForChangingWorkerInformation(id);
                    formForChangingWorkerInformation.ShowDialog();
                }
                else
                {
                    SetOfParameters3();
                }
            }
            catch
            {
                SetOfParameters4();
            }
        }

        private void Generation_Time_Click(object sender, RoutedEventArgs e)
        {
            time.Text = "";
            StringBuilder stringBuilder = RandomTime();
            time.Text = stringBuilder.ToString();
        }

        private void FillingArrayWithWorkesIdFromChangedInformation()
        {
            try
            {
                foreach (DataRow row in tableFromChangedInformation.Rows)
                {
                    IdFromChangedInformation.Add((int)row[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckingForThePresenceOfP(List<string> values)
        {
            try
            {
                for (int i = 0; i < listOfValuesFromChangedInformation.Count; i++)
                {
                    if (listOfValuesFromChangedInformation[i] == "P")
                    {
                        valuesOfTime = workerDAO.GetValuesOfTime(dayNumber_, Convert.ToInt32(year_), month_, IdFromChangedInformation[i]);

                        AssigningValuesForP();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AssigningValuesForP()
        {
            try
            {
                foreach (DataRow row in valuesOfTime.Rows)
                {
                    sinceWhatTime_P.Add(row[0].ToString());
                    tillWhatTime_P.Add(row[1].ToString());
                    indexForP++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetTheValuesOfShiftsFromChangedInformation()
        {
            try
            {
                IdFromChangedInformation.Clear();
                listOfValuesFromChangedInformation.Clear();

                FillingArrayWithWorkesIdFromChangedInformation();

                string value = "";

                ChangeTheWorkShedule changeTheWorkShedule = new ChangeTheWorkShedule();

                for (int i = 0; i < IdFromChangedInformation.Count; i++)
                {
                    changeTheWorkShedule.DisplayingChangedShedule(IdFromChangedInformation[i], Convert.ToInt32(year_), month_, true);

                    value = changeTheWorkShedule.GetValueOfShifts(dayNumber_);

                    listOfValuesFromChangedInformation.Add(value);
                }

                CheckingForThePresenceOfP(listOfValuesFromChangedInformation);

                FillingInListFromChangedInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillingInListFromChangedInformation()
        {
            try
            {
                timeOfUse = builder.ToString();

                int count = 0;

                foreach (DataRow row in tableFromChangedInformation.Rows)
                {
                    if (count < listOfValuesFromChangedInformation.Count)
                    {
                        AssigningValues(listOfValuesFromChangedInformation[count]);

                        inf.Add(new ControlOfTheUseOfThePass(year_, month_, (int)row[0], timeOfUse, theResultOfUsingThePass, dayNumber_, condition, sinceWhatTime,
                                                                                     tillWhatTime, listOfValuesFromChangedInformation[count].ToString())
                        {
                            Year = year_,
                            Month = month_,
                            WorkerId = IdFromChangedInformation[count],
                            TimeOfUseOfThePass = timeOfUse,
                            TheResultOfUsingThePass = theResultOfUsingThePass
                        });
                    }
                    count++;
                }
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

        /// <summary>
        /// A set of instructions in case  of absence of an workerwith such an identifier
        /// </summary>
        public void SetOfParameters3()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutThereIsNoWorkerWithSuchIdentificator(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("There is no worker with such identificator!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Нет работника с таким идентификатором!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A set of instructions in case  the worker ID is not entered
        /// </summary>
        public void SetOfParameters4()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutEnterWorkerId(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Enter the worker ID!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Введите идентификатор работника!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SoundMessageAboutEnterWorkerId(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.enter_the_worker_id);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.введите_идентификаиор_работника);
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

        public void SoundMessageAboutThereIsNoWorkerWithSuchIdentificator(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.there_is_no_worker);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.нет_работника);
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

        public void SoundMessageAboutTimeIsNotSet(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.the_time_is_not_set);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.время_не_установлено);
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
        /// A set of instructions in case when the time is not set
        /// </summary>
        public void SetOfParameters5()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutTimeIsNotSet(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("The time isn't set!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Время не установлено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
