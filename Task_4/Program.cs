using System;
using Domain;
using Migrations.Migrations;
using Domain.Mapper;
using Models;
using System.Linq;
using Domain.Repository;
using System.Data.Entity;
using Domain;
using Domain.Exception;
using System.Text.RegularExpressions;

namespace Task_4
{
    class Program
    {
        private const string ADMINID = "80AB7036-5D4A-11E6-9903-0050569977A1";

        //@"w_[0-9]{2}[0-12]{1}[0-9]{4}"
        public const string VALIDATEREGEX = @"\w_\d{8}$";

        static void Main(string[] args)
        {       
            Configuration conf = new Configuration();

            var adminGuid = Guid.Parse(ADMINID);

            using (SalesEntities dc = new SalesEntities())
            {
                string line = Regex.Replace("Ivanov_19112012".Trim(), @"\s+", @" ");
                if (line != "")
                {
                    if (Regex.IsMatch(line, VALIDATEREGEX))
                    {
                        /*
                       //добавим контакт "6acb9fb3-9213-49cd-abda-f9785a658d12"
                       var contact = new Domain.Contact();
                       contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d12");
                       contact.FirstName = "Гайдель";
                       contact.LastName = "Ирина";
                       dc.Contact.Add(contact);

                       //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
                       var manager = new Domain.Manager();
                       manager.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d88");
                       manager.ContactId = contact.Id;
                       dc.Manager.Add(manager);

                       //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
                       var client = new Domain.Client();
                       client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
                       client.ContactId = contact.Id;
                       dc.Client.Add(client);

                       //добавим продукт
                       var product = new Domain.Product();
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


                       var DomainSale = dc.Sale.First();
                       var ModelsSale = new Models.Sale();

                       Mapping.Map(DomainSale, ModelsSale);

                        */


                        //проверка менеджера

                        //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
                        var manager = new Domain.Manager();
                        manager.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d88");
                        //manager.ContactId = contact.Id;
                        //dc.Manager.Add(manager);

                        var repos = new GenericRepository<Domain.Manager>((DbContext)dc);

                        try
                        {
                            var managerId = repos.Get(dc.Manager, manager.Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного менеджера нет в БД");
                        }

                        //проверка клиента


                        //проверка продукта



                        //var ex = new Exception("Данного менеджера нет в БД");
                        //ExceptionUtility.ProcessException(new Object(), ex);

                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
