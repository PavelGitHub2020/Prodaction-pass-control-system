using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class Worker
    {
        public int ID { get; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Profession { get; set; }
        public string DateOfStartToWork { get; set; }
        public int DepartmentId { get; set; }

        public int NumberOfShift { get; set; }

        public Worker() { }

        public Worker(int id, string surname, string name, string patronymic, string dateOfBirth,bool gender,string phoneNumber,
                      int departmentId, string profession, string dateOfStartToWork, int numberOfShift)
        {
            ID = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
            DepartmentId = departmentId;
            Profession = profession;
            DateOfStartToWork = dateOfStartToWork;
            NumberOfShift = numberOfShift;
        }

        public override string ToString()
        {
            return $"Last name - {Surname}\n" +
                   $"Name - {Name}\n" +
                   $"Patronymic - {Patronymic}\n" +
                   $"Date of birth - {DateOfBirth}\n" +
                   $"Gender - {Gender}\n" +
                   $"Phone number - {PhoneNumber}\n" +
                   $"Name of department - {DepartmentId}\n" +
                   $"Profession - {Profession}\n" +
                   $"Date of start to work - {DateOfStartToWork}\n" +
                   $"Number of shift - {NumberOfShift}";
        }

    }
}
