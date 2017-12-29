using ERP.Web.Mappings;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;

namespace Upms.Mappings
{
    public class DomainToViewModelMappingProfile : AutomapperProfile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<CompanyDepository, CompanyDepositoryViewModel>();
            CreateMap<CompanyInformation, CompanyInfoViewModel>();
            CreateMap<BrokerDepositoryPariticipant, BrokerDepositoryViewModel>();
            //  Mapper.CreateMap<BrokerEmployee, BrokerEmployeeViewModel>();
            //  Mapper.CreateMap<BrokerBranch, BrokerBranchViewModel>();
            CreateMap<BrokerTrader, BrokerTraderViewModel>();
            CreateMap<BrokerDPBranch, BrokerDPBranchViewModel>();
            CreateMap<InvestorDetail, InvestorDetailViewModel>();
            CreateMap<InvestorNominee, InvestorDetailViewModel>();
            CreateMap<InvestorNomineeGuardian, InvestorDetailViewModel>();
            // Mapper.CreateMap<UserLogin, UserLoginViewModel>();
            CreateMap<InvestorPowerOfAttorney, PowerofAttorneyViewModel>();
        }
    }
}