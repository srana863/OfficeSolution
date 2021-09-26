using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Settings
{
    public class DocumentTypeRepository : DataCommon, IDocumentTypeRepository
    {
        public DocumentTypeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(DocumentType entity)
        {
            var query = CRUD<DocumentType>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<DocumentType>.Delete(o => o.DocTypeId == o.DocTypeId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { DocTypeId = id, OrgId = orgId }).Single();
        }

        public DocumentType Get(int id, int orgId)
        {
            var query = CRUD<DocumentType>.Select(o => o.DocTypeId == o.DocTypeId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<DocumentType>(query, new { DocTypeId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<DocumentType> GetAll(int orgId)
        {
            var query = CRUD<DocumentType>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<DocumentType>(query, new { OrgId = orgId });
        }

        public int Update(DocumentType entity)
        {
            var query = CRUD<DocumentType>.Update(o => o.DocTypeId == o.DocTypeId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}