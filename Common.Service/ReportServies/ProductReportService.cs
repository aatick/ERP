using BasicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataAccess.Data;

namespace Common.Service.ReportServies
{
    public interface IProductReportService
    {
        DataSet GetDataProductInfo<TParamOType>(TParamOType target) where TParamOType : class;
    }

    public class ProductReportService : IProductReportService
    {
        public DataSet GetDataProductInfo<TParamOType>(TParamOType target) where TParamOType : class        
        {
            var storeProcedureName = "GetProductInfo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }


}
