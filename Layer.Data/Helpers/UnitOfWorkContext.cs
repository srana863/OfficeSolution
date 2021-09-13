using Layer.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Helpers
{
    public class UnitOfWorkContext
    {
        public DbContext _dbContext;
        public UnitOfWorkContext()
        {
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
        }
    }

}
