using System;
using System.Collections.Generic;
using BLL.Services;

namespace BLL.Classes.Mapper
{
    public static class MappingService
    {
        public static IEnumerable<T> MappingForBLLEntities<T, V>(IService<T, V> service, IEnumerable<V> entities)
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

        public static T MappingForDALEntity<T, V>(IService<T, V> service, V entity)
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

        public static IEnumerable<T> MappingForDALEntities<T, V>(IService<T, V> service, IEnumerable<V> entities)
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
