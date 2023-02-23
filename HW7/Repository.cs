using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace HW7
{
    class Repository
    {
        private string path;
       
        /// <summary>
        /// Конструктор
        /// </summary>
        public Repository(string Path)
        {
            this.path = Path;
            if(!File.Exists(Path)) File.Create(Path).Close();
        }

        /// <summary>
        /// Получаем массив работников из файла
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            // Вызгрузка всех записей из файла в массив
            string[] entries = File.ReadAllLines(path);

            // Создание пустого массива работников по размеру записей из файла
            Worker[]  workers = new Worker[entries.Length];

            //Заполняем новый массив работников из файла по конструктору
            for (int i = 0; i < entries.Length; i++)
            {
                string[] line = entries[i].Split('#'); //выгрузка из массива по 1-ой строчке без символа-разделителя
                                                       // в новый массив
                // Запись в массив работников разделенных данных по строкам
                workers[i] = new Worker(
                    line[0],
                    line[1],
                    line[2],
                    line[3],
                    line[4],
                    line[5],
                    line[6]
                    );
            }
            return workers;
        }

         
        /// <summary>
        /// Ищет работника по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Worker GetWorkerById(int id)
        {
            //Выгружаем в массив данные работников
            Worker[] workers = GetAllWorkers();
            foreach(Worker e in workers)       // Перебираем все элементы массива
            
                if (e.id == id) return e;      // Находим элемент по ID
            return new Worker();
            
        }

        /// <summary>
        /// Метод добавления нового работника
        /// </summary>
        /// <param name="NewWorker"></param>
        public void AddNewWorker(Worker newWorker)
        {
            int id;
            Worker[] workers = GetAllWorkers();
            if (workers.Length > 0)
            {
                id = workers[workers.Length - 1].id; //вычисляем номер последней записи
            }
            else id = 0;               

            using (StreamWriter sw = new StreamWriter(new FileStream(this.path, FileMode.Append, FileAccess.Write)))
            {
                sw.WriteLine($"{++id}#{DateTime.Now:dd.MM.yyyy HH:mm}#{newWorker.fullName}#{newWorker.age}#{newWorker.height}#{newWorker.dateOfBirth:dd.MM.yyyy}#{newWorker.placeOfBirth}");

            }

        }

        
        /// <summary>
        /// Удаляет работника по ID
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(int id)
        {
            Worker[] workers = GetAllWorkers();

            //Записываем файл снова за исключением удаленного ID
            using (StreamWriter sw = new StreamWriter(new FileStream(this.path, FileMode.Truncate, FileAccess.Write)))
            {
                foreach(Worker w in workers)
                {
                    if (w.id != id)
                        sw.WriteLine(w.Print());
                }
            }
        }

        /// <summary>
        /// Возвращает данные, добавленные в указанный период
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenDates(DateTime startDate, DateTime endDate)
        {
            Worker[] workers = GetAllWorkers();

            //вычисляем количество записей
            int count = 0;
            foreach (Worker e in workers)
                if (e.dateOfEntry.Date>=startDate.Date && e.dateOfEntry.Date <= endDate.Date) count++;

            //заполняем новый массив
            Worker[] result = new Worker[count];
            int ind = 0;
            for (int i =0; i < workers.Length; i++)
            {
                if (workers[i].dateOfEntry.Date>=startDate.Date && workers[i].dateOfEntry.Date<=endDate.Date)
                {
                    result[ind] = workers[i];
                    ind++;
                }
            }

            return result;
        }
        

        /// <summary>
        /// Сохраняет данные в файл
        /// </summary>
        /// <param name="workers"></param>
        public void SaveData(Worker[] workers)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(this.path, FileMode.Truncate, FileAccess.Write)))
            {
                foreach (Worker e in workers)
                {
                    sw.WriteLine(e.Print());
                }
            }
        }
    }
}
