using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    class Processes_aditionaly
    { 
        public string Name;
        public int Id;
        public long TaskMemory;

        public Processes_aditionaly(string name, int id, long taskmemory)
        {
            Name = name;
            Id = id;
            TaskMemory = taskmemory;
        }
    }
}
