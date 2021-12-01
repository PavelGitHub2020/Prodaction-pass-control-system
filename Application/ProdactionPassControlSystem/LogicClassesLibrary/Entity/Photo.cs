using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.Entity
{
    public class Photo
    {
        public int ID { get; }

        public string Path { get; set; }

        public int WorkerId { get; set; }

        public Photo() { }

        public Photo(int id, string path, int workerId)
        {
            ID = id;
            Path = path;
            WorkerId = workerId;
        }

        public override string ToString()
        {
            return $"Path - {Path}";
        }
    }
}
