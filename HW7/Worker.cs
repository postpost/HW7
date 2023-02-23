using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7
{
    struct Worker
    {

        #region Свойства
        /// <summary>
        /// Номер ID
        /// </summary>
        public int id { get; private set; }

        /// <summary>
        /// Дата и время добавления записи
        /// </summary>
        public DateTime dateOfEntry { get; private set; }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string fullName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime dateOfBirth { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string placeOfBirth { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод вывода данных
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{this.id}#{this.dateOfEntry:dd.MM.yyyy HH.mm}#{this.fullName}#{this.age}#{this.height}#{this.dateOfBirth: dd.MM.yyyy}#{this.placeOfBirth}";
        }
        #endregion

        #region Конструктор для выгрузки данных

        /// <summary>
        /// Конструктор для выгрузки данных из файла
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="DateOfEntry"></param>
        /// <param name="FullName"></param>
        /// <param name="Age"></param>
        /// <param name="Height"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="PlaceOfBirth"></param>
        public Worker(string ID, string DateOfEntry, string FullName, string Age, string Height, string DateOfBirth, string PlaceOfBirth)
        {
            this.id = Convert.ToInt32(ID);
            this.dateOfEntry = DateTime.Parse("DateOfEntry");
            this.fullName = FullName;
            this.age = Convert.ToInt32(Age);
            this.height = Convert.ToInt32(Height);
            this.dateOfBirth = DateTime.Parse("DateOfBirth");
            this.placeOfBirth = PlaceOfBirth;
        }
        #endregion

        #region Конструктор для создания нового работника

        /// <summary>
        /// Конструктор для создания нового работника
        /// </summary>
        /// <param name="FullName"></param>
        /// <param name="Height"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="PlaceOfBirth"></param>
        public Worker(string FullName, int Height, DateTime DateOfBirth, string PlaceOfBirth):
        this("0", DateTime.Now.ToString(), FullName, "0", Height.ToString(), DateOfBirth.ToShortDateString(), PlaceOfBirth)
        {
            int Age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.AddYears(Age) > DateTime.Today) Age--;
            this.age = Age;
        }
       
        #endregion


    }


}
