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
            Configuration conf = new Configuration();

            var adminGuid = Guid.Parse(ADMINID);

            using (SalesEntities dc = new SalesEntities())
            {
                //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
                var client = new Client();
                client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
                var contact = new Contact();
                contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d99");
                contact.FirstName = "Гайдель";
                contact.LastName = "Ирина";
                dc.Contact.Add(contact);
                client.ContactId = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d99");
                dc.Client.Add(client);

                //добавим продукт
                var product = new Product();
                product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
                dc.Product.Add(product);

                conf.ParseResource(dc.Sale, Migrations.Resources.Resource._11,
                    sale =>
                {
                    sale.CreatedByUserId = adminGuid;
                    sale.CreatedDateTime = DateTime.UtcNow;
                });

               conf.RunEntityValidationException(() =>
                {
                    dc.SaveChanges();
                });

                Console.ReadLine();
            }
        }
    }
}
