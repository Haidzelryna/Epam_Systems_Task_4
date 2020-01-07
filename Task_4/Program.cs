using System;
using BLL;
using Database.Migrations;
using BLL.Mapper;
using DAL;
using System.Linq;
using DAL.Repository;
using System.Data.Entity;
using BLL;
using BLL.Exception;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AutoMapper;
using BLL.Services;
using System.Threading.Tasks;
using BLL.Mapper;
using System.Collections.Generic;
//using DAL;

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
                IMapper mapper = BLL.Mapper.SetupMapping.SetupMapper();

               

                string line = Regex.Replace("Ivanov_19112012".Trim(), @"\s+", @" ");
                if (line != "")
                {
                    if (Regex.IsMatch(line, VALIDATEREGEX))
                    {

                        //добавим контакт "6acb9fb3-9213-49cd-abda-f9785a658d12"
                        var contactRepos = new GenericRepository<DAL.Contact>((DbContext)dc);
                        ContactService contactService = new ContactService(contactRepos, mapper);
                        var contact = new BLL.Contact();
                        contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d12");
                        contact.FirstName = "Гайдель";
                        contact.LastName = "Ирина";
                        //AutoMapper DAL
                        var contactDAL = MappingForDALEntity(contactService, contact);
                        contactService.Add(contactDAL);
                        try
                        {
                            contactService.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении контакта! Возможно контакт уже существует");
                        }


                        //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
                        var managerRepos = new GenericRepository<DAL.Manager>((DbContext)dc);
                        ManagerService managerService = new ManagerService(managerRepos, mapper);
                        var manager = new BLL.Manager();
                        manager.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d88");
                        manager.ContactId = contact.Id;
                        //AutoMapper DAL
                        var managerDAL = MappingForDALEntity(managerService, manager);
                        managerService.Add(managerDAL);
                        try
                        {
                            managerService.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении менеджера! Возможно менеджер уже существует");
                        }


                        //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
                        var clientRepos = new GenericRepository<DAL.Client>((DbContext)dc);
                        ClientService clientService = new ClientService(clientRepos, mapper);
                        var client = new BLL.Client();
                        client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
                        client.ContactId = contact.Id;
                        //AutoMapper DAL
                        var clientDAL = MappingForDALEntity(clientService, client);
                        clientService.Add(clientDAL);
                        try
                        {
                            clientService.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении клиента! Возможно клиент уже существует");
                        }


                        //добавим продукт
                        var productRepos = new GenericRepository<DAL.Product>((DbContext)dc);
                        ProductService productService = new ProductService(productRepos, mapper);
                        var product = new BLL.Product();
                        product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
                        //AutoMapper DAL
                        var productDAL = MappingForDALEntity(productService, product);
                        productService.Add(productDAL);
                        try
                        {
                            productService.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении продукта! Возможно продукт уже существует");
                        }


                        //    sale =>
                        //{
                        //    sale.CreatedByUserId = adminGuid;
                        //    sale.CreatedDateTime = DateTime.UtcNow;
                        //});

                        //conf.RunEntityValidationException(() =>
                        // {
                        //     dc.SaveChanges();
                        // });


                        //2.IEnumerable<Sales>
                        IEnumerable<Sales> sales = conf.ParseResource<Sales>(DAL.Resources.Resource.Ivanov_19112012, sale =>
                        {
                            sale.CreatedByUserId = adminGuid;
                           // sales.CreatedDateTime = DateTime.UtcNow;
                        });


                        //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
                        //var manager = new Domain.Manager();
                        //manager.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d88");
                        //manager.ContactId = contact.Id;
                        //dc.Manager.Add(manager);

                        //var repos = new GenericRepository<BLL.Manager>((DbContext)dc);


                        //3.AutoMapper BLL

                        SalesService salesService = new SalesService(mapper);
                        var saleBLL = MappingForBLLEntities<BLL.Sale, BLL.Sales>(salesService, sales);

                        //4.AutoMapper DAL
                        var saleRepos = new GenericRepository<DAL.Sale>((DbContext)dc);
                        SaleService saleService = new SaleService(saleRepos, mapper);
                        var saleDAL = MappingForDALEntities<DAL.Sale, BLL.Sale>(saleService, saleBLL);


                        //var outer = Task.Factory.StartNew(() =>      // внешняя задача
                        //{
                        //    await GetData(DbContext dc);

                        //    //Console.WriteLine("Outer task starting...");
                        //    //var inner = Task.Factory.StartNew(() =>  // вложенная задача
                        //    //{
                        //    //    Console.WriteLine("Inner task starting...");
                        //    //    Thread.Sleep(2000);
                        //    //    Console.WriteLine("Inner task finished.");
                        //    //});
                        //});
                        //outer.Wait(); // ожидаем выполнения внешней задачи
                        //Console.WriteLine("End of Main");




                        //проверка менеджера
                        try
                        {
                            //var managerId = repos.Get(dc.Manager, sales.First().CreatedByUserId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного менеджера нет в БД");
                        }
                        //проверка клиента
                        try
                        {
                            //var clientId = repos.Get(dc.Client, sales.First().ClientId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного клиента нет в БД");
                        }
                        //проверка продукта
                        try
                        {
                            //var productId = repos.Get(dc.Product, sales.First().ProductId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного продукта нет в БД");
                        }


                        //var ex = new Exception("Данного менеджера нет в БД");
                        //ExceptionUtility.ProcessException(new Object(), ex);

                        Console.WriteLine();

                        Console.ReadLine();
                    }
                }
            }
        }

        static IEnumerable<T> MappingForBLLEntities<T,V>(IService<T, V> service, IEnumerable<V> entities)
        {          
            try
            {
                var i = service.Get(entities);
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        static T MappingForDALEntity<T, V>(IService<T, V> service, V entity)
        {
            //try
            //{
                var i = service.Get(entity);
                return i;
           // }
          //  catch (Exception ex)
           // {
          //      Console.WriteLine(ex.Message);
          //  }

          //  return ;
        }

        static IEnumerable<T> MappingForDALEntities<T,V>(IService<T, V> service, IEnumerable<V> entities)
        {
            try
            {
                var i = service.Get(entities);
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

    }
}
