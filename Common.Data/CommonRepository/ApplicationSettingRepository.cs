using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.DBDetailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IApplicationSettingRepository : IRepository<ApplicationSetting>
    {
        IEnumerable<DBApplicationSettingsDetail> GetApplicationDetailDetail(int? OrgID,int? officeID);
    }
    public class ApplicationSettingRepository : RepositoryBaseCodeFirst<ApplicationSetting, CommonDbContext>, IApplicationSettingRepository
    {
        public ApplicationSettingRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }

        public IEnumerable<DBApplicationSettingsDetail> GetApplicationDetailDetail(int? OrgID, int? officeID)
        {
            var obj = DataContext.ApplicationSettings.Where(x => x.OfficeId == officeID && x.OrgId == OrgID)
             .Select(s => new DBApplicationSettingsDetail()
             {
                
                 OfficeID = s.OfficeId,
                // OfficeCode = s.Office == null ? "" : s.Office.OfficeCode,
              
                 //ApplicationSettingsID = s.ApplicationSettingsID,
                 BankAccount = s.BankAccount,
                 CashBook = s.CashBook,
                 CellNo = s.CellNo,
                 Email = s.Email,
                 LicenseEndDate = s.LicenseEndDate,
                 LicenseNo = s.LicenseNo,
                 LicenseStartDate = s.LicenseStartDate,
                 MonthClosingDate = s.MonthClosingDate,
                 OperationStartDate = s.OperationStartDate,
                 OrganizationAddress = s.OrganizationAddress,
                 //OrganizationID = s.OrganizationID,
                 //OrganizationName = s.OrganizationName,
                 //PhoneNo = s.PhoneNo,
                 //PLAccount = s.PLAccount,
                 //ProcessType = s.ProcessType,
                 TransactionDate = s.TransactionDate,
                 YearClosingDate = s.YearClosingDate

             });

            return obj;
        }
    }
}
