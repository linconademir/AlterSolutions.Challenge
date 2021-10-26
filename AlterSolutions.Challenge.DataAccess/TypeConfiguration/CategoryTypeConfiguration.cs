using AlterSolutions.Challenge.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Challenge.DataAccess.TypeConfiguration
{
    public class CategoryTypeConfiguration : ChallengeAbstractConfig<Category>
    {
        protected override void ConfigForeignKEy()
        {
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigTableName()
        {
            ToTable("TB_CATEGORY");
        }

        protected override void ConfigTablesField()
        {
            Property(p => p.Id)
                    .IsRequired()
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                    .HasColumnName("ID_IN_CATEGORY");

            Property(p => p.Description)
              .IsRequired()
              .HasColumnName("DESCRIPTION_VC_CATEGORY")
              .HasMaxLength(300)
              .HasColumnType("varchar");

            Property(p => p.Active)
              .IsRequired()
              .HasColumnName("ACTIVE_SI_CATEGORY");

        }
    }
}
