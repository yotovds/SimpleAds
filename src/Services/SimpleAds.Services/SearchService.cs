using AutoMapper;
using SimpleAds.Data;
using SimpleAds.Services.Contracs;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAds.Services
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly IAdsService adsService;

        public SearchService(SimpleAdsDbContext context, IMapper mapper, IAdsService adsService)
            : base(context, mapper)
        {
            this.adsService = adsService;
        }

        public IEnumerable<AdViewModel> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var ads = this.adsService
                .GetAllActiveAds()
                .Where(a => a.Title.ToLower().Contains(name.ToLower()));

            return ads;
        }
    }
}
