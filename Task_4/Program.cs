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
                        //Стартовые данные, заполняем БД - начало

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
                        SaveChangesWithException(contactService, "контакта");


                        //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
                        var managerRepos = new GenericRepository<DAL.Manager>((DbContext)dc);
                        ManagerService managerService = new ManagerService(managerRepos, mapper);
                        var manager = new BLL.Manager();
                        manager.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d88");
                        manager.ContactId = contact.Id;
                        //AutoMapper DAL
                        var managerDAL = MappingForDALEntity(managerService, manager);
                        managerService.Add(managerDAL);
                        SaveChangesWithException(managerService, "менеджера");


                        //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
                        var clientRepos = new GenericRepository<DAL.Client>((DbContext)dc);
                        ClientService clientService = new ClientService(clientRepos, mapper);
                        var client = new BLL.Client();
                        client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
                        client.ContactId = contact.Id;
                        //AutoMapper DAL
                        var clientDAL = MappingForDALEntity(clientService, client);
                        clientService.Add(clientDAL);
                        SaveChangesWithException(clientService, "клиента");


                        //добавим продукт
                        var productRepos = new GenericRepository<DAL.Product>((DbContext)dc);
                        ProductService productService = new ProductService(productRepos, mapper);
                        var product = new BLL.Product();
                        product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
                        //AutoMapper DAL
                        var productDAL = MappingForDALEntity(productService, product);
                        productService.Add(productDAL);
                        SaveChangesWithException(productService, "продукта");

                        //Стартовые данные, заполняем БД - конец


                        //2.IEnumerable<Sales>
                        IEnumerable<Sales> sales = conf.ParseResource<Sales>(DAL.Resources.Resource.Ivanov_19112012, sale =>
                        {
                            sale.CreatedByUserId = adminGuid;
                           // sales.CreatedDateTime = DateTime.UtcNow;
                        });

                        //3.AutoMapper BLL
                        SalesService salesService = new SalesService(mapper);
                        var saleBLL = MappingForBLLEntities<BLL.Sale, BLL.Sales>(salesService, sales);

                        //4.AutoMapper DAL
                        var saleRepos = new GenericRepository<DAL.Sale>((DbContext)dc);
                        SaleService saleService = new SaleService(saleRepos, mapper);
                        var saleDAL = MappingForDALEntities<DAL.Sale, BLL.Sale>(saleService, saleBLL);


                        //проверка менеджера
                        try
                        {
                            var managerActive = managerService.Find(sales.First().CreatedByUserId);
                            MessageUtility.ShowValidationMessage(new Object(), "Менеджер найден:" + managerActive.Name);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного менеджера нет в БД");
                        }

                        //проверка клиентов
                        try
                        {
                            IEnumerable<Guid> clients = sales.Select(s => s.ClientId).ToList();
                            bool i = CheckAsync(clientService, clients);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного клиента нет в БД");
                        }

                        //проверка продуктов
                        try
                        {
                            //var productId = repos.Get(dc.Product, sales.First().ProductId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception Handler: {e}");
                            MessageUtility.ShowErrorMessage(new Object(), "Данного продукта нет в БД");
                        }


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

        static void SaveChangesWithException(IService service, string text)
        {
            try
            {
                service.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении " + text + "! Возможно " + text.Remove(text.Length-1,1) + " уже существует");
            }
        }

        static async Task<bool> CheckAsync(ClientService service, IEnumerable<Guid> clients)
        {
            bool result = await service.Check(clients);
            return result;
        }

    }
}
