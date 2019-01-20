using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleAds.Data;
using SimpleAds.Data.Models;
using SimpleAds.Services.Contracs;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAds.Services
{
    public class AdsService : BaseService, IAdsService
    {
        public AdsService(SimpleAdsDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<int> CreateAdAsync(CreateAdInputModel inputModel, string userId)
        {
            var ad = this.Mapper.Map<Ad>(inputModel);
            ad.AuthorId = userId;
            ad.CreatedOn = DateTime.UtcNow;
            ad.Status = Data.Models.Enums.Status.Created;
            //TODO: Add cloudinary to store images 
            ad.ImageUrl = "https://res.cloudinary.com/dr8axwivq/image/upload/v1546794753/test.jpg";

            await this.DbContext.Ads.AddAsync(ad);
            await this.DbContext.SaveChangesAsync();

            return ad.Id;
        }

        public async Task<AdViewModel> GetAdViewModelAsync(int adId)
        {
            var ad = await this.DbContext
                .Ads
                .Where(a => a.Id == adId)
                .FirstOrDefaultAsync();

            var viewModel = this.Mapper.Map<AdViewModel>(ad);

            return viewModel;
        }

        public IEnumerable<AdViewModel> GetUserAds(string userId)
        {
            var ads = this.DbContext
                .Ads
                .Where(a => a.AuthorId == userId);

            var viewModel = this.Mapper.Map<IEnumerable<AdViewModel>>(ads);

            return viewModel;
        }

        //private DateTime SetExpirationDate(int expirationEnumValue)
        //{
        //    var expirationDate = DateTime.UtcNow;

        //    switch (expirationEnumValue)
        //    {
        //        case 1:
        //            expirationDate = expirationDate.AddDays(1);
        //            break;
        //        case 2:
        //            expirationDate = expirationDate.AddDays(7);
        //            break;
        //        case 3:
        //            expirationDate = expirationDate.AddMonths(1);
        //            break;
        //    }

        //    return expirationDate;
        //}
    }
}
