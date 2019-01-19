using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleAds.Data.Models;
using System;

namespace SimpleAds.Data
{
    public class SimpleAdsDbContext : IdentityDbContext<SimpleAdsUser>
    {
        public SimpleAdsDbContext(DbContextOptions<SimpleAdsDbContext> options)
            : base(options)
        {
        }

        public DbSet<PendingAd> PendingAds { get; set; }

        public DbSet<Ad> Ads { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
