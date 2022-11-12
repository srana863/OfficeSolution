using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.Common
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id,int InstituteId);
        IEnumerable<T> GetAll(int InstituteId);
        int Create(T entity);
        int Update(T entity);
        int Delete(int id, int InstituteId);
    }
}
