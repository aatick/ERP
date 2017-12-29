using ERP.Web.Mappings;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;

namespace Upms.Mappings
{
    public class ViewModelToDomainMappingProfile : AutomapperProfile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Mapper.CreateMap<LookupRelationViewModel, LookupRelation>();
            CreateMap<CompanyDepositoryViewModel, CompanyDepository>();
            CreateMap<CompanyInfoViewModel, CompanyInformation>();
            CreateMap<BrokerDepositoryViewModel, BrokerDepositoryPariticipant>();
            // Mapper.CreateMap<BrokerEmployeeViewModel, BrokerEmployee>();
            // Mapper.CreateMap<BrokerBranchViewModel, BrokerBranch>();
            CreateMap<BrokerTraderViewModel, BrokerTrader>();
            CreateMap<BrokerDPBranchViewModel, BrokerDPBranch>();
            CreateMap<InvestorDetailViewModel, InvestorDetail>();
            CreateMap<InvestorDetailViewModel, InvestorNominee>();
            CreateMap<InvestorDetailViewModel, InvestorNomineeGuardian>();

            CreateMap<PowerofAttorneyViewModel, InvestorPowerOfAttorney>();
        }
    }
}