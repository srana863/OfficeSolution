using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.HRMS.Settings;
using Microsoft.Extensions.Configuration;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Implementations.HRMS.Settings
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<int> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateAsync(User user, string password)
        {
 
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            byte[] passwordHash, passwordSalt;
            
            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query_1 = CRUD<User>.Select(o => o.Username == o.Username);

                var res = await cnn.QueryFirstOrDefaultAsync<User>(query_1, new { Username = user.Username });

                if (res != null)
                    throw new AppException("Username \"" + user.Username + "\" is already taken");

                
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.SetDate = DateTime.Now;

                var query_2 = CRUD<User>.Insert();

                int count = await cnn.ExecuteAsync(query_2, user);

                return user;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query_1 = CRUD<User>.Select(o => o.UserId == o.UserId);
                var user = await cnn.QueryFirstOrDefaultAsync<User>(query_1, new { UserId = id });

                int rowsAffected = 0;
                if (user != null)
                {
                    var query = CRUD<User>.Delete(o => o.UserId == o.UserId);
                    rowsAffected = await cnn.ExecuteAsync(query, new User { UserId = id });
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query = CRUD<User>.Select();
                var users = await cnn.QueryAsync<User>(query);
                return users;
            }
        }

        public async Task<IEnumerable<User>> GetByFilterAsync(int page, int itemsPerPage, string search, string sortBy, bool reverse)
        {
            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var p = new DynamicParameters();
                p.Add("@page", page);
                p.Add("@search", search);
                p.Add("@sortBy", sortBy);
                p.Add("@itemsPerPage", itemsPerPage);
                p.Add("@sortOrder", reverse ? "DESC" : "ASC");
                string sql = "dbo.spUser_GetAll";
                var uses = await cnn.QueryAsync<User>(sql, p, commandType: CommandType.StoredProcedure);
                return uses;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query_1 = CRUD<User>.Select(o => o.UserId == o.UserId);
                var user = await cnn.QueryFirstOrDefaultAsync<User>(query_1, new { UserId = id });

                return user;
            }
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdateUserAsync(User userParam, string password = null)
        {

            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query_1 = CRUD<User>.Select(o => o.UserId == o.UserId);
                var user = await cnn.QueryFirstOrDefaultAsync<User>(query_1, new { UserId = userParam.UserId });

                if (user == null)
                {
                    throw new AppException("User not found");
                }

                if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
                {
                    var query_2 = CRUD<User>.Select(o => o.Username == o.Username);

                    var res = await cnn.QueryFirstOrDefaultAsync<User>(query_2, new { Username = user.Username });

                    if (res != null)
                        throw new AppException("Username " + userParam.Username + " is already taken");

                    user.Username = userParam.Username;
                }

                if (!string.IsNullOrWhiteSpace(userParam.UserFullName))
                    user.UserFullName = userParam.UserFullName;

                if (!string.IsNullOrWhiteSpace(userParam.Designation))
                    user.Designation = userParam.Designation;

                // update password if provided
                if (!string.IsNullOrWhiteSpace(password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }
                var query = CRUD<User>.Update(o => o.UserId == o.UserId);
                int rowsAffected = 0;
                rowsAffected = await cnn.ExecuteAsync(query,user);
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var query_1 = CRUD<User>.Select(o => o.Username == o.Username);

                var user = await cnn.QueryFirstOrDefaultAsync<User>(query_1, new { Username = username });

                if (user == null)
                    return null;

                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                return user;
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
