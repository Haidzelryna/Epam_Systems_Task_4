using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using BLL;
using BLL.Services;
using BLL.Exception;
using BLL.Classes.Mapper;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        private static ClientService  clientService  = new ClientService (mapper);
        private static ProductService productService = new ProductService(mapper);

        private static string PATH; 

        static void Main(string[] args)
        {
            adminGuid = Guid.Parse(ADMINID);

            PATH = ConfigurationSettings.AppSettings["file"];

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.csv";

                // Add event handlers.
                watcher.Created += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }


            //Стартовые данные, заполняем БД
            //StartData();

            //Проверка формата названия 1 файла
            if (ValidateFileName("Ivanov_19112012"))
            {
                //обработка 1 файла
                Task.Run(() =>
                {
                    using (StreamReader streamReader = new StreamReader(PATH, Encoding.Default))
                    {
                        byte[] bytes = streamReader.CurrentEncoding.GetBytes(streamReader.ReadToEnd());
                        WorkWithFile(bytes);
                    }
                });
            }

            //Проверка формата названия 2 файла
            if (ValidateFileName("Ivanov_07012020"))
            {
                //обработка 2 файла
                Task.Run(() =>
                {
                    using (StreamReader streamReader = new StreamReader(PATH, Encoding.Default))
                    {
                        byte[] bytes = streamReader.CurrentEncoding.GetBytes(streamReader.ReadToEnd());
                        WorkWithFile(bytes);
                    }
                });
            }

            Console.ReadLine();
        }



        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e) =>
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");




        static void WorkWithFile(Byte[] file)
        {
            //1-2.IEnumerable<Sales>
            IEnumerable<Sales> sales = ParseCsv.ParseResource<Sales>(file, sale =>
            {
                sale.CreatedByUserId = adminGuid;
                // sales.CreatedDateTime = DateTime.UtcNow;
            });

            //Проверка данных, есть ли в БД
            ValidateData(sales);

            //запись в БД Sales

            //3.AutoMapper BLL
            SalesService salesService = new SalesService(mapper);
            var saleBLL = MappingService.MappingForBLLEntities<BLL.Sale, BLL.Sales>(salesService, sales);

            //4.AutoMapper DAL
            SaleService saleService = new SaleService(mapper);
            var saleDAL = MappingService.MappingForDALEntities<DAL.Sale, BLL.Sale>(saleService, saleBLL);

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
            //var contact = new BLL.Contact();
            //contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d12");
            //contact.FirstName = "Гайдель";
            //contact.LastName = "Ирина";
            ////AutoMapper DAL
            //var contactDAL = MappingService.MappingForDALEntity(contactService, contact);
            //contactService.Add(contactDAL);
            //SaveChangesWithException(contactService, "контакта");
            var contact = new BLL.Contact();
            contact.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d22");
            contact.FirstName = "Гайдель";
            contact.LastName = "Лев";
            //AutoMapper DAL
            var contactDAL = MappingService.MappingForDALEntity(contactService, contact);
            contactService.Add(contactDAL);
            SaveChangesWithException(contactService, "контакта");

            //добавим менеджера "6acb9fb3-9213-49cd-abda-f9785a658d88"
            //80AB7036-5D4A-11E6-9903-0050569977A1
            //var manager = new BLL.Manager();
            //manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-0050569977A1");
            //manager.ContactId = contact.Id;
            ////AutoMapper DAL
            //var managerDAL = MappingService.MappingForDALEntity(managerService, manager);
            //managerService.Add(managerDAL);
            //SaveChangesWithException(managerService, "менеджера");
            var manager = new BLL.Manager();
            manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-005056997722");
            manager.ContactId = contact.Id;
            //AutoMapper DAL
            var managerDAL = MappingService.MappingForDALEntity(managerService, manager);
            managerService.Add(managerDAL);
            SaveChangesWithException(managerService, "менеджера");

            //добавим клиента 6acb9fb3-9213-49cd-abda-f9785a658d55
            //var client = new BLL.Client();
            //client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
            //client.ContactId = contact.Id;
            ////AutoMapper DAL
            //var clientDAL = MappingService.MappingForDALEntity(clientService, client);
            //clientService.Add(clientDAL);
            //SaveChangesWithException(clientService, "клиента");
            var client = new BLL.Client();
            client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d22");
            client.ContactId = contact.Id;
            //AutoMapper DAL
            var clientDAL = MappingService.MappingForDALEntity(clientService, client);
            clientService.Add(clientDAL);
            SaveChangesWithException(clientService, "клиента");

            //добавим продукт
            //var product = new BLL.Product();
            //product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
            ////AutoMapper DAL
            //var productDAL = MappingService.MappingForDALEntity(productService, product);
            //productService.Add(productDAL);
            //SaveChangesWithException(productService, "продукта");
            var product = new BLL.Product();
            product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc22");
            //AutoMapper DAL
            var productDAL = MappingService.MappingForDALEntity(productService, product);
            productService.Add(productDAL);
            SaveChangesWithException(productService, "продукта");
        }

        static void SaveChangesWithException(IService service, string text)
        {
            try
            {
                service.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageUtility.ShowErrorMessage(new Object(), "Ошибка при добавлении " + text + "! Возможно " + text.Remove(text.Length - 1, 1) + " уже существует");
            }
        }
    }
}
