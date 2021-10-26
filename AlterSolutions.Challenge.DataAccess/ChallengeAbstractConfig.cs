using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Challenge.DataAccess
{
    public abstract class ChallengeAbstractConfig<TEntidade> : EntityTypeConfiguration<TEntidade>
        where TEntidade : class
    {
        public ChallengeAbstractConfig()
        {
            ConfigTableName();
            ConfigTablesField();
            ConfigPrimaryKey();
            ConfigForeignKEy();
        }


        protected abstract void ConfigTableName();
        protected abstract void ConfigTablesField();
        protected abstract void ConfigPrimaryKey();
        protected abstract void ConfigForeignKEy();


    }
}
