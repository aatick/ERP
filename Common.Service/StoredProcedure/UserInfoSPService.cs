using BasicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataAccess.Data;

namespace Common.Service.StoredProcedure
{
    public interface IUserInfoSPService
    {
        DataSet GetAspNetUserList();       
    }
    public class UserInfoSPService : IUserInfoSPService
    {
        public DataSet GetAspNetUserList()
        {
            var storeProcedureName = "SP_GetAspNetUserList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
            }
        }

    }
}
