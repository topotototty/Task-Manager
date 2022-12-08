using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    public static class Tasks
    {

        private static List<Processes_aditionaly> GetTasks()
        {
            List<Processes_aditionaly> result = new List<Processes_aditionaly>();
            Process[] processes = Process.GetProcesses();

            for (int i = 0; i < processes.Length; i++)
            {
                result.Add(new Processes_aditionaly(processes[i].ProcessName, processes[i].Id, processes[i].PagedMemorySize64));
            }

            return result;
        }

        static int selection = 0;

        public static void Task()
        {
            Console.WriteLine("  _______________ДИСПЕТЧЕР ЗАДАЧ________________");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   Имя процесса                       ID процесса          Память");
            Console.ResetColor();

            Process[] processes = Process.GetProcesses();
            

            List<Processes_aditionaly> tasks = GetTasks();
            int i = 0;

            foreach (Processes_aditionaly Additional in tasks)
            {
                i++;
                Console.SetCursorPosition(0, 1 + i);
                Console.WriteLine($"   {Additional.Name} ");
                Console.SetCursorPosition(35, 1 + i);
                Console.WriteLine($"   {Additional.Id}");
                Console.SetCursorPosition(56, 1 + i);
                Console.WriteLine($"   {Additional.TaskMemory}");
            }

            while (true)
            {
                Arrows.DisplayArrow(selection, 2);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:

                        if (selection + 1 < tasks.Count)
                        {
                            selection++;
                            if (selection == tasks.Count)
                            {
                                selection = 0;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (selection > 0)
                        {
                            selection--;
                        }
                        break;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();

                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($" {processes[selection].ProcessName}");
                            Console.ResetColor();
                            Console.WriteLine("------------------------------------------------------------");

                            Console.WriteLine($" Класс приоритета                         : {processes[selection].PriorityClass}");
                            Console.WriteLine($" Время пользовательского процессора       : {processes[selection].UserProcessorTime}");
                            Console.WriteLine($" Общее время процессора                   : {processes[selection].TotalProcessorTime}");
                            Console.WriteLine($" Объем выгружаемой системной памяти       : {processes[selection].PagedSystemMemorySize64 / 8 / 1024 / 1024} МБ");
                            Console.WriteLine($" Размер выгружаемой памяти                : {processes[selection].PagedMemorySize64 / 8 / 1024 / 1024} МБ");

                            Console.WriteLine("------------------------------------------------------------");


                            if (processes[selection].Responding)
                            {
                                Console.WriteLine(" Статус -> Запущен");
                            }
                            else
                            {
                                Console.WriteLine(" Статус -> Не запущен");
                            }

                            Console.WriteLine("------------------------------------------------------------");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" D -> Завершить процесс");
                            Console.WriteLine(" Delete -> Завершить все процессы с таким именем");
                            Console.ResetColor();

                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                            if (keyInfo.Key == ConsoleKey.D)
                            {
                                processes[selection].Kill();

                                Console.WriteLine("Процесс завершен!");
                                Thread.Sleep(2000);

                                Console.Clear();
                                Task();
                            }

                            else if (keyInfo.Key == ConsoleKey.Delete)
                            {
                                foreach (Process process in processes)
                                {
                                    if (process.ProcessName == tasks[selection].Name)
                                    {
                                        process.Kill(true);
                                    }
                                }

                                Console.WriteLine("Процессы завершены!");
                                Thread.Sleep(2000);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Нет доступа(");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                        finally
                        {
                            Console.Clear();
                            Task();
                        }
                        break;
                }
            }
        }
    }
}
