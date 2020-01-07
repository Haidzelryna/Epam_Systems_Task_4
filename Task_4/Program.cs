using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using BLL;
using BLL.Services;
using BLL.Exception;

namespace Task_4
{
    class Program
    {
        private const string ADMINID = "80AB7036-5D4A-11E6-9903-0050569977A1";

        private static Guid adminGuid;

        private const string VALIDATEREGEX = @"\w_\d{8}$";

        private static IMapper mapper = BLL.Mapper.SetupMapping.SetupMapper();

        private static ContactService contactService = new ContactService(mapper);
        private static ManagerService managerService = new ManagerService(mapper);
        private static ClientService clientService = new ClientService(mapper);
        private static ProductService productService = new ProductService(mapper);

        static void Main(string[] args)
        {
            adminGuid = Guid.Parse(ADMINID);

            //Стартовые данные, заполняем БД
            StartData();

            //Проверка формата названия файла
            if (ValidateFileName("Ivanov_19112012"))
            {
                //обработка файла
                WorkWithFile();

                Console.ReadLine();
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

        static void WorkWithFile()
        {
            //1-2.IEnumerable<Sales>
            IEnumerable<Sales> sales = ParseCsv.ParseResource<Sales>(BLL.Resources.Resource.Ivanov_19112012, sale =>
            {
                sale.CreatedByUserId = adminGuid;
                // sales.CreatedDateTime = DateTime.UtcNow;
            });

            //Проверка данных, есть ли в БД
            ValidateData(sales);

            //запись в БД Sales

            //3.AutoMapper BLL
            SalesService salesService = new SalesService(mapper);
            var saleBLL = MappingForBLLEntities<BLL.Sale, BLL.Sales>(salesService, sales);

            //4.AutoMapper DAL
            SaleService saleService = new SaleService(mapper);
            var saleDAL = MappingForDALEntities<DAL.Sale, BLL.Sale>(saleService, saleBLL);

            //запись в БД sales из файла
            saleService.Add(saleDAL);
            SaveChangesWithException(saleService, "заказа");
        }

        static bool ValidateFileName(string fileName)
        {
            string line = Regex.Replace(fileName.Trim(), @"\s+", @" ");
            if (line != "")
            {
                if (Regex.IsMatch(line, VALIDATEREGEX))
                {
                    return true;
                }
            }
            return false;
        }

        static void ValidateData(IEnumerable<Sales> sales)
        {
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
                throw new Exception("Данного менеджера нет в БД");
            }

            //проверка клиентов
            try
            {
                IEnumerable<Guid> clients = sales.Select(s => s.ClientId).ToList();
                bool check = clientService.Check(clients).Result;
                if (check == false)
                {
                    MessageUtility.ShowErrorMessage(new Object(), "Одного или нескольких клиентов нет в БД");
                    throw new Exception("Одного или нескольких клиентов нет в БД");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Handler: {e}");
                MessageUtility.ShowErrorMessage(new Object(), "ERROR IN CLIENTS CHECKING");
            }

            //проверка продуктов
            try
            {
                IEnumerable<Guid> products = sales.Select(s => s.ProductId).ToList();
                bool check = productService.Check(products).Result;
                if (check == false)
                {
                    MessageUtility.ShowErrorMessage(new Object(), "Одного или нескольких продуктов нет в БД");
                    throw new Exception("Одного или нескольких продуктов нет в БД");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Handler: {e}");
                MessageUtility.ShowErrorMessage(new Object(), "ERROR IN PRODUCTS CHECKING");
            }
        }

        static void StartData()
        {
            //добавим контакт "6acb9fb3-9213-49cd-abda-f9785a658d12"
            var contact = new BLL.Contact();
            contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d12");
            contact.FirstName = "Гайдель";
            contact.LastName = "Ирина";
            //AutoMapper DAL
            var contactDAL = MappingForDALEntity(contactService, contact);
            contactService.Add(contactDAL);
            SaveChangesWithException(contactService, "контакта");


            //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
            //80AB7036-5D4A-11E6-9903-0050569977A1
            var manager = new BLL.Manager();
            manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-0050569977A1");
            manager.ContactId = contact.Id;
            //AutoMapper DAL
            var managerDAL = MappingForDALEntity(managerService, manager);
            managerService.Add(managerDAL);
            SaveChangesWithException(managerService, "менеджера");


            //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
            var client = new BLL.Client();
            client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
            client.ContactId = contact.Id;
            //AutoMapper DAL
            var clientDAL = MappingForDALEntity(clientService, client);
            clientService.Add(clientDAL);
            SaveChangesWithException(clientService, "клиента");


            //добавим продукт
            var product = new BLL.Product();
            product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
            //AutoMapper DAL
            var productDAL = MappingForDALEntity(productService, product);
            productService.Add(productDAL);
            SaveChangesWithException(productService, "продукта");
        }
    }
}
