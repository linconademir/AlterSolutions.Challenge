using AlterSolutions.Challenge.DataAccess.Context;
using AlterSolutions.Challenge.Domain;
using AlterSolutions.Comum.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Challenge.Repository.Entity
{
    public class RepositoryProduct : RepositoryAlterSolutions<Product, int>
    {
        public RepositoryProduct(ChallengeDbContext context) 
            : base(context)
        {
            
        }

        
    }
}
