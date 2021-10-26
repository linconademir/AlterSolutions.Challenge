using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Comum.Repository.Interface
{
    public interface IRepositoryAlterSolutions<TDomain, TKey> 
        where TDomain : class
    {
        List<TDomain> Get(Expression<Func<TDomain, bool>> where = null);
        TDomain GetById(TKey id);
        void Save(TDomain domain);
        void Update(TDomain domain);
        void Delete(TDomain domain);
        void DeleteById(TKey id);
        void SetSource(Dictionary<string, TDomain> source);
        IEnumerable<TDomain> Values { get; }
        Dictionary<string, TDomain> Source { get; }
    }
}
