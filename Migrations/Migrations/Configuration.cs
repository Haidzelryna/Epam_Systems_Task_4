using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Data.Entity;
using CsvHelper;
using System.Data.Entity.Validation;
using Domain;
using System.Data.Entity.Migrations;

namespace Migrations.Migrations
{
    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        private const string AdminId = "F84B5406-C71E-4189-9220-5B7290ECB863";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            var adminGuid = Guid.Parse(AdminId);

            Configuration.ParseResource(context.Sales, Resources.Resource._11,
                 sale =>
                 {
                     sale.CreatedByUserId = adminGuid;
                     sale.CreatedDateTime = DateTime.UtcNow;
                 });

            Configuration.RunEntityValidationException(() =>
            {
                context.SaveChanges();
            });
        }

            public static void ParseResource<TDataType>(IDbSet<TDataType> dbSet, byte[] resourceName, Action<TDataType> action = null)
           where TDataType : class
        {
            if (!dbSet.Any())
            {
                return;
            }
            ParseCollection(dbSet, ParseResource(resourceName, action).ToArray());
        }

        private static void ParseCollection<T>(IDbSet<T> dbSet, T[] entities) where T : class
        {
            dbSet.AddOrUpdate(entities);
        }

        public static IEnumerable<T> ParseResource<T>(byte[] resourceName, Action<T> action = null)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = new MemoryStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ApplicationException($"Resource {resourceName} not found.");
                }
                using (StreamReader reader = new StreamReader(stream))
                {
                    IEnumerable<T> entities = ReadCsv<T>(reader).ToList();
                    if (action != null)
                    {
                        foreach (T entity in entities)
                        {
                            action(entity);
                        }
                    }
                    return entities;
                }
            }
        }

        private static IEnumerable<T> ReadCsv<T>(StreamReader reader)
        {
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Configuration.WillThrowOnMissingField = false;
            csvReader.Configuration.IgnoreHeaderWhiteSpace = true;
            csvReader.Configuration.TrimFields = true;
            csvReader.Configuration.IgnoreReferences = true;
            return csvReader.GetRecords<T>().ToArray();
        }

        public static void RunEntityValidationException(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    if (exception is DbEntityValidationException)
                    {
                        throw new ApplicationException(GetValidationMessage((DbEntityValidationException)exception));
                    }
                }

                throw;
            }
            catch (DbEntityValidationException validationException)
            {
                throw new ApplicationException(GetValidationMessage(validationException));
            }
        }

        private static string GetValidationMessage(DbEntityValidationException validationException)
        {
            return $"Validation errors saving an entity to the database:"; // { GetFullMessage("\r\n") }";
        }

        //public static string GetFullMessage(this DbEntityValidationException exception, string separator = ";", bool messagesOnly = false)
        //{
        //    var messages = exception.EntityValidationErrors
        //            .SelectMany(validationResult => validationResult.ValidationErrors,
        //                (rslt, error) => error.PropertyName != null && !messagesOnly
        //                    ? $"{error.ErrorMessage} [{error.PropertyName}]"
        //                    : $"{error.ErrorMessage}")
        //            .Distinct();

        //    return string.Join(separator, messages);
        //}
    }
}
