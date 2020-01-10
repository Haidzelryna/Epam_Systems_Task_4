using System;
using System.Configuration;

namespace Task_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Стартовые данные, заполняем БД
            //MainMethods.StartData();

            MainMethods.watcherCreated();
        
            //Для работы с двумя уже существующими файлами из папки Task4\Files
            //MainMethods.WorkWithSomeFiles();

            Console.ReadLine();
        }      
    }
}
