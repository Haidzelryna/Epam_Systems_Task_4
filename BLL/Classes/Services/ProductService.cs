using System;
using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class ProductService: IService<DAL.Product, BLL.Product>
    {
        private readonly IGenericRepository<DAL.Product> _productRepository;
        private readonly IMapper _mapper;

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
            return await _productRepository.GetAllAsync();
        }

        public async Task<bool> Check(IEnumerable<Guid> productsCheck)
        {
            IEnumerable<DAL.Product> products = await GetAll();
            foreach (Guid productId in productsCheck)
            {
                if (products.Select(p => p.Id).ToList().Contains(productId) == false)
                {
                    return false;
                };
            }
            return true;
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
