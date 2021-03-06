﻿using GlobalConstans;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleAds.Services.ViewModels.Ads
{
    public class AdEditModel
    {
        [Required]
        public int Id { get; set; }

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

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Expiration After")]
        public Expiration ExpirationAfter { get; set; }
    }

}
