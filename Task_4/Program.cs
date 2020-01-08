using System;
using System.Configuration;

namespace Task_4
{
    class Program
    {
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
