﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Repository;

namespace BLL.Servises
{
    public class SalesService
    {
        private readonly IGenericRepository<Sale> _saleRepository;
        private readonly IMapper _mapper;

        public SalesService(IGenericRepository<Sale> saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sale>> Get()
        {
            var saleEntities = await _saleRepository.GetAsync();
            return _mapper.Map<IEnumerable<Sale>>(saleEntities);
        }
    }
}
