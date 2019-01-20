using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAds.Services.ViewModels.Ads
{
    public class AdViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string ExpirationOn { get; set; }

        public int Status { get; set; }
    }
}
