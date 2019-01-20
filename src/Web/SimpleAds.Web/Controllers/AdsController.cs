using AutoMapper;
using GlobalConstans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data.Models;
using SimpleAds.Services.Contracs;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAds.Web.Controllers
{
    [Authorize]
    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;
        private readonly IMapper mapper;

        public AdsController(UserManager<SimpleAdsUser> userManager, IAdsService adsService, IMapper mapper)
            : base(userManager)
        {
            this.adsService = adsService;
            this.mapper = mapper;
        }

        [Authorize(Roles = StringConstants.UserRole)]
        public IActionResult Create()
        {
            return this.View();
        }
                
        [HttpPost]
        [Authorize(Roles = StringConstants.UserRole)]
        public async Task<IActionResult> Create(CreateAdInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var adId = await this.adsService.CreateAdAsync(inputModel, CurrentUser.Id);

            return this.RedirectToAction("Details", new { id = adId});
        }

        [Authorize(Roles = StringConstants.UserRole + ", " + StringConstants.AdminRole)]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.adsService.GetAdViewModelAsync(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.AdminRole)]
        public IActionResult ApproveAd(int id)
        {
            this.adsService.ApproveAd(id);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.AdminRole)]
        public IActionResult RejectAd(int id, string message)
        {
            this.adsService.RejectAd(id, message);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.UserRole)]
        public async Task<IActionResult> RepostAd(int id)
        {
            var adId = await this.adsService.RepostAdAsync(id, CurrentUser.Id);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = StringConstants.UserRole + ", " + StringConstants.AdminRole)]
        public IActionResult DeleteAd(int id)
        {
            this.adsService.DeleteAd(id, CurrentUser.Id);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = StringConstants.UserRole)]
        public IActionResult Edit(AdViewModel viewModel)
        {
            var editModel = this.adsService.GetEditViewModel(viewModel, CurrentUser.Id);

            return this.View(editModel);
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.UserRole)]
        public async Task<IActionResult> EditAd(int id)
        {
            var viewModel = await this.adsService.GetAdViewModelAsync(id);

            return this.RedirectToAction("Edit", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.UserRole)]
        public IActionResult Save(AdEditModel editModel)
        {
            var adId = this.adsService.Update(editModel, CurrentUser.Id);

            return this.RedirectToAction("Details", new { id = adId });
        }
    }
}
