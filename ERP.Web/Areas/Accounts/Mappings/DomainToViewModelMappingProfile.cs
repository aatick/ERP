using Accounts.Data.AccountsDataModel;
using Common.Data.CommonDataModel;
using ERP.Web.Mappings;
using ERP.Web.ViewModels;

namespace Accounts.Mappings
{
    public class DomainToViewModelMappingProfile : AutomapperProfile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AccChart, AccChartViewModel>();
        }
    }
}