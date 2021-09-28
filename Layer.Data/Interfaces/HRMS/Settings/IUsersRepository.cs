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
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<UserInfoSession> GetUserByUserName(string username);
    }
    public interface IUserDetailsRepository : IGenericRepository<UserDetails>
    {
    }
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
       IEnumerable<DocumentType> GetAllWithParent(int orgId);
    }
    public interface IOrganizationProfileRepository : IGenericRepository<OrganizationProfile>
    {
    }
    public interface IOrgAuthoriseOrKeyPersonRepository : IGenericRepository<OrgAuthoriseOrKeyPerson>
    {
    }
    public interface IOrgDocumentRepository : IGenericRepository<OrgDocument>
    {
    }
    public interface IOrgTradingHoursRepository : IGenericRepository<OrgTradingHours>
    {
    }

    
}
