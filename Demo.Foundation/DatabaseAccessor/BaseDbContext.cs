using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Foundation.DatabaseAccessor.Configures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Foundation.DatabaseAccessor
{
    public abstract class BaseDbContext<TDbContext> : DbContext
        where TDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<TDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modelBuilderMethod = typeof(ModelBuilder)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 0);
            var entityBuildDescribers = DbContextModelBuidler.EntityBuildDescribers;
            foreach (var entityBuildDescriber in entityBuildDescribers)
            {
                var targetType = entityBuildDescriber.TargetType;
                var instanceType = entityBuildDescriber.InstanceType;
                var entityTypeBuilder = modelBuilderMethod!.MakeGenericMethod(targetType).Invoke(modelBuilder, null) as EntityTypeBuilder;
                if (targetType == null || entityTypeBuilder == null)
                    continue;
                var instance = Activator.CreateInstance(instanceType);

                // 构造参数类型，用于同类型多种接口实现。
                var parameterType = typeof(EntityTypeBuilder<>).MakeGenericType(targetType);
                var method = instanceType.GetMethod(nameof(IEntityTypeBuilder<Object>.ModelConfigure), new[] { parameterType });
                method!.Invoke(instance, new object[] { entityTypeBuilder });
            }

            InternalDbContext.MainDbContextType = this.GetType();
        }
    }
}
