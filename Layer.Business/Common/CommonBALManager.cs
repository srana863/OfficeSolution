using Layer.Data.Interfaces;
using Layer.Data.HRMS.Settings;
using Layer.Business.Interfaces;

namespace Layer.Business.Common
{
    public class CommonBALManager : BllCommon, ICommonBALManager
    {
        private readonly IUsersRepository _usersRepository;

        public CommonBALManager()
        {
            _usersRepository = new UsersRepository(_dbContext);
        }

        //public IEnumerable<ContactViewModel> GetContact(string identityCode, DateTime utcNow)
        //{
        //    try
        //    {
        //        _dbContext.Open();
        //        var data = _nightShiftingRepository.GetContact(identityCode, utcNow);
        //        return data;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        _dbContext.Close();
        //    }
        //}


    }
}
