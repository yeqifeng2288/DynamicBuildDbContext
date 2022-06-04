using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Foundation.DatabaseAccessor.Configures
{
    /// <summary>
    /// 实体构建描述。
    /// </summary>
    /// <param name="OriginType">IEntityTypeBuilder<>接口类型。</param>
    /// <param name="InstanceType">实现方式类型。</param>
    /// <param name="TargetType">实体的具体类型。</param>
    internal record EntityBuildDescriber(Type OriginType, Type InstanceType, Type TargetType)
    {
    }
}
