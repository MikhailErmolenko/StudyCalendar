using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace WebCalendar.Common
{
    public class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            IEnumerable<Assembly> assemblies = domain.GetAssemblies().Where(a => a.FullName.StartsWith("WebCalendar"));
            IEnumerable<Type> profiles = assemblies.SelectMany(s => s.GetTypes())
                .Where(a => typeof(AutoMapperProfile).IsAssignableFrom(a));

            var mapperConfiguration = new MapperConfiguration(a => profiles.ForEach(a.AddProfile));

            return mapperConfiguration.CreateMapper();
        }
    }

    public class AutoMapperProfile : Profile
    {

    }

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable,
            Action<T> action)
        {
            foreach (T item in enumerable) { action(item); }
        }
    }
}