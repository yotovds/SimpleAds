using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAds.Services.Contracs
{
    public interface IAdsService
    {
        Task<int> CreateAdAsync(CreateAdInputModel inputModel, string userId);
    }
}
