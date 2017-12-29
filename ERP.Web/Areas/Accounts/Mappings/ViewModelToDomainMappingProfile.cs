using Accounts.Data.AccountsDataModel;
using Common.Data.CommonDataModel;
using ERP.Web.Mappings;
using ERP.Web.ViewModels;

namespace Accounts.Mappings
{
    public class ViewModelToDomainMappingProfile : AutomapperProfile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AccChartViewModel, AccChart>();
        }
    }
}