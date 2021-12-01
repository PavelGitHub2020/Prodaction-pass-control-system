using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicClassesLibrary.Entity;
using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using System.Windows.Controls;
using System.Media;
using System.Windows.Media;
using System.Collections.Generic;

namespace ProdactionPassControlSystem
{
    /// <summary>
    /// Interaction logic for AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window, IAudioMessagesAboutSave, IAudioMessageAboutNotAllParametersAreFilledIn, IStringMessage,
                                             ISettingSoundParameters, ISettingLanguageParameters, ISetOfParameters, ISetOfParameters2
    {
        private Address address;
        private Worker worker;
        private Pass pass;
        private Photo photos;

        private System.Media.SoundPlayer player;

        private static readonly IWorkerDAO workerDAO;
        private static readonly IAddressDAO addressDAO;
        private static readonly IPassDAO passDAO;
        private static readonly IPhotoDAO photoDAO;

        private OpenFileDialog op;

        private static Random rnd = new Random();

        private int workId;

        private bool conditions;

        private bool soundState;
        private string langaugeState;

        static AddWorker()
        {
            workerDAO = new WorkerDAO();
            addressDAO = new AddressDAO();
            passDAO = new PassDAO();
            photoDAO = new PhotoDAO();
        }
        public AddWorker()
        {
            InitializeComponent();
        }

        private void Upload_Photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                op = new OpenFileDialog();

                op.FileName = "";

                op.DefaultExt = ".png";

                op.Title = "Select a picture";

                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";

                if (op.ShowDialog() == true)
                {
                    image.Source = new BitmapImage(new Uri(op.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetNumOfMonth(string value)
        {
            string numOfMonth = "";

            try
            {
                switch (value)
                {
                    case "January":
                        numOfMonth = "1";
                        break;
                    case "February":
                        numOfMonth = "2";
                        break;
                    case "March":
                        numOfMonth = "3";
                        break;
                    case "April":
                        numOfMonth = "4";
                        break;
                    case "May":
                        numOfMonth = "5";
                        break;
                    case "June":
                        numOfMonth = "6";
                        break;
                    case "July":
                        numOfMonth = "7";
                        break;
                    case "August":
                        numOfMonth = "8";
                        break;
                    case "September":
                        numOfMonth = "9";
                        break;
                    case "October":
                        numOfMonth = "10";
                        break;
                    case "November":
                        numOfMonth = "11";
                        break;
                    case "December":
                        numOfMonth = "12";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return numOfMonth;
        }
        private void CreateOfInstanceOfWorker()
        {
            try
            {
                string srm = surname.Text;
                string nm = name.Text;
                string ptr = patronymic.Text;

                string numOfMonthOfBirth = GetNumOfMonth(monthDateOfBirth.Text);
                string dob = yearDateOfBirth.Text + "-" + numOfMonthOfBirth + "-" + dayOfDateOfBirth.Text;

                string phoneNum = codeOfCountry.Text + mobileOperator.Text + phoneNumber.Text;

                bool gender = true;

                if ((bool)man.IsChecked)
                {
                    gender = true;
                }
                if ((bool)women.IsChecked)
                {
                    gender = false;
                }

                int departmentId = 0;

                if (nameOfDepartment.SelectedItem == service_M)
                {
                    departmentId = 1;
                }
                if (nameOfDepartment.SelectedItem == service_H)
                {
                    departmentId = 2;
                }
                if (nameOfDepartment.SelectedItem == traffic_Service)
                {
                    departmentId = 3;
                }
                if (nameOfDepartment.SelectedItem == electro_Mechanical_Service)
                {
                    departmentId = 4;
                }
                if (nameOfDepartment.SelectedItem == security_Service)
                {
                    departmentId = 5;
                }
                if (nameOfDepartment.SelectedItem == economic_Department)
                {
                    departmentId = 6;
                }
                if (nameOfDepartment.SelectedItem == computer_Department)
                {
                    departmentId = 7;
                }

                string profess = profession.Text;

                string numOfMonth = GetNumOfMonth(monthSt.Text);
                
                string dateOfStWork = yearSt.Text + "-" + numOfMonth + "-" + daySt.Text;///

                int numOfShift = 0;

                if (numberOfShift.SelectedItem == firstShift)
                {
                    numOfShift = 1;
                }
                if (numberOfShift.SelectedItem == secondShift)
                {
                    numOfShift = 2;
                }
                if (numberOfShift.SelectedItem == thirdShift)
                {
                    numOfShift = 3;
                }
                if (numberOfShift.SelectedItem == fourthShift)
                {
                    numOfShift = 4;
                }

                worker = new Worker(0, srm, nm, ptr, dob, gender, phoneNum, departmentId, profess, dateOfStWork, numOfShift);
                workerDAO.Add(worker);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateOfInstanceOfAddress()
        {
            try
            {
                string city = nameOfTheCity.Text;
                string street = nameOfTheStreet.Text;
                string number = houseNumber.Text;
                workId = workerDAO.GetMaxId();

                address = new Address(0, city, street, number, workId);
                addressDAO.Add(address);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateOfInstanceOfPass()
        {
            try
            {
                string numPass = passNumber.Text;
                bool cond = true;

                if (condition.Text == unlocked.Text)
                {
                    cond = true;
                }
                if (condition.Text == locked.Text)
                {
                    cond = false;
                }

                pass = new Pass(0, numPass, cond, workId);
                passDAO.Add(pass);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateOfInstanceOfPhoto()
        {
            try
            {
                photos = new Photo(0, op.FileName, workId);
                photoDAO.Add(photos);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                conditions = false;

                СheckingWhetherAllFieldsAreFilledIn();

                if (conditions == true)
                {
                    CreateOfInstanceOfWorker();
                    CreateOfInstanceOfAddress();
                    CreateOfInstanceOfPass();
                    CreateOfInstanceOfPhoto();

                    SetOfParameters();

                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateAPass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int letters = 5;
                int numbers = 5;

                StringBuilder sb = new StringBuilder(letters + numbers);

                string letterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefgijklmnopqrstuvwxyz";
                string numberSet = "0123456789";

                for (int i = 0; i < letters; i++)
                    sb.Append(letterSet[rnd.Next(letterSet.Length)]);
                for (int i = 0; i < numbers; i++)
                    sb.Append(numberSet[rnd.Next(numberSet.Length)]);

                passNumber.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            surname.Text = "";
            name.Text = "";
            patronymic.Text = "";
            yearDateOfBirth.SelectedIndex = 0;
            monthDateOfBirth.SelectedIndex = 0;
            dayOfDateOfBirth.SelectedIndex = 0;
            codeOfCountry.SelectedIndex = 0;
            mobileOperator.SelectedIndex = 0;
            phoneNumber.Text = "";
            nameOfTheCity.Text = "";
            nameOfTheStreet.Text = "";
            houseNumber.Text = "";
            nameOfDepartment.Text = "";
            profession.Text = "";
            yearSt.SelectedIndex = 0;
            monthSt.SelectedIndex = 0;
            daySt.SelectedIndex = 0;
            passNumber.Text = "";
            numberOfShift.SelectedIndex = 0;
            image.Source = null;
            condition.SelectedIndex = 0;
        }

        private void СheckingWhetherAllFieldsAreFilledIn()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(surname.Text) || string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(patronymic.Text)
                    || string.IsNullOrWhiteSpace(phoneNumber.Text) || string.IsNullOrWhiteSpace(nameOfTheCity.Text) || string.IsNullOrWhiteSpace(nameOfTheStreet.Text)
                    || string.IsNullOrWhiteSpace(houseNumber.Text) || string.IsNullOrWhiteSpace(nameOfDepartment.Text)
                    || string.IsNullOrWhiteSpace(profession.Text) || string.IsNullOrWhiteSpace(nameOfTheCity.Text)
                    || string.IsNullOrWhiteSpace(passNumber.Text) || string.IsNullOrWhiteSpace(nameOfTheCity.Text)
                    || yearDateOfBirth.SelectedIndex == 0 || monthDateOfBirth.SelectedIndex == 0 || dayOfDateOfBirth.SelectedIndex == 0
                    || numberOfShift.SelectedIndex == 0 || yearSt.SelectedIndex == 0 || monthSt.SelectedIndex == 0
                    || daySt.SelectedIndex == 0 || condition.SelectedIndex == 0 || image.Source == null)
                {
                    SetOfParameters2();
                }
                else
                {
                    conditions = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// A set of instructions in case of successful saving of information
        /// </summary>
        public void SetOfParameters()
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
    }
}
