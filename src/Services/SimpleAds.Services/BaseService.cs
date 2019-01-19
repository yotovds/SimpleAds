using AutoMapper;
using SimpleAds.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAds.Services
{
    public abstract class BaseService
    {
        private readonly SimpleAdsDbContext context;
        private readonly IMapper mapper;

        protected BaseService(SimpleAdsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected SimpleAdsDbContext DbContext
        {
            get
            {
                return this.context;
            }
        }

        protected IMapper Mapper
        {
            get
            {
                return this.mapper;
            }
        }

    }
}
