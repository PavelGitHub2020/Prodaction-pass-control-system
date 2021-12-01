using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicClassesLibrary.Entity;

namespace LogicClassesLibrary.DAL
{
    public interface IWorkerDAO
    {
        void Add(Worker worker);
        void Update(Worker worker);
        int AvailabilityOfASpecificWorkerId(int workerId);
        void Remove(int id);
        void FindByPassNumberAndDepartmentName(string passNumber, string departmentName);
        int GetMaxId();
        int GetNumberOfWorkers();
        DataTable GetTable();
        DataTable GetTable1();
        DataTable GetAllWorker();
        DataTable GetAllWorkerByDepartment(string departmentName);
        DataTable GetWorkerById(int id);
        int FindTheNumberOfEmployeesInDepartment(string nameOfDepartment);
        void AddFieldsForChanging(FieldsForChangingTheWorkShedule fieldsForChangingTheWorkShedule, int id, List<FieldsForChangingTheWorkShedule> fields, int year, string month);
        int ChekingForChangedDataInTheDatabase(int year, string month, int workId);
        DataTable GetChangedInformation(int year, string month, int workId);
        void UpdateChangedShedule(FieldsForChangingTheWorkShedule fieldsForChangingTheWorkShedule, int id, List<FieldsForChangingTheWorkShedule> fields, int year, string month);
        DataTable GetWorkerIdAndNumberOfShift();
        DataTable GetWorkerIdFromChangedInformation(int year, string month);
        DataTable GetValuesOfTime(int numberOfDay, int year, string month, int workerId);
        void Add_Data_Of_The_Use_Of_A_Pass_By_A_Worker(ControlOfTheUseOfThePass controlOfTheUseOfThePass, List<ControlOfTheUseOfThePass> controls, string timeOfUseOfThePass);
        DataTable GetAllInformationAboutUseThePass();
        DataTable GetAllInformationAboutUseThePassByWorkerId(int workerId);
        DataTable GetAllInformationAboutUseThePassByWorkerIdYear(int workerId, int year);
        DataTable GetAllInformationAboutUseThePassByWorkerIdYearMonth(int workerId, int year, string month);
        DataTable GetAllInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay);
        int DeleteAllFromInformationAboutUseThePass();
        int DeleteAllInformationAboutUseThePassByWorkerId(int workerId);
        int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYear(int workerId, int year);
        int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonth(int workerId, int year, string month);
        int DeletemoreSpecificInformationAboutUseThePassByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay);
        int TotalNumberOfPassesUsed();
        int TotalNumberOfPassesUsedByWorkerId(int workerId);
        int TotalNumberOfPassesUsedByWorkerIdYear(int workerId, int year);
        int TotalNumberOfPassesUsedByWorkerIdYearMonth(int workerId, int year, string month);
        int TotalNumberOfPassesUsedByWorkerIdYearMonthNumberOfDay(int workerId, int year, string month, int numberOfDay);
    }
}
