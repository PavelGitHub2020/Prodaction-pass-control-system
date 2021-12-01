using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class Pass
    {
        public int ID { get; }

        public string Number { get; set; }

        public bool Condition { get; set; }
        public int WorkerId { get; set; }

        public Pass() { }

        public Pass(int id, string number, bool condition, int workerId)
        {
            ID = id;
            Number = number;
            Condition = condition;
            WorkerId = workerId;
        }

        public override string ToString()
        {
            return $"Number - {Number}\n" +
                   $"Condition - {Condition}";
        }
    }
}
