using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using LogicClassesLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for FindWorker.xaml
    /// </summary>
    public partial class FindWorker : Window, IAudioMessageAboutNotAllParametersAreFilledIn, IStringMessage,
                                              ISettingSoundParameters, ISettingLanguageParameters, IAudioMessageAboutNothingWasFound,
                                              ISetOfParameters, ISetOfParameters2
    {
        private WorkerDAO workerDAO;

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;
        public FindWorker()
        {
            workerDAO = new WorkerDAO();

            InitializeComponent();

            Get_Surname_DepartmentName_PassNumber();
        }

        private void ClearParameters()
        {
            surname.Text = "";
            name.Text = "";
            patronymic.Text = "";
            dateOfBirth.Text = "";
            gender.Text = "";
            phoneNumber.Text = "";
            profession.Text = "";
            dateOfStartToWork.Text = "";
            numberOfShift.Text = "";
            nameOfTheCity.Text = "";
            nameOfTheStreet.Text = "";
            houseNumber.Text = "";
            nameOfTheCity.Text = "";
            nameOfTheStreet.Text = "";
            houseNumber.Text = "";
            image.Source = null;
            nameOfDepartment.Text = "";
            passNumber.Text = "";
            passCondition.Text = "";
        }

        private void Find_Worker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passNumberBox.Text == "" || departmentNameBox.Text == "")
                {
                    SetOfParameters();
                }
                else
                {
                    ClearParameters();

                    workerDAO.FindByPassNumberAndDepartmentName(passNumberBox.Text, departmentNameBox.Text);

                    foreach (Worker worker in workerDAO.worker)
                    {
                        surname.Text = worker.Surname;
                        name.Text = worker.Name;
                        patronymic.Text = worker.Patronymic;
                        dateOfBirth.Text = worker.DateOfBirth;
                        gender.Text = worker.Gender.ToString();
                        phoneNumber.Text = worker.PhoneNumber;
                        profession.Text = worker.Profession;
                        dateOfStartToWork.Text = worker.DateOfStartToWork;
                        numberOfShift.Text = worker.NumberOfShift.ToString();
                    }

                    foreach (Address address in workerDAO.address)
                    {
                        nameOfTheCity.Text = address.NameOfTheCity;
                        nameOfTheStreet.Text = address.NameOfTheStreet;
                        houseNumber.Text = address.HouseNumber;
                    }

                    foreach (Pass pass in workerDAO.pass_)
                    {
                        passNumber.Text = pass.Number;
                        passCondition.Text = pass.Condition.ToString();
                    }

                    nameOfDepartment.Text = workerDAO.depName[0].ToString();

                    foreach (Photo photo in workerDAO.photo)
                    {
                        image.Source = new BitmapImage(new Uri(photo.Path));
                    }

                    workerDAO.worker = null;
                    workerDAO.address = null;
                    workerDAO.pass_ = null;
                    workerDAO.photo = null;
                    workerDAO.depName[0] = null;
                }
            }
            catch 
            {
                SetOfParameters2();
            }
        }

        private void Get_Surname_DepartmentName_PassNumber()
        {
            dataGrid.ItemsSource = workerDAO.GetTable().DefaultView;
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

        public void SoundMessageAboutNothingWasFound(SoundPlayer player, bool soundState, string langugeState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.nothing_was_found);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.ничего_не_найдено);
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
        /// A set of instructions in case when nothing is found, checking the set parameters
        /// </summary>
        public void SetOfParameters2()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SoundMessageAboutNothingWasFound(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Nothing was found, check the set parameters!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Ничего не найдено, проверьте установленные параметры!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
