using System.Linq;
using System.Reflection;
using Trimfit.Domain;

namespace Trimfit.Utils.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static IQueryable<T> SetQuery<T>(this DatabaseContext context)
        {
            MethodInfo method = typeof(DatabaseContext).GetMethod(nameof(DatabaseContext.Set), BindingFlags.Public | BindingFlags.Instance);

            method = method.MakeGenericMethod(typeof(T));

            return method.Invoke(context, null) as IQueryable<T>;
        }
    }
}
