//using UcasPortfolio.Data.DBDetailModels;
using AutoMapper;
using Common.Data.CommonDataModel;
using ERP.Web.ViewModels;

namespace ERP.Web.Mappings
{
    public class DomainToViewModelMappingProfile : AutomapperProfile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<LookupRelation, LookupRelationViewModel>();
            //Mapper.CreateMap<CompanyDepository, CompanyDepositoryViewModel>();
            //Mapper.CreateMap<CompanyInformation, CompanyInfoViewModel>();
            //Mapper.CreateMap<BrokerDepositoryPariticipant, BrokerDepositoryViewModel>();
            //Mapper.CreateMap<BrokerInformation, BrokerInfoViewModel>();
            CreateMap<BrokerEmployee, BrokerEmployeeViewModel>();
            CreateMap<BrokerBranch, BrokerBranchViewModel>();
            //Mapper.CreateMap<BrokerTrader, BrokerTraderViewModel>();
            //Mapper.CreateMap<BrokerDPBranch, BrokerDPBranchViewModel>();
            //Mapper.CreateMap<InvestorDetail, InvestorDetailViewModel>();
            //Mapper.CreateMap<InvestorNominee, InvestorDetailViewModel>();
            //Mapper.CreateMap<InvestorNomineeGuardian, InvestorDetailViewModel>();


            CreateMap<AspNetRole, AspNetRoleViewModel>();
            //Mapper.CreateMap<AccChart, AccChartViewModel>();

            CreateMap<UserLogin, UserLoginViewModel>();
            CreateMap<BrokerInformation, BrokerInfoViewModel>();
            //Mapper.CreateMap<InvestorPowerOfAttorney, PowerofAttorneyViewModel>();
        }
        //public override string ProfileName
        //{
        //    get { return "DomainToViewModelMappings"; }
        //}

        //protected override void Configure()
        //{


           

        //}
    }
}