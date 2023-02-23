using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace HW7
{
    class Program
    {
        static string path = "data.txt";
        static Repository rep = new Repository(path);
        static string headers;

        /// <summary>
        /// Показывает меню пользователю
        /// </summary>
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("-------Меню------");
            Console.WriteLine("1 - Добавить работника");
            Console.WriteLine("2 - Вывести список работников");
            Console.WriteLine("3 - Найти работника по ID");
            Console.WriteLine("4 - Удалить работника");
            Console.WriteLine("5 - Вывести записи за указанный период");
            Console.WriteLine("6 - Редактировать запись");
            Console.WriteLine("Для выхода нажмите \"exit\"");
        }
        
        /// <summary>
        /// Считывает новые данные от пользователя
        /// </summary>
        /// <returns></returns>
        static string[] WriteNewData()
        {
            string[] data = new string[4];
            Console.WriteLine("Введите ФИО работника");
            data[0] = Console.ReadLine();

            Console.WriteLine("Введите Дату рождения");
            data[1] = Console.ReadLine();

            Console.WriteLine("Введите Рост работника");
            data[2] = Console.ReadLine();

            Console.WriteLine("Введите Место рождения работника");
            data[3] = Console.ReadLine();

            return data;
        }

        /// <summary>
        /// Добавляет нового работника
        /// </summary>
        static void AddWorker()
        {
            Console.Clear();
            Console.WriteLine("Добавление нового работника");
            string[] newWorkerData = WriteNewData();

            rep.AddNewWorker(new Worker(newWorkerData[0], Convert.ToInt32(newWorkerData[1]), DateTime.Parse(newWorkerData[2]), newWorkerData[3]));
            Console.WriteLine("Запись добавлена");
        }

        /// <summary>
        /// Выводит в консоль данные о работниках
        /// </summary>
        /// <param name="worker"></param>
        static void ShowWorker(Worker worker)
        {
            Console.Clear();
            Console.WriteLine(worker.Print());
        }

        /// <summary>
        /// Отображает список работников
        /// </summary>
        static void ShowWorkersList()
        {
            Console.Clear();
            Worker[] workers = rep.GetAllWorkers();
            Console.WriteLine("Список работников: ");

            Console.WriteLine(headers);
            foreach (Worker w in workers) ShowWorker(w);
        }

        /// <summary>
        /// Находит работника по ID
        /// </summary>
        static void FindWoker()
        {
            Console.Clear();
            Console.WriteLine("Введите ID работника");
            int id = Convert.ToInt32(Console.ReadLine());

            Worker worker = rep.GetWorkerById(id);

            Console.WriteLine(headers);
            ShowWorker(worker);
        }

        /// <summary>
        /// Удаляет работника по ID
        /// </summary>
        static void DeleteWorker()
        {
            Console.Clear();
            Console.WriteLine("Введите ID работника, которого хотите удалить");

            int id = Convert.ToInt32(Console.ReadLine());
            rep.DeleteWorker(id);

            Console.WriteLine($"Работник по ID \"{id}\" удален");
        }


        /// <summary>
        /// Показывает данные за выбранный период
        /// </summary>
        static void GetWorkersBetweenDates()
        {
            Console.Clear();
            Console.WriteLine("Введите дату начала периода");
            DateTime dateFrom = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату окончания периода");
            DateTime dateTo= DateTime.Parse(Console.ReadLine());

            Worker [] workers = rep.GetWorkersBetweenDates(dateFrom, dateTo);

            Console.WriteLine(headers);
            foreach(Worker w in workers)
                ShowWorker(w);
        }


        /// <summary>
        /// Метод редактирования данных
        /// </summary>
        static void EditData()
        {
            Console.Clear();
            Console.WriteLine("Редактирование записи\n");
            Console.WriteLine("Введите id строки, которую необходимо изменить");
            int id = Convert.ToInt32((string)Console.ReadLine());
            int index = 0;

            Worker[] workers = rep.GetAllWorkers();
            foreach(Worker w in workers)
            {
                if (w.id == id)
                {
                    index = Array.IndexOf(workers, w);
                    break;
                }
            }

            Worker worker = workers[index];
            ShowWorker(worker);

            string[] loadData = WriteNewData();
            worker.fullName = loadData[0];
            worker.dateOfBirth = DateTime.Parse(loadData[1]);
            worker.height = Convert.ToInt32(loadData[2]);
            worker.placeOfBirth = loadData[3];

            workers[index] = worker;
            rep.SaveData(workers);

        }

        /// <summary>
        /// Ожидание команды от пользователя
        /// </summary>
        static void Delay()
        {
            Console.WriteLine("Чтобы продолжить, нажмите любую клавишу");
            Console.ReadKey();
        }


         
        static void Main(string[] args)
        {
            headers = $"{"ID",5}{"Дата добавления",10}{"ФИО",15}{"Дата Рождения",15}{"Возраст",7}{"Рост",7}{"Место рождения",20}";

            bool exit = false;
            //Метод отображения меню
            ShowMenu();

            do
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        AddWorker();
                        Delay();
                        break;
                    case "2":
                        ShowWorkersList();
                        Delay(); break;
                    case "3":
                        FindWoker();
                        Delay(); break;
                    case "4":
                        DeleteWorker();
                        Delay(); break;
                    case "5":
                        GetWorkersBetweenDates();
                        Delay(); break;
                    case "6":
                        EditData();
                        Delay(); break;
                    case "exit": exit = true; break;

                    default:
                        Console.WriteLine("Данного пункта не существует. Выберите от 1 до 6.");
                        Delay(); break;
                }
                ShowMenu();
            } while (!exit);
        }
    }
}
