//using UcasPortfolio.Data.DBDetailModels;
using AutoMapper;
using Common.Data.CommonDataModel;
using ERP.Web.ViewModels;

namespace ERP.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : AutomapperProfile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LookupRelationViewModel, LookupRelation>();
            // Mapper.CreateMap<CompanyDepositoryViewModel, CompanyDepository>();
            // Mapper.CreateMap<CompanyInfoViewModel, CompanyInformation>();
            // Mapper.CreateMap<BrokerDepositoryViewModel, BrokerDepositoryPariticipant>();
            // Mapper.CreateMap<BrokerInfoViewModel, BrokerInformation>();
            CreateMap<BrokerEmployeeViewModel, BrokerEmployee>();
            CreateMap<BrokerBranchViewModel, BrokerBranch>();
            //   Mapper.CreateMap<BrokerTraderViewModel, BrokerTrader>();
            //   Mapper.CreateMap<BrokerDPBranchViewModel, BrokerDPBranch>();
            //  Mapper.CreateMap<InvestorDetailViewModel, InvestorDetail>();
            //  Mapper.CreateMap<InvestorDetailViewModel, InvestorNominee>();
            //  Mapper.CreateMap<InvestorDetailViewModel, InvestorNomineeGuardian>();
            CreateMap<AspNetRoleViewModel, AspNetRole>();
            CreateMap<UserLoginViewModel, UserLogin>();
            CreateMap<BrokerInfoViewModel, BrokerInformation>();
            // Mapper.CreateMap<AccChartViewModel,AccChart >();

            // Mapper.CreateMap<PowerofAttorneyViewModel, InvestorPowerOfAttorney>();
        }
    }
}