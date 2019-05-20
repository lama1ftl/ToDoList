using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;

namespace ToDoList
{
    class Program
    {          
        public static void header()
        {
            Console.Clear();
            DateTime dt = DateTime.Now;

            Console.WriteLine("\n\n\n\t\t\t\t\t " + dt.ToString("dd-MM-yyyy"));
            Console.WriteLine("\t\t\t\t================================");
            Console.WriteLine("\t\t\t\t\t What's new?");
            Console.WriteLine("\t\t\t\t================================");
        }

        public static void body()
        {
            Console.WriteLine("\t\t\t\t1.New task.\t  5.Update task.\n");
            Console.WriteLine("\t\t\t\t2.View Al.\t  6.Delete task.\n");
            Console.WriteLine("\t\t\t\t3.View b/w Dates. 7.Sort.\n");
            Console.WriteLine("\t\t\t\t4.Find task.\t  8.Exit\n");            
        }

        public static void footer()
        {
            Console.WriteLine("\t\t\t\t================================");
        }



        public static void UI_msg(String msg)
        {
            header();
            Console.WriteLine("\n\n\t\t\t\t" + msg + "\n\n");
            footer();
            Console.Write("\t\t\t\tPress <any> key to continue:");
            Console.ReadKey();

        }


        public static bool check_date(String daaat)
        {
            string[] formats = { "d-M-yyyy" };
            DateTime parsedDateTime;
            if (DateTime.TryParseExact(daaat, formats, new CultureInfo("ru-RU"),
                    DateTimeStyles.None, out parsedDateTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Main()
        {
            Random rnd = new Random();
            int ID = rnd.Next(100);

            List<ToDo> TD_Task = new List<ToDo>();
            bool check = true; ;

        MAIN:

            while (true)
            {


                header();
                body();
                footer();

                Console.Write("\t\t\t\tEnter your choice: ");

                int ch = 0;

                try
                {
                    ch = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                    UI_msg("ERROR: Insert Only Intergers!");
                }


                switch (ch)
                {
                    case 1:
                        header();
                        Console.Write("\t\t\t\tEnter the Date.\t[dd-MM-yyyy]\n\t\t\t\t");
                        try
                        {
                            string dat = Console.ReadLine();
                            string daat = dat;

                            DateTime cur_time = DateTime.Now;
                            cur_time.ToString("d-M-yyyy");
                            try
                            {
                                TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(dat.ToString()));


                                int day = (int)Math.Round(duration.TotalDays);

                                int x = 0;
                                if (day % 2 != 0)
                                {
                                    x = 2;
                                }
                                else
                                {
                                    x = 1;
                                }


                                if (day >= x)    
                                {
                                    DateTime dtu = DateTime.Now;
                                    string msg = "Plz select date from\n\t\t\t\t" + dtu.ToString("d-M-yyyy") + " onwards!";
                                    UI_msg("ERROR: " + msg);
                                    goto MAIN;
                                }

                            }
                            catch (FormatException)
                            {
                                UI_msg("ERROR: Invalid Date!");
                                goto MAIN;

                            }



                            if (check_date(daat)) 
                            {
                                Console.Write("\n\t\t\t\tEnter task.\n\t\t\t\t");
                                string msg = Console.ReadLine();

                                Console.Write("\n\t\t\t\tEnter Level of Importance.\t[1-5]\n\t\t\t\t");
                                int lvl = int.Parse(Console.ReadLine());
                                if (lvl >= 1 && lvl <= 5)
                                {
                                    ID++;

                                    TD_Task.Add(new ToDo(ID, DateTime.Parse(dat), msg, lvl));
                                    UI_msg("New task created with task ID = " + ID.ToString());
                                    TD_Task.Sort(); 
                                }
                                else
                                {
                                    UI_msg("ERROR: Only between [1-5]!");
                                }
                            }
                            else
                            {
                                UI_msg("ERROR: Invalid Date!");
                            }


                        }
                        catch (Exception)
                        {
                            UI_msg("ERROR: Enter Integer Only!!");
                        }
                        break;

                    case 2:
                        header();
                        Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");

                        foreach (ToDo x in TD_Task)
                        {
                            check = false;
                            Console.WriteLine("\t\t\t\t" + x.toDoID + " " + x.date.ToString("dd-MM-yyyy") + "\t" + x.task + "\t   " + x.lvlImp);
                        }
                        if (check)
                        {
                            Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                        }
                        footer();
                        Console.Write("\t\t\t\tPress <any> key to continue:");
                        Console.ReadKey();

                        break;

                    case 3:

                        header();

                        string cmp_date1, mon1, day1, S_d, E_d, S_m, E_m, S_da, E_da;
                        int SD, ED, cmp_date, mon, SM, EM, dayx, SDA, EDA;

                        Console.Write("\t\t\t\tEnter starting Date.[dd-MM-yyyy]\n\t\t\t\t");
                        string Sdat3 = Console.ReadLine();

                        if (check_date(Sdat3))    
                        {
                            Console.Write("\n\t\t\t\tEnter ending Date.[dd-MM-yyyy]\n\t\t\t\t");
                            string Edat3 = Console.ReadLine();
                            Console.WriteLine("\t\t\t\t--------------------------------");
                            Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");

                            if (check_date(Edat3)) 
                            {

                                DateTime s = DateTime.Parse(Sdat3);
                                DateTime e = DateTime.Parse(Edat3);


                                for (int i = 0; i < TD_Task.Count; i++)
                                {
                                     
                                    cmp_date1 = TD_Task[i].date.ToString("yyyy");
                                    cmp_date = int.Parse(cmp_date1);
                                    S_d = s.ToString("yyyy");
                                    E_d = e.ToString("yyyy");
                                    SD = int.Parse(S_d);
                                    ED = int.Parse(E_d);

                                      
                                    mon1 = TD_Task[i].date.ToString("MM");
                                    mon = int.Parse(mon1);
                                    S_m = s.ToString("MM");
                                    E_m = e.ToString("MM");
                                    SM = int.Parse(S_m);
                                    EM = int.Parse(E_m);

                                      
                                    day1 = TD_Task[i].date.ToString("dd");
                                    dayx = int.Parse(day1);
                                    S_da = s.ToString("dd");
                                    E_da = e.ToString("dd");
                                    SDA = int.Parse(S_da);
                                    EDA = int.Parse(E_da);

                                    if (cmp_date >= SD && cmp_date <= ED)  
                                    {
                                        if (mon >= SM && mon <= EM)       
                                        {
                                            if (dayx >= SDA && dayx <= EDA)  
                                            {
                                                check = false;
                                                Console.WriteLine("\t\t\t\t" + TD_Task[i].toDoID + " " + TD_Task[i].date.ToString("dd-MM-yyyy") + "\t" + TD_Task[i].task + "\t   " + TD_Task[i].lvlImp);
                                            }
                                        }
                                    }
                                }

                                if (check)
                                {
                                    Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                                }
                                footer();
                                Console.Write("\t\t\t\tPress <any> key to continue:");
                                Console.ReadKey();
                            }
                            else
                            {
                                UI_msg("ERROR: Invalid Ending Date!");
                            }
                        }
                        else
                        {
                            UI_msg("ERROR: Invalid Starting Date!");
                        }

                        break;

                    case 4:
                        header();
                        Console.Write("\t\t\t\tEnter the String.\n\t\t\t\t");
                        try
                        {
                            string str1;
                            string str = Console.ReadLine();
                            str.ToLower();
                            Console.WriteLine("\t\t\t\t--------------------------------");
                            Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                str1 = TD_Task[i].task;
                                str1.ToLower();
                                if (str1.Contains(str))
                                {
                                    check = false;
                                    Console.WriteLine("\t\t\t\t" + TD_Task[i].toDoID + " " + TD_Task[i].date.ToString("dd-MM-yyyy") + "\t" + TD_Task[i].task + "\t   " + TD_Task[i].lvlImp);
                                }


                            }

                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                            }
                            footer();
                            Console.Write("\t\t\t\tPress <any> key to continue:");
                            Console.ReadKey();
                        }
                        catch (Exception)
                        {

                            UI_msg("Error in Find string");
                        }


                        break;

                    case 5:
                        header();
                        Console.Write("\t\t\t\tEnter the Task_ID.\n\t\t\t\t");
                        try
                        {
                            int T_ID = int.Parse(Console.ReadLine());
                            Console.WriteLine("\t\t\t\t--------------------------------");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                if (TD_Task[i].toDoID == T_ID)
                                {
                                    check = false;
                                    Console.Write("\t\t\t\tEnter the Date.[dd-MM-yyyy]\n\t\t\t\t");
                                    try
                                    {
                                        string dat = Console.ReadLine();
                                        string daat = dat;

                                        DateTime cur_time = DateTime.Now;
                                        cur_time.ToString("d-M-yyyy");
                                        try
                                        {
                                            TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(dat.ToString()));


                                            int day = (int)Math.Round(duration.TotalDays);

                                            if (day >= 2)    
                                            {
                                                DateTime dtu = DateTime.Now;
                                                string msg = "Plz select date from\n\t\t" + dtu.ToString("d-M-yyyy") + " onwards!";
                                                UI_msg("ERROR: " + msg);
                                                goto MAIN;
                                            }

                                        }
                                        catch (FormatException)
                                        {
                                            UI_msg("ERROR: Invalid Date!");
                                            goto MAIN;

                                        }



                                        if (check_date(daat)) 
                                        {
                                            Console.Write("\n\t\t\t\tEnter task.\n\t\t\t\t");
                                            string msg = Console.ReadLine();

                                            Console.Write("\n\t\t\t\tEnter Level of Importance.\t[1-5]\n\t\t\t\t");
                                            int lvl = int.Parse(Console.ReadLine());
                                            if (lvl >= 1 && lvl <= 5)
                                            {


                                                TD_Task[i].date = DateTime.Parse(dat);
                                                TD_Task[i].task = msg;
                                                TD_Task[i].lvlImp = lvl;

                                                Console.WriteLine("\t\t\t\tTask Updated!");
                                                TD_Task.Sort();  
                                            }
                                            else
                                            {
                                                UI_msg("ERROR: Only between [1-5]!");
                                            }
                                        }
                                        else
                                        {
                                            UI_msg("ERROR: Invalid Date!");
                                        }


                                    }
                                    catch (Exception)
                                    {
                                        UI_msg("ERROR: Enter Integer Only!!");
                                    }

                                }
                            }
                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\t\t No Record Found!\n\n");
                            }
                            footer();
                            Console.Write("\t\t\t\tPress <any> key to continue:");
                            Console.ReadKey();

                        }
                        catch (Exception)
                        {

                            UI_msg("ERROR: Insert Only Intergers!");
                        }
                        break;

                    case 6:
                        header();
                        Console.Write("\t\t\t\tEnter the Task_ID.\n\t\t\t\t");
                        try
                        {
                            int T_ID = int.Parse(Console.ReadLine());
                            Console.WriteLine("\t\t\t\t--------------------------------");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                if (TD_Task[i].toDoID == T_ID)
                                {
                                    check = false;
                                    TD_Task.RemoveAll(e => e.toDoID == T_ID);
                                }
                            }
                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\t\tNo Record Found!\n\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\n\t\t\t\tRecord Deleted!\n\n");

                            }
                            footer();
                            Console.Write("\t\t\t\tPress <any> key to continue:");
                            Console.ReadKey();

                        }
                        catch (Exception)
                        {

                            UI_msg("ERROR: Insert Only Intergers!");
                        }
                        break;


                    case 7:

                        while (true)
                        {
                            header();
                            Console.WriteLine("\t\t\t\t1.Sort By ID.");
                            Console.WriteLine("\t\t\t\t2.Sort By DATE.");
                            Console.WriteLine("\t\t\t\t3.Sort By Level Of Importance.");
                            Console.WriteLine("\t\t\t\t4.Exit.");
                            footer();
                            Console.Write("\t\t\t\tEnter your choice: ");
                            ch = int.Parse(Console.ReadLine());

                            switch (ch)
                            {

                                case 1:

                                    header();
                                    Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");
                                    Console.WriteLine("\t\t\t\t--------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.toDoID).ToList();
                                    foreach (ToDo x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t\t\t" + x.toDoID + " " + x.date.ToString("dd-MM-yyyy") + "\t" + x.task + "\t   " + x.lvlImp);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.date).ToList();

                                    Console.Write("\t\t\t\tPress <any> key to continue:");
                                    Console.ReadKey();

                                    break;

                                case 2:

                                    header();
                                    Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");
                                    Console.WriteLine("\t\t\t\t--------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.date).ToList();
                                    foreach (ToDo x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t\t\t" + x.toDoID + " " + x.date.ToString("dd-MM-yyyy") + "\t" + x.task + "\t   " + x.lvlImp);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.date).ToList();

                                    Console.Write("\t\t\t\tPress <any> key to continue:");
                                    Console.ReadKey();

                                    break;

                                case 3:

                                    header();
                                    Console.WriteLine("\t\t\t\tID Date\t\tTask\t   Level");
                                    Console.WriteLine("\t\t\t\t--------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.lvlImp).ToList();
                                    TD_Task.Reverse();
                                    foreach (ToDo x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t\t\t" + x.toDoID + " " + x.date.ToString("dd-MM-yyyy") + "\t" + x.task + "\t   " + x.lvlImp);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.date).ToList();

                                    Console.Write("\t\t\t\tPress <any> key to continue:");
                                    Console.ReadKey();

                                    break;
                                case 4:
                                    goto MAIN;


                                default:
                                    UI_msg("Invalid choice!");
                                    break;
                            }

                        }

                    case 8:
                        Environment.Exit(0);
                        break;

                    default:
                        UI_msg("Invalid choice!");
                        break;

                }
            }
        }
    }
}