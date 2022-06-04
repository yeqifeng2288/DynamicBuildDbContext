using Demo.Foundation.DatabaseAccessor.Configures;
using Demo.Module.StoreManage.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Module.StoreManage.Data
{
    public class StoreModelConfigure : IEntityTypeBuilder<Store>, IEntityTypeBuilder<Shelf>
    {
        public void ModelConfigure(EntityTypeBuilder<Store> entityBuilder)
        {
            entityBuilder.HasKey(o => o.Id);
            entityBuilder.Property(o => o.StoreName)
                .HasMaxLength(100);
        }

        public void ModelConfigure(EntityTypeBuilder<Shelf> entityBuilder)
        {
            entityBuilder.HasKey(o => o.Id);
        }
    }
}
