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

        Task<AdViewModel> GetAdViewModelAsync(int adId);

        AdEditModel GetEditViewModel(AdViewModel viewModel, string userId);

        IEnumerable<AdViewModel> GetUserAds(string userId);

        IEnumerable<AdViewModel> GetPendingAds();

        void ApproveAd(int adId);

        Task<int> RepostAdAsync(int id, string userId);

        void DeleteAd(int id, string userId);

        void RejectAd(int id, string message);

        int Update(AdEditModel editModel, string userId);

        IEnumerable<AdViewModel> GetAllActiveAds();
    }
}
