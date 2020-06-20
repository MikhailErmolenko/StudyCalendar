using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class PushSubscriptionConfiguration : IEntityTypeConfiguration<PushSubscription>
    {
        public void Configure(EntityTypeBuilder<PushSubscription> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(p => p.User)
                .WithMany(u => u.PushSubscriptions)
                .HasForeignKey(p => p.UserId);
        }
    }
}