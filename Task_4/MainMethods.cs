﻿using System;
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

namespace Task_4
{
    public static class MainMethods
    {
        #region private static fields

        private const string ADMINID = "80AB7036-5D4A-11E6-9903-0050569977A1";
        private static Guid adminGuid = Guid.Parse(ADMINID);

        private static string PATH = ConfigurationSettings.AppSettings["file"];

        private const string VALIDATEREGEX = @"\w_\d{8}.csv$";

        private static IMapper mapper = BLL.Mapper.SetupMapping.SetupMapper();

        private static ContactService contactService = new ContactService(mapper);
        private static ManagerService managerService = new ManagerService(mapper);
        private static ClientService  clientService  = new ClientService (mapper);
        private static ProductService productService = new ProductService(mapper);

        #endregion

        #region static methods for Main

        public static void watcherCreated()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = ConfigurationSettings.AppSettings["folder"];
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
                Console.WriteLine("Press 'q' to quit.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        internal static void OnChanged(object source, FileSystemEventArgs e) =>
        // actions when adding a new file
        // file processing
        Task.Run(() =>
        {
            using (StreamReader streamReader = new StreamReader(e.FullPath, Encoding.Default))
            {
                byte[] bytes = streamReader.CurrentEncoding.GetBytes(streamReader.ReadToEnd());
                FileInfo fileInfo = new FileInfo(e.FullPath);
                // Check the format of the file name 1
                if (ValidateFileName(fileInfo.Name))
                {
                    WorkWithFile(bytes);
                    MessageUtility.ShowInformationMessage("OK");
                }
                else
                {
                    MessageUtility.ShowValidationMessage("Invalid file format!");
                }
            }
        });

        internal static void WorkWithFile(Byte[] file)
        {
            //1-2.IEnumerable<Sales>
            IEnumerable<Sales> sales = ParseCsv.ParseResource<Sales>(file, sale =>
            {
                sale.CreatedByUserId = adminGuid;
                sale.CreatedDateTime = DateTime.UtcNow;
            });

            // Check data whether there is a database (manager)
            ValidateData(sales);

            // write to the Sales database

            //3.AutoMapper BLL
            SalesService salesService = new SalesService(mapper);
            var saleBLL = MappingService.MappingForBLLEntities<BLL.Sale, BLL.Sales>(salesService, sales);

            //4.AutoMapper DAL
            SaleService saleService = new SaleService(mapper);
            var saleDAL = MappingService.MappingForDALEntities<DAL.Sale, BLL.Sale>(saleService, saleBLL);

            // find customers and products by their ID and, if not, create new IDs
            saleDAL = clientService.CheckNameId(saleDAL).Result;
            saleDAL = productService.CheckNameId(saleDAL).Result;

            // write to the sales database from the file
            saleService.Add(saleDAL);
            //SaveChangesWithException(saleService, "заказа");
        }

        internal static bool ValidateFileName(string fileName)
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

        internal static async void ValidateData(IEnumerable<Sales> sales)
        {
            // manager check
            try
            {
                var managerActiveTask = managerService.FindAsync(sales.First().CreatedByUserId);
                var managerActive = await managerActiveTask;
                MessageUtility.ShowInformationMessage("Manager found:" + managerActive.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Handler: {e}");
                MessageUtility.ShowErrorMessage("This manager is not in the database");
                throw new Exception("This manager is not in the database");
            }

            #region validate clients & products

            //// customer check
            //try
            //{
            //    var clients = sales.Select(s => s.ClientName).ToList();
            //    bool check = clientService.Check(clients).Result;
            //    if (check == false)
            //    {
            //        MessageUtility.ShowErrorMessage("Одного или нескольких клиентов нет в БД");
            //        throw new Exception("Одного или нескольких клиентов нет в БД");
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Exception Handler: {e}");
            //    MessageUtility.ShowErrorMessage("ERROR IN CLIENTS CHECKING");
            //}

            //// check products
            //try
            //{
            //    var products = sales.Select(s => s.ProductName).ToList();
            //    bool check = productService.Check(products).Result;
            //    if (check == false)
            //    {
            //        MessageUtility.ShowErrorMessage("Одного или нескольких продуктов нет в БД");
            //        throw new Exception("Одного или нескольких продуктов нет в БД");
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Exception Handler: {e}");
            //    MessageUtility.ShowErrorMessage("ERROR IN PRODUCTS CHECKING");
            //}

            #endregion
        }

        internal static void StartData()
        {
            //add contact "6acb9fb3-9213-49cd-abda-f9785a658d12"
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

            //add manager "6acb9fb3-9213-49cd-abda-f9785a658d88"
            //80AB7036-5D4A-11E6-9903-0050569977A1
            //var manager = new BLL.Manager();
            //manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-0050569977A1");
            //manager.ContactId = contact.Id;
            ////AutoMapper DAL
            //var managerDAL = MappingService.MappingForDALEntity(managerService, manager);
            //managerService.Add(managerDAL);
            //SaveChangesWithException(managerService, "менеджера");
            var manager = new BLL.Manager();
            //manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-005056997722");
            manager.Id = Guid.Parse("80AB7036-5D4A-11E6-9903-0050569977A1");
            manager.ContactId = contact.Id;
            //AutoMapper DAL
            var managerDAL = MappingService.MappingForDALEntity(managerService, manager);
            managerService.Add(managerDAL);
            SaveChangesWithException(managerService, "менеджера");

            //add client 6acb9fb3-9213-49cd-abda-f9785a658d55
            //var client = new BLL.Client();
            //client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d55");
            //client.ContactId = contact.Id;
            ////AutoMapper DAL
            //var clientDAL = MappingService.MappingForDALEntity(clientService, client);
            //clientService.Add(clientDAL);
            //SaveChangesWithException(clientService, "клиента");

            //var client = new BLL.Client();
            //client.Id = Guid.Parse("6acb9fb3-9213-49cd-abda-f9785a658d22");
            //client.ContactId = contact.Id;
            //client.Name = "Haidzel Iryna Ivanovna";
            ////AutoMapper DAL
            //var clientDAL = MappingService.MappingForDALEntity(clientService, client);
            //clientService.Add(clientDAL);
            //SaveChangesWithException(clientService, "клиента");

            //add product
            //var product = new BLL.Product();
            //product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc7d");
            ////AutoMapper DAL
            //var productDAL = MappingService.MappingForDALEntity(productService, product);
            //productService.Add(productDAL);
            //SaveChangesWithException(productService, "продукта");

            //var product = new BLL.Product();
            //product.Id = Guid.Parse("89a5c4a4-6d02-412f-bb58-55a09f8afc22");
            //product.Name = "boots";
            ////AutoMapper DAL
            //var productDAL = MappingService.MappingForDALEntity(productService, product);
            //productService.Add(productDAL);
            //SaveChangesWithException(productService, "продукта");
        }

        internal static void SaveChangesWithException(IService service, string text)
        {
            try
            {
                service.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageUtility.ShowErrorMessage("Error adding " + text + "! maybe " + text.Remove(text.Length - 1, 1) + " already exists");
            }
        }

        internal static void WorkWithSomeFiles()
        {
            //Проверка формата названия 1 файла
            if (ValidateFileName("Ivanov_08012020"))
            {
                // process 1 file
                Task.Run(() =>
                {
                    using (StreamReader streamReader = new StreamReader(PATH, Encoding.Default))
                    {
                        byte[] bytes = streamReader.CurrentEncoding.GetBytes(streamReader.ReadToEnd());
                        WorkWithFile(bytes);
                    }
                });
            }

            ////Проверка формата названия 2 файла
            //if (ValidateFileName("Ivanov_07012020"))
            //{
            //    //обработка 2 файла
            //    Task.Run(() =>
            //    {
            //        using (StreamReader streamReader = new StreamReader(PATH, Encoding.Default))
            //        {
            //            byte[] bytes = streamReader.CurrentEncoding.GetBytes(streamReader.ReadToEnd());
            //            WorkWithFile(bytes);
            //        }
            //    });
            //}
        }

        #endregion
    }
}
