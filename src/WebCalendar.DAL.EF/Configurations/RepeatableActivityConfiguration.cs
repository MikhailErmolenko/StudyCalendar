using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalendar.DAL.Models;

namespace WebCalendar.DAL.EF.Configurations
{
    public abstract class RepeatableActivityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IRepeatableActivity
    {
        protected readonly ValueConverter _valueConverter =
            new ValueConverter<ICollection<int>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => int.Parse(val)).ToHashSet());

        protected readonly ValueComparer _valueComparer = new IntegerCollectionComparer();
        protected void ConvertFieds(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Years)
                .HasConversion(_valueConverter)
                .Metadata.SetValueComparer(_valueComparer);

            builder.Property(e => e.Monthes)
                .HasConversion(_valueConverter)
                .Metadata.SetValueComparer(_valueComparer);

            builder.Property(e => e.DaysOfMounth)
                .HasConversion(_valueConverter)
                .Metadata.SetValueComparer(_valueComparer);

            builder.Property(e => e.DaysOfWeek)
                .HasConversion(_valueConverter)
                .Metadata.SetValueComparer(_valueComparer);
        }
        public abstract void Configure(EntityTypeBuilder<T> builder);
      
    }
}
