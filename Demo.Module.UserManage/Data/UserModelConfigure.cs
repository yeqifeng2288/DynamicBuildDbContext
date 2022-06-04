using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Foundation.DatabaseAccessor.Configures;
using Demo.Module.UserManage.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Module.UserManage.Data
{
    public class UserModelConfigure : IEntityTypeBuilder<User>
    {
        public void ModelConfigure(EntityTypeBuilder<User> entityBuilder)
        {
        }
    }
}
