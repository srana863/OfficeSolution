using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Common
{
    public interface ICommonDataMethods<TEntity>
    {
        int Create(TEntity model);
        int Update(TEntity model);
        int Delete(int id, string identityCode = null);
        int Delete(string id, string identityCode = null);
        IEnumerable<TEntity> GetAll(string identityCode = null);
        TEntity Get(int id, string identityCode = null);
        TEntity Get(string id, string identityCode = null);
    }
}
