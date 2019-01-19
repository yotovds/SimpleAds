using SimpleAds.Services.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleAds.Data.Models
{
    public class PendingAd : BaseAd
    {
        public Expiration  ExpirationAfter{ get; set; }
    }
}
