using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.HRMS.Settings
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetByFilterAsync(int page, int itemsPerPage, string search, string sortBy, bool reverse);
        Task<Users> CreateAsync(Users user, string password);
        Task<Users> AuthenticateAsync(string username, string password);
        Task<bool> UpdateUserAsync(Users userParam, string password = null);
        Task<Users> GetByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<int> AddAsync(Users entity);
        Task<int> UpdateAsync(Users entity);
        Task<bool> DeleteAsync(int id);

    }

    public interface IUsersRepository : IGenericRepository<Users>
    {
        UserInfoSession GetUserByUserName(string username);
    }
}
