using System;
using System.Windows;
using LogicClassesLibrary.DAL;
using LogicClassesLibrary.Entity;
using System.Windows.Media.Imaging;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Controls;

namespace ProdactionPassControlSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ScheduleOfShift scheduleOfShifts = new ScheduleOfShift();

            //ScheduleOfShiftsDAO scheduleOfShiftsDAO = new ScheduleOfShiftsDAO();
            //scheduleOfShiftsDAO.AddingDataAboutShifts(scheduleOfShifts, 914);///914,1218


            //MessageBox.Show("Done");

        }

        private void Add_Worker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
        }

        private void Find_Worker_Click(object sender, RoutedEventArgs e)
        {
            FindWorker findWorker = new FindWorker();
            findWorker.ShowDialog();
        }

        private void Get_All_Worker_Click(object sender, RoutedEventArgs e)
        {
            GetAll getAll = new GetAll();
            getAll.ShowDialog();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Remove remove = new Remove();
            remove.ShowDialog();
        }

        private void Changing_Worker_Information_Click(object sender, RoutedEventArgs e)
        {
            ChangingWorkerInformation changingWorkerInformation = new ChangingWorkerInformation();
            changingWorkerInformation.ShowDialog();
        }

        private void Change_The_Work_Shedule_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheWorkShedule changeTheWorkShedule = new ChangeTheWorkShedule();
            changeTheWorkShedule.ShowDialog();
        }

        private void Passage_Control_Click(object sender, RoutedEventArgs e)
        {
            PassageControl passageControl = new PassageControl();
            passageControl.ShowDialog();
        }

        private void Information_About_Use_The_Pass_Click(object sender, RoutedEventArgs e)
        {
            InformationAboutUseThePass informationAboutUseThePass = new InformationAboutUseThePass();
            informationAboutUseThePass.ShowDialog();
        }

        private void Information_About_Shifts_Click(object sender, RoutedEventArgs e)
        {
            InformationAboutShifts informationAboutShifts = new InformationAboutShifts();
            informationAboutShifts.ShowDialog();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
    
    

