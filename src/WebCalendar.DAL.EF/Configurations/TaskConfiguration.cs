using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(t => t.Calendar)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CalendarId);
        }
    }
}