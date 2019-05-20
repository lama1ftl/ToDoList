using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class ToDo:IComparable<ToDo>
    {
        public int toDoID { get; set; }
        public DateTime date { get; set; }
        public string task { get; set; }
        public int lvlImp { get; set; }

        public ToDo(int TODOID, DateTime DATE, string TASK, int LVLIMP)
        {
            this.toDoID = TODOID;
            this.date = DATE;
            this.task = TASK;
            this.lvlImp = LVLIMP;
        }
        public int CompareTo(ToDo other)
        {
            return this.toDoID.CompareTo(other.toDoID);
        }

    }
}
