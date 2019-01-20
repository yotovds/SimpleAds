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

        public AdsController(UserManager<SimpleAdsUser> userManager, IAdsService adsService)
            : base(userManager)
        {
            this.adsService = adsService;
        }

        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var adId = await this.adsService.CreateAdAsync(inputModel, CurrentUser.Id);

            return this.RedirectToAction("Details", new { id = adId});
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.adsService.GetAdViewModelAsync(id);

            return this.View(viewModel);
        }
    }
}
