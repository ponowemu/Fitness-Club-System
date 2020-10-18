using System.Linq;
using System.Reflection;
using Trimfit.Domain;

namespace Trimfit.Utils.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static IQueryable<T> SetQuery<T>(this DatabaseContext context)
        {
            return typeof(DatabaseContext)
                .GetMethod(nameof(DatabaseContext.Set), BindingFlags.Public | BindingFlags.Instance)
                ?.MakeGenericMethod(typeof(T)).Invoke(context, null) as IQueryable<T>;
        }
    }
}
