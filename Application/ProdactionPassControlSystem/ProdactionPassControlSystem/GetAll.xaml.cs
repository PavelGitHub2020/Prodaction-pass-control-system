using LogicClassesLibrary.BLL;
using LogicClassesLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for GetAll.xaml
    /// </summary>
    public partial class GetAll : Window, IStringMessage, IAudioMessageAboutWhatManagedToFind, ISettingSoundParameters, 
                                          ISettingLanguageParameters, ISetOfParameters
    {
        private WorkerDAO workerDAO;

        private System.Media.SoundPlayer player;

        private bool soundState;
        private string langaugeState;

        int counter = 0;
        public GetAll()
        {
            workerDAO = new WorkerDAO();

            InitializeComponent();

            SetOfParameters();

            Get_All_Worker();
        }

        private void Get_All_Worker()
        {
            try
            {
                getAllWorkerGrid.Columns.Clear();

                int holeList_ = workerDAO.GetNumberOfWorkers();
                int serviceM = workerDAO.FindTheNumberOfEmployeesInDepartment("Service M");
                int serviceH = workerDAO.FindTheNumberOfEmployeesInDepartment("Service H");
                int trafficService = workerDAO.FindTheNumberOfEmployeesInDepartment("Traffic service");
                int electroMechanicalService = workerDAO.FindTheNumberOfEmployeesInDepartment("Electro-mechanical service");
                int securityService = workerDAO.FindTheNumberOfEmployeesInDepartment("Security service");
                int economicDepartment = workerDAO.FindTheNumberOfEmployeesInDepartment("Economic department");
                int computerDepartment = workerDAO.FindTheNumberOfEmployeesInDepartment("Computer department");

                getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorker().DefaultView;

                wholeList.Text = holeList_.ToString();
                numOfServiceM.Text = serviceM.ToString();
                numOfServiceH.Text = serviceH.ToString();
                numOfTrafficService.Text = trafficService.ToString();
                numOfElectroMechanicalService.Text = electroMechanicalService.ToString();
                numOfSecurityService.Text = securityService.ToString();
                numOfEconomicDepartment.Text = economicDepartment.ToString();
                numOfComputerDepartment.Text = computerDepartment.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Service_M_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(service_M.Text).DefaultView;
        }

        private void Service_H_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(service_H.Text).DefaultView;
        }

        private void Traffic_Service_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(traffic_Service.Text).DefaultView;
        }

        private void Eletro_Mechanical_Service_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(electro_Mechanical_Service.Text).DefaultView;
        }

        private void Security_Service_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(security_Service.Text).DefaultView;
        }

        private void Economic_Department_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(economic_Department.Text).DefaultView;
        }

        private void Computer_Department_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorkerByDepartment(computer_Department.Text).DefaultView;
        }

        private void The_Whole_List_Click(object sender, RoutedEventArgs e)
        {
            getAllWorkerGrid.ItemsSource = workerDAO.GetAllWorker().DefaultView;
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
    }
}
