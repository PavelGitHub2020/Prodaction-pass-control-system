using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChangingWorkerInformation.xaml
    /// </summary>
    public partial class ChangingWorkerInformation : Window, IStringMessage, IAudioMessageAboutWhatManagedToFind, ISettingSoundParameters,
                                                             ISettingLanguageParameters, ISetOfParameters, IAudioMessageAboutEnterWorkerId,
                                                             ISetOfParameters2, IAudioMessageAboutThereIsNoWorkerWithSuchIdentificator,
                                                             IAudioMessageAboutTheInputStringHadAnIncorretFormat, ISetOfParameters3,
                                                             ISetOfParameters4
    {
        private WorkerDAO workerDAO;
        private FormForChangingWorkerInformation forChangingWorkerInformation;

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;

        int counter = 0;
        public ChangingWorkerInformation()
        {
            InitializeComponent();

            SetOfParameters();

            workerDAO = new WorkerDAO();
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorker().DefaultView;
        }

        private void Select_Worker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxId.Text == "")
                {
                    SetOfParameters2();
                }
                else
                {
                    int count = workerDAO.AvailabilityOfASpecificWorkerId(Convert.ToInt32(textBoxId.Text));

                    if (count > 0)
                    {
                        forChangingWorkerInformation = new FormForChangingWorkerInformation(Convert.ToInt32(this.textBoxId.Text));
                        forChangingWorkerInformation.ShowDialog();
                    }
                    else
                    {
                        SetOfParameters3();
                    }
                }
                removeWorkerGrid.ItemsSource = workerDAO.GetAllWorker().DefaultView;
            }
            catch
            {
                SetOfParameters4();
            }
        }

        private void The_Whole_List_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorker().DefaultView;
        }

        private void Service_M_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(service_M.Text).DefaultView;
        }

        private void Service_H_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(service_H.Text).DefaultView;
        }

        private void Traffic_Service_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(traffic_Service.Text).DefaultView;
        }

        private void Eletro_Mechanical_Service_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(electro_Mechanical_Service.Text).DefaultView;
        }

        private void Security_Service_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(security_Service.Text).DefaultView;
        }

        private void Economic_Department_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(economic_Department.Text).DefaultView;
        }

        private void Computer_Department_Click(object sender, RoutedEventArgs e)
        {
            removeWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(computer_Department.Text).DefaultView;
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
                        SetOfParameters();
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

        public void SoundMessageAboutWhatManagedToFind(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.here_Is_what_managed_to_find);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.вот_что_удалось_найти);
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
        /// A set of instructions in case to successful search
        /// </summary>
        public void SetOfParameters()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();
                SoundMessageAboutWhatManagedToFind(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("Here is what managed to find!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Вот, что удалось найти!");
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

        /// <summary>
        /// A set of instructions in case  the worker ID is not entered
        /// </summary>
        public void SetOfParameters2()
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

        public void SondMessageAboutTheInputStringHadAnIncorretFormat(SoundPlayer player, bool soundState, string languageState)
        {
            if (soundState == true)
            {
                try
                {
                    if (langaugeState == "eng")
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.the_input_string_had_incorrect_format);
                        player.Play();
                        player.Dispose();
                    }
                    else
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.входная_строка_имела_неверный_формат);
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
        /// A set of instructions in case the input string had an incorrect format
        /// </summary>
        public void SetOfParameters4()
        {
            try
            {
                SettingSoundParameters();
                SettingLanguageParameters();

                SondMessageAboutTheInputStringHadAnIncorretFormat(player, soundState, langaugeState);

                if (langaugeState == "eng" && soundState == false)
                {
                    StringMessageInEnglish("The input string had an incorrect format!");
                }
                else if (langaugeState == "ru" && soundState == false)
                {
                    StringMessageInRussian("Входная строка имела неверный формат!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
