using Common.Data;
using Common.Data.CommonDataModel;
//using UcasPortfolio.Data.CodeFirstMigration.Db;
using Common.Data.Infrastructure;
using Common.Data.DBDetailModels;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IApplicationSettingsService : IServiceBase<ApplicationSetting>
    {
        IEnumerable<ValidationResult> IsValidSettings(ApplicationSetting applicationsetting);
        IEnumerable<ValidationResult> IsValidEdit(ApplicationSetting applicationsetting);
        IEnumerable<DBApplicationSettingsDetail> GetApplicationDetailDetail(int? OrgID, int? officeID);
    }
    public class ApplicationSettingsService: IApplicationSettingsService
    {
        private readonly IApplicationSettingRepository repository;
        public ApplicationSettingsService(IApplicationSettingRepository repository)
        {
            this.repository = repository;
           
        }
      
        public IEnumerable<ApplicationSetting> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.OfficeId);
            return entities;
        }

        public ApplicationSetting GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public ApplicationSetting Create(ApplicationSetting objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(ApplicationSetting objectToUpdate)
        {
            repository.Update(objectToUpdate);
            Save();
        }

        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }

        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }

        public bool IsContinued(long id)
        {
            var obj = repository.GetById(id);
            if (obj != null)
            {
                var isActive = obj.IsActive;
                if (isActive == false)
                {
                    return false;
                }
            }

            return true;
        }

        public void Save()
        {
            repository.Commit();
        }

        public IEnumerable<ValidationResult> IsValidSettings(ApplicationSetting applicationsetting)
        {
            var entity = repository.Get(a => a.OfficeId == applicationsetting.OfficeId);

            if (entity != null)
            {
                yield return new ValidationResult("OfficeID", "Duplicate Record");
            }
        }


        public IEnumerable<ValidationResult> IsValidEdit(ApplicationSetting applicationsetting)
        {
            var entity = repository.Get(a => a.OfficeId == applicationsetting.OfficeId);

            if (entity == null)
            {
                yield return new ValidationResult("OfficeID", "Duplicate Record");
            }
        }


        public IEnumerable<DBApplicationSettingsDetail> GetApplicationDetailDetail(int? OrgID, int? officeID)
        {
            return repository.GetApplicationDetailDetail(OrgID,officeID);
        }


        public ApplicationSetting GetByIdLong(long id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
    }
}
