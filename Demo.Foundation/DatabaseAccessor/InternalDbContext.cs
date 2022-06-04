using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Foundation.DatabaseAccessor
{
    internal static class InternalDbContext
    {
        public static Type? MainDbContextType { get; set; }
    }
}
