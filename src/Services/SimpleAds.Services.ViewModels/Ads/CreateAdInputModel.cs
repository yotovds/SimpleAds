using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
//TODO: Add validation
namespace SimpleAds.Services.ViewModels.Ads
{
    public class CreateAdInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        public Expiration ExpirationAfter { get; set; }

        public enum Expiration
        {
            Day = 1,
            Week = 2,
            Month = 3
        }
    }    
}