using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleAds.Data.Models
{
    public abstract class BaseAd : BaseModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        [Required]
        public string AdCreatorId { get; set; }

        public SimpleAdsUser AdCreator { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
