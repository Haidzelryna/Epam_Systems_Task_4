using System;
using Domain;
using Migrations.Migrations;
using Domain.Mapper;
using Models;
using System.Linq;

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

                //добавим контакт "6acb9fb3-9213-49cd-abda-f9785a658d99"
                var contact = new Domain.Contact();
                contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d12");
                contact.FirstName = "Гайдель";
                contact.LastName = "Ирина";
                dc.Contact.Add(contact);

                //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
                var client = new Domain.Client();
                client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d23");
                client.ContactId = contact.Id;
                dc.Client.Add(client);

                //добавим продукт
                var product = new Domain.Product();
                product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-f9785a658d23");
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

                var DomainSale = dc.Sale.First();
                var ModelsSale = new Models.Sale();

                Mapping.Map(DomainSale, ModelsSale);

                Console.ReadLine();
            }
        }
    }
}
