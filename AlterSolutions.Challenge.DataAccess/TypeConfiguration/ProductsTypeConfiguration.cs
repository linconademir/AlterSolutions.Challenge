using AlterSolutions.Challenge.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Challenge.DataAccess.TypeConfiguration
{
    public class ProductsTypeConfiguration : ChallengeAbstractConfig<Product>
    {
        protected override void ConfigForeignKEy()
        {
            HasRequired(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(fk => fk.IdCategory);
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigTableName()
        {
            ToTable("TB_PRODUCT");
        }

        protected override void ConfigTablesField()
        {
            Property(p => p.Id)
                 .IsRequired()
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                 .HasColumnName("ID_IN_PRODUCT");

            Property(p => p.Description)
              .IsRequired()
              .HasColumnName("DESCRIPTION_VC_PRODUCT")
              .HasMaxLength(300)
              .HasColumnType("varchar");

            Property(p => p.Code)
             .HasColumnName("CODE_VC_PRODUCT")
             .HasMaxLength(160)
             .HasColumnType("varchar");

            Property(p => p.Active)
             .HasColumnName("ACTIVE_SI_PRODUCT");

            Property(p => p.IdCategory)
             .HasColumnName("ID_IN_CATEGORY");

            Property(p => p.InventoryBalance)
             .HasColumnName("INVENTORY_BALANCE_IN_PRODUCT");

            Property(p => p.Price)
             .HasColumnName("PRICE_VC_PRODUCT");

            Property(p => p.Reference)
             .HasColumnName("REFERENCE_VC_PRODUCT")
             .HasMaxLength(160)
             .HasColumnType("varchar");

            Property(p => p.Dimensions)
             .HasColumnName("DIMENSIONS_VC_PRODUCT")
             .HasMaxLength(160)
             .HasColumnType("varchar");

        }
    }
}
