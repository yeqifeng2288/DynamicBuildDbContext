using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Demo.Foundation.DatabaseAccessor.Configures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Demo.Foundation.DatabaseAccessor
{
    internal static class DbContextModelBuidler
    {
        public static readonly ConcurrentBag<EntityBuildDescriber> EntityBuildDescribers = new();

        static DbContextModelBuidler()
        {
            FindAllEntityTypeBuilder();
        }

        private static void FindAllEntityTypeBuilder()
        {
            var dependencyContext = DependencyContext.Default;

            var scanAssemblies = dependencyContext.RuntimeLibraries
                .Where(a => a.Name.StartsWith(nameof(Demo)))
               .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
               .ToList();

            var types = scanAssemblies
                                  .SelectMany(assembly => assembly.GetExportedTypes())
                                  .Where(o => o != null && o.IsPublic && !o.IsInterface && !o.IsAbstract && typeof(IPrivateEntityTypeBuilder).IsAssignableFrom(o))
                                  .ToList();

            foreach (var instanceType in types)
            {
                var modelConfigureList = instanceType.GetInterfaces().Where(t => t.HasImplementedRawGeneric(typeof(IEntityTypeBuilder<>)));
                foreach (var originType in modelConfigureList)
                {
                    if (originType.GetGenericArguments().Length >= 1)
                    {
                        var targetType = originType.GetGenericArguments().First();
                        EntityBuildDescribers.Add(new EntityBuildDescriber(originType, instanceType, targetType));
                    }
                }
            }
        }

        /// <summary>
        /// 判断类型是否实现某个泛型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="generic">泛型类型</param>
        /// <returns>bool</returns>
        private static bool HasImplementedRawGeneric(this Type type, Type generic)
        {
            // 检查接口类型
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 检查类型
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            return false;

            // 判断逻辑
            bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
        }
    }
}
