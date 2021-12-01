using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class Address
    {
        public int ID { get; }
        public string NameOfTheCity { get; set; }
        public string NameOfTheStreet { get; set; }
        public string HouseNumber { get; set; }

        public int WorkerId { get; set; }

        public Address() { }

        public Address(int id, string nameOfTheCity, string nameOfTheStreet, string houseNumber, int workerId)
        {
            ID = id;
            NameOfTheCity = nameOfTheCity;
            NameOfTheStreet = nameOfTheStreet;
            HouseNumber = houseNumber;
            WorkerId = workerId;
        }

        public override string ToString()
        {
            return $"Name of the city - {NameOfTheCity}\n" +
                   $"Name of the street - {NameOfTheStreet}\n" +
                   $"House number - {HouseNumber}";
        }
    }
}
