using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebCalendar.DAL.Models
{
    public class IntegerCollectionComparer : ValueComparer<ICollection<int>>
    {
        public IntegerCollectionComparer() : base(true)
        {

        }
    }
}
