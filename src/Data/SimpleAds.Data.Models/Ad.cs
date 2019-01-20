using SimpleAds.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleAds.Data.Models
{
    public class Ad
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public SimpleAdsUser Author { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ExpirationOn { get; set; }

        public Expiration ExpirationAfter { get; set; }

        public Status Status { get; set; }
    }
}
