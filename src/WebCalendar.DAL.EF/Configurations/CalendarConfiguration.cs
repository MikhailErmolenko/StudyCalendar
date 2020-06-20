using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebCalendar.DAL.EF.Configurations
{
    public class CalendarConfiguration : IEntityTypeConfiguration<Models.Entities.Calendar>
    {
        public void Configure(EntityTypeBuilder<Models.Entities.Calendar> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(c => c.CalendarUsers)
                .WithOne(cu => cu.Calendar)
                .HasForeignKey(cu => cu.CalendarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Calendars)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.Events)
                .WithOne(e => e.Calendar)
                .HasForeignKey(e => e.CalendarId);

            builder.HasMany(c => c.Reminders)
                .WithOne(r => r.Calendar)
                .HasForeignKey(r => r.CalendarId);

            builder.HasMany(c => c.Tasks)
                .WithOne(t => t.Calendar)
                .HasForeignKey(t => t.CalendarId);
        }
    }
}