using AutoMapper;
using SimpleAds.Data;
using SimpleAds.Data.Models;
using SimpleAds.Services.Contracs;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
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
            var ad = this.Mapper.Map<PendingAd>(inputModel);
            ad.AdCreatorId = userId;
            //TODO: Add cloudinary to store images 
            ad.ImageUrl = "https://res.cloudinary.com/dr8axwivq/image/upload/v1546794753/test.jpg";

            await this.DbContext.PendingAds.AddAsync(ad);
            await this.DbContext.SaveChangesAsync();

            return ad.Id;
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
