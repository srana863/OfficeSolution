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
        Task<IEnumerable<User>> GetByFilterAsync(int page, int itemsPerPage, string search, string sortBy, bool reverse);
        Task<User> CreateAsync(User user, string password);
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> UpdateUserAsync(User userParam, string password = null);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<int> AddAsync(User entity);
        Task<int> UpdateAsync(User entity);
        Task<bool> DeleteAsync(int id);

    }
}
