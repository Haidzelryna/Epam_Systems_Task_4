using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using CsvHelper;

namespace BLL
{
    public static class ParseCsv
    {
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
            csvReader.Configuration.Delimiter = ";";
            return csvReader.GetRecords<T>().ToArray();
        }
    }
}
