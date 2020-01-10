using System;
using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;
using System.Threading.Tasks;
using System.Linq;
using BLL.Classes.Services;

namespace BLL.Services
{
    public class ProductService: IService<DAL.Product, BLL.Product>
    {
        private readonly IGenericRepository<DAL.Product> _productRepository;
        private readonly IMapper _mapper;

        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();

        public ProductService(IMapper mapper)
        {
            _productRepository = new GenericRepository<DAL.Product>();
            _mapper = mapper;
        }

        public DAL.Product Get(BLL.Product Entity)
        {
            return _mapper.Map<DAL.Product>(Entity);
        }

        public IEnumerable<DAL.Product> Get(IEnumerable<BLL.Product> Entities)
        {
            return _mapper.Map<IEnumerable<DAL.Product>>(Entities);
        }

        public async Task<IEnumerable<DAL.Product>> GetAll()
        {
            IEnumerable<DAL.Product> result = Enumerable.Empty<DAL.Product>();
            await _locker.LockAsync(async () =>
            {
                result = await _productRepository.GetAllAsync();
            });
            return result;
        }

        public async Task<bool> Check(IEnumerable<string> productsCheck)
        {
            IEnumerable<DAL.Product> products = await GetAll();
            foreach (string productName in productsCheck)
            {
                if (products.Select(p => p.Name).ToList().Contains(productName) == false)
                {
                    return false;
                };
            }
            return true;
        }

        //для сопоставления Id - name
        public async Task<IEnumerable<DAL.Sale>> CheckNameId(IEnumerable<DAL.Sale> Entities)
        {
            IEnumerable<DAL.Product> products = await GetAll();
            if (products.Any())
            {
                foreach (var sale in Entities)
                {
                    Guid idProduct = new Guid();
                    var products1 = products.Where(c => c.Name == sale.ProductName);
                    var i = products1.Where(x => x != null).Select(c => c.Id);
                    if (i.Count() > 0)
                    {
                        idProduct = i.Where(x => x != null).First();
                    }
                    //создать в БД
                    else
                    {
                        DAL.Product prod = new DAL.Product();
                        prod.Id = Guid.NewGuid();
                        prod.Name = sale.ProductName;
                        Add(prod);
                        SaveChanges();
                        idProduct = prod.Id;
                    }
                    sale.ProductId = idProduct;
                }
            }
            return Entities;
        }

        public void Remove(DAL.Product Entity)
        {
            _productRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Product> Entities)
        {
            _productRepository.Delete(Entities);
        }

        public void Add(DAL.Product Entity)
        {
            _productRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Product> Entities)
        {
            _productRepository.Add(Entities);
        }

        public void SaveChanges()
        {
            _productRepository.SaveChanges();
        }

        public DAL.Product Find(Guid productId)
        {
            return _productRepository.Find(productId);
        }
    }
}
