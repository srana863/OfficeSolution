using Layer.Model.Common;

namespace Layer.Business.Common
{
    public class BllCommon
    {
        public DbContext _dbContext;
        public ReturnMessage _vmReturn;
        public BllCommon()
        {
            _vmReturn = new ReturnMessage();
            _dbContext = new DbContext(AppSetting.ConnectionString, "System.Data.SqlClient");
        }
    }
}
