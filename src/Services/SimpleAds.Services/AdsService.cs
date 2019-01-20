using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleAds.Data;
using SimpleAds.Data.Models;
using SimpleAds.Data.Models.Enums;
using SimpleAds.Services.Contracs;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expiration = SimpleAds.Data.Models.Enums.Expiration;

namespace SimpleAds.Services
{
    public class AdsService : BaseService, IAdsService
    {
        public AdsService(SimpleAdsDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public void ApproveAd(int adId)
        {
            var ad = this.DbContext
                .Ads
                .Where(a => a.Id == adId)
                .FirstOrDefault();

            ad.Status = Status.Approved;
            ad.ExpirationOn = SetExpirationDate((int)ad.ExpirationAfter);

            this.DbContext.SaveChanges();
        }

        public async Task<int> CreateAdAsync(CreateAdInputModel inputModel, string userId)
        {
            var ad = this.Mapper.Map<Ad>(inputModel);
            ad.AuthorId = userId;
            ad.CreatedOn = DateTime.UtcNow;
            ad.ImageUrl = UploadImage(inputModel.Image);            

            ad.Status = Status.Created;

            ad.Status = Status.PendingApproval;
            await this.DbContext.Ads.AddAsync(ad);
            await this.DbContext.SaveChangesAsync();

            return ad.Id;
        }

        public void DeleteAd(int id, string userId)
        {
            var ad = this.DbContext
                .Ads
                .Where(a => a.Id == id && a.AuthorId == userId)
                .FirstOrDefault();

            this.DbContext.Remove(ad);

            this.DbContext.SaveChanges();
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

        public IEnumerable<AdViewModel> GetAllActiveAds()
        {
            var ads = this.DbContext
                .Ads
                .Where(a => a.Status == Status.Approved);

            var adsViewModel = this.Mapper.Map<IEnumerable<AdViewModel>>(ads);

            return adsViewModel;
        }

        public AdEditModel GetEditViewModel(AdViewModel viewModel, string userId)
        {
            if (viewModel.AuthorId == userId)
            {
                var editModel = new AdEditModel
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    Category = viewModel.Category,
                    ImageUrl = viewModel.ImageUrl
                };

                return editModel;
            }

            return null;
            
        }

        public IEnumerable<AdViewModel> GetPendingAds()
        {
            var ads = this.DbContext
                .Ads
                .Where(a => a.Status <= Status.PendingApproval);

            var viewModel = this.Mapper.Map<IEnumerable<AdViewModel>>(ads);

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

        public void RejectAd(int id, string message)
        {
            var ad = this.DbContext
                   .Ads
                   .Where(a => a.Id == id)
                   .FirstOrDefault();

            ad.RejectMessage = message;
            ad.Status = Status.Created;

            this.DbContext.SaveChanges();
        }

        public async Task<int> RepostAdAsync(int id, string userId)
        {
            var ad = await this.DbContext
                .Ads
                .Where(a => a.Id == id && a.AuthorId == userId)
                .FirstOrDefaultAsync();

            ad.Status = Status.Created;

            await this.DbContext.SaveChangesAsync();

            return ad.Id;
        }

        public int Update(AdEditModel editModel, string userId)
        {
            var ad = this.DbContext
                .Ads
                .Where(a => a.Id == editModel.Id && a.AuthorId == userId)
                .FirstOrDefault();

            ad.Title = editModel.Title;
            ad.Content = editModel.Content;
            ad.Category = editModel.Category;
            ad.CreatedOn = DateTime.UtcNow;
            ad.RejectMessage = string.Empty;
            ad.ExpirationAfter = (Expiration)editModel.ExpirationAfter;
            if (editModel.Image != null)
            {
                ad.ImageUrl = UploadImage(editModel.Image);
            }
            ad.Status = Status.PendingApproval;

            this.DbContext.SaveChanges();

            return ad.Id;
        }

        private DateTime SetExpirationDate(int expirationEnumValue)
        {
            var expirationDate = DateTime.UtcNow;

            switch (expirationEnumValue)
            {
                case 1:
                    expirationDate = expirationDate.AddDays(1);
                    break;
                case 2:
                    expirationDate = expirationDate.AddDays(7);
                    break;
                case 3:
                    expirationDate = expirationDate.AddMonths(1);
                    break;
            }

            return expirationDate;
        }

        private string UploadImage(IFormFile image)
        {
            string imageUrl = "https://res.cloudinary.com/dr8axwivq/image/upload/v1546794753/test.jpg";
            if (image != null)
            {
                Account account = new Account("dr8axwivq", "766763689436115", "I9KoG0cgt3QoCd3Dp2K2QpHMpsM");
                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(@"image.jpg", image.OpenReadStream())
                };
                var uploadResult = cloudinary.Upload(uploadParams);


                imageUrl = uploadResult.Uri.ToString();
            }

            return imageUrl;
        }
    }
}
