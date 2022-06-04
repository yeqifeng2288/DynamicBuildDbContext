using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Foundation.DatabaseAccessor.Configures
{
    public interface IEntityTypeBuilder<TEntity> : IPrivateEntityTypeBuilder
        where TEntity : class
    {
        void ModelConfigure(EntityTypeBuilder<TEntity> entityBuilder);
    }

    /// <summary>
    /// 用于识别实体构建类的接口。
    /// </summary>
    public interface IPrivateEntityTypeBuilder { }
}
