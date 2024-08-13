using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Persistence
{
    public class PasSecDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public PasSecDatabaseContext(DbContextOptions<PasSecDatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(PasSecDatabaseContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
