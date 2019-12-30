using System;
using Domain;
using Migrations.Migrations;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext DC = new DataContext();        

            Configuration.ParseResource(DC.Sales, Migrations.Resources.Resource._11);

            Console.ReadLine();
        }
    }
}
