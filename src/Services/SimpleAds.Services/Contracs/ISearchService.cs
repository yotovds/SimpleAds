using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAds.Services.Contracs
{
    public interface ISearchService
    {
        IEnumerable<AdViewModel> Search(string name);
    }
}
