using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrms.Data.HRMSDataModel;

namespace Hrms.Data.HRMSRepository
{
    public interface IEMPDocumentUploadRepository : IRepository<EMP_Document_Upload>
    {

    }
    public class EMPDocumentUploadRepository : RepositoryBaseCodeFirst<EMP_Document_Upload, HRMSDbContext>, IEMPDocumentUploadRepository
    {
        public EMPDocumentUploadRepository(IDatabaseFactoryCodeFirst<HRMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
