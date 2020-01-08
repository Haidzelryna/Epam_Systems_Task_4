using System;
using System.Configuration;

namespace Task_4
{
    class Program
    {
        private const string ADMINID = "80AB7036-5D4A-11E6-9903-0050569977A1";
        private static Guid adminGuid;

        static void Main(string[] args)
        {
           
            MainMethods.watcherCreated();

            //Стартовые данные, заполняем БД
            //MainMethods.StartData();

            //Для работы с двумя уже существующими файлами из папки Task4\Files
            //MainMethods.WorkWithSomeFiles();

            Console.ReadLine();
        }      
    }
}
