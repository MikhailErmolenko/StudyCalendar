using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class CalendarUserConfiguration : IEntityTypeConfiguration<CalendarUser>
    {
        public void Configure(EntityTypeBuilder<CalendarUser> builder)
        {
            builder.HasKey(cu => new
            {
                cu.CalendarId,
                cu.UserId
            });

            builder.HasOne(cu => cu.Calendar)
                .WithMany(c => c.CalendarUsers)
                .HasForeignKey(cu => cu.CalendarId);

            builder.HasOne(cu => cu.User)
                .WithMany(c => c.CalendarUsers)
                .HasForeignKey(cu => cu.UserId);
        }
    }
}