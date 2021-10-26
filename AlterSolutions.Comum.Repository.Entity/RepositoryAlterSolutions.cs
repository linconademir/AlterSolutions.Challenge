using AlterSolutions.Comum.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlterSolutions.Comum.Repository.Entity
{
    public abstract class RepositoryAlterSolutions<TDomain, TKey> : IRepositoryAlterSolutions<TDomain, TKey>
        where TDomain : class
    {
        protected DbContext _context;

        public RepositoryAlterSolutions(DbContext context)
        {
            _context = context;
        }
        public void Delete(TDomain domain)
        {
            _context.Entry(domain).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteById(TKey id)
        {
            TDomain domain = GetById(id);
            Delete(domain);
        }

        public List<TDomain> Get(Expression<Func<TDomain, bool>> where = null)
        {
            DbSet<TDomain> dbset = _context.Set<TDomain>();
            if (where == null)
            {
                return dbset.ToList();
            }
            else
            {
                return dbset.Where(where).ToList();
            }
        }

        public TDomain GetById(TKey id)
        {
            return _context.Set<TDomain>().Find(id);
        }

        public void Save(TDomain domain)
        {
            _context.Set<TDomain>().Add(domain);
            _context.SaveChanges();
        }

        public void Update(TDomain domain)
        {
            _context.Entry(domain).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<TDomain> Values
        {
            get
            {
                return Source.Values;
            }
        }

        public Dictionary<string, TDomain> Source { get; private set; } = new Dictionary<string, TDomain>();

        public void SetSource(Dictionary<string, TDomain> source)
        {
            Source = source;
        }
    }
}
