using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GlobalConstans;
using Microsoft.AspNetCore.Http;

namespace SimpleAds.Services.ViewModels.Ads
{
    public class CreateAdInputModel
    {
        [Required]
        [StringLength(25, ErrorMessage = StringConstants.StringLength, MinimumLength = 5)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = StringConstants.StringLength, MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = StringConstants.StringLength, MinimumLength = 3)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Expiration After")]
        public Expiration ExpirationAfter { get; set; }
    }    

    public enum Expiration
    {
        Day = 1,
        Week = 2,
        Month = 3
    }
}