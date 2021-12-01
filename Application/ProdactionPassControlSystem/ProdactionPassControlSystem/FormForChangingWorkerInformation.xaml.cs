using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using LogicClassesLibrary.Entity;
using Microsoft.Win32;
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
    /// Interaction logic for FormForChangingWorkerInformation.xaml
    /// </summary>
    public partial class FormForChangingWorkerInformation : Window, IStringMessage, ISettingSoundParameters,ISettingLanguageParameters,
                                                                    IAudioMessageAboutInformationAboutWorkerChanged,
                                                                    ISetOfParameters
    {
        private OpenFileDialog op;
        private DataTable table;

        private WorkerDAO workerDAO;
        private AddressDAO addressDAO;
        private PassDAO passDAO;
        private PhotoDAO photoDAO;
        
        private Address address;
        private Worker worker;
        private Pass pass;
        private Photo photos;

        private int workerId;
        private int addressId;
        private int passId;
        private int photosId;
        private int departmentId;

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;

        int counter = 0;

        public FormForChangingWorkerInformation(int id)
        {
            InitializeComponent();

            workerDAO = new WorkerDAO();
            table = workerDAO.GetWorkerById(id);

            addressDAO = new AddressDAO();
            passDAO = new PassDAO();
            photoDAO = new PhotoDAO();

            GetInformationFromTable(table);
        }

        private void GetInformationFromTable(DataTable table)
        {
            try
            {
                foreach (DataRow dataTable in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        workerId = (int)dataTable[0];
                        surnameUp.Text = dataTable[1].ToString();
                        nameUp.Text = dataTable[2].ToString();
                        patronymicUp.Text = dataTable[3].ToString();
                        dateOfBirthUp.Text = dataTable[4].ToString();
                        genderUp.Text = dataTable[5].ToString();
                        phonenumberUp.Text = dataTable[6].ToString();
                        professionUp.Text = dataTable[8].ToString();
                        dateOfStartToWorkUp.Text = dataTable[9].ToString();

                        if (dataTable[10].ToString() == "1")
                        {
                            numberOfShiftUp.Text = "First shift";
                        }
                        if (dataTable[10].ToString() == "2")
                        {
                            numberOfShiftUp.Text = "Second shift";
                        }
                        if (dataTable[10].ToString() == "3")
                        {
                            numberOfShiftUp.Text = "Third shift";
                        }
                        if (dataTable[10].ToString() == "4")
                        {
                            numberOfShiftUp.Text = "Fourth shift";
                        }

                        addressId = (int)dataTable[11];
                        nameOfTheCityUp.Text = dataTable[12].ToString();
                        nameOfTheStreetUp.Text = dataTable[13].ToString();
                        houseNumberUp.Text = dataTable[14].ToString();

                        passId = (int)dataTable[16];
                        passNumberUp.Text = dataTable[17].ToString();

                        if ((bool)dataTable[18] == true)
                        {
                            conditionUp.Text = "Unlocked";
                        }
                        else
                        {
                            conditionUp.Text = "Locked";
                        }

                        departmentId = (int)dataTable[20];
                        nameOfDepartmentUp.Text = dataTable[21].ToString();

                        photosId = (int)dataTable[22];
                        image.Source = new BitmapImage(new Uri(dataTable[23].ToString()));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    MessageBox.Show(op.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetOfParameters();

                string surname = surnameUp.Text;
                string name = nameUp.Text;
                string paatronymic = patronymicUp.Text;
                string dateOfBirth = dateOfBirthUp.Text;
                bool gender = Convert.ToBoolean(genderUp.Text);
                string phoneNumber = phonenumberUp.Text;
                string profession = professionUp.Text;
                string dateOfStartToWork = dateOfBirthUp.Text;

                int numberOfShift = 0;

                switch (numberOfShiftUp.Text)
                {
                    case "First shift":
                        numberOfShift = 1;
                        break;
                    case "Second shift":
                        numberOfShift = 2;
                        break;
                    case "Third shift":
                        numberOfShift = 3;
                        break;
                    case "Fourth shift":
                        numberOfShift = 4;
                        break;
                    default:
                        break;
                }

                switch (nameOfDepartmentUp.Text)
                {
                    case "Service M":
                        departmentId = 1;
                        break;
                    case "Service H":
                        departmentId = 2;
                        break;
                    case "Traffic service":
                        departmentId = 3;
                        break;
                    case "Electro-mechanical service":
                        departmentId = 4;
                        break;
                    case "Security service":
                        departmentId = 5;
                        break;
                    case "Economic department":
                        departmentId = 6;
                        break;
                    case "Computer department":
                        departmentId = 7;
                        break;
                    default:
                        break;
                }

                worker = new Worker(workerId, surname, name, paatronymic, dateOfBirth, gender, phoneNumber, departmentId, profession, dateOfStartToWork, numberOfShift);

                string nameOfTheCity = nameOfTheCityUp.Text;
                string nameOfTheStreet = nameOfTheStreetUp.Text;
                string houseNumber = houseNumberUp.Text;
                address = new Address(addressId, nameOfTheCity, nameOfTheStreet, houseNumber, workerId);

                string passNumber = passNumberUp.Text;
                bool condition = false;

                switch (conditionUp.Text)
                {
                    case "Unlocked":
                        condition = true;
                        break;
                    case "Locked":
                        condition = false;
                        break;
                    default:
                        break;
                }

                pass = new Pass(passId, passNumber, condition, workerId);

                string path = image.Source.ToString();
                photos = new Photo(photosId, path, workerId);

                workerDAO.Update(worker);
                addressDAO.Update(address);
                passDAO.Update(pass);
                photoDAO.Update(photos);

                this.Close();
            }
            catch(Exception ex)
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

                    if (counter == 0)
                    {
                        counter++;
                    }
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
        /// A set of instructions in case of a change in worker information
        /// </summary>
        public void SetOfParameters()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();
                SoundMessageAboutInformationAboutWorkerChanged(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Worker information changed!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Информация о работнике изменена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SoundMessageAboutInformationAboutWorkerChanged(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.worker_information_changed);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.информацияя_о_работнике_изменена);
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
