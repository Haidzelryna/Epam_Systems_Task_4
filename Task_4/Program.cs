using System;
using Domain;
using Migrations.Migrations;

namespace Task_4
{
    class Program
    {
        private const string ADMINID = "80AB7036-5D4A-11E6-9903-0050569977A1";

        static void Main(string[] args)
        {
            var adminGuid = Guid.Parse(ADMINID);

            using (DataContext dc = new DataContext())
            {
                Configuration.ParseResource(dc.Sales, Migrations.Resources.Resource._11,
                    sale =>
                {
                    sale.CreatedByUserId = adminGuid;
                    sale.CreatedDateTime = DateTime.UtcNow;
                });

                Configuration.RunEntityValidationException(() =>
                {
                    dc.SaveChanges();
                });

                Console.ReadLine();
            }

            //Configuration conf = new Configuration();

            //conf.seed
        }
    }
}
