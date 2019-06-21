using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanMgntAPI.ViewModels;
using Utility.Models;

namespace LoanMgntAPI.Helper
{
    public class MyMappingProfiles : Profile
    {
        public MyMappingProfiles()
        {
            CreateMap<RegistrationVM, LoginCredentials>();
            CreateMap<CustomerViewModel, LoginCredentials>().ReverseMap();
            CreateMap<LoggedInUserVM, LoginCredentials>().ReverseMap();
            CreateMap<AgentFund, FundVM>().
                ForMember(dest => dest.AgnetName, source => source.MapFrom(src => src.Agent.Firstname + " " + src.Agent.Middlename + " " + src.Agent.Lastname)).
                ForMember(dest => dest.DepositBy, source => source.MapFrom(src => src.CreatedByNavigation.Firstname + " " + src.CreatedByNavigation.Middlename + " " + src.CreatedByNavigation.Lastname));

            CreateMap<LoanTypeMaster,LoanTypeVM>();

            CreateMap<RoleRight, MenuLinkViewModel>().ReverseMap();
            CreateMap<LoadDetailVM, CustomerLoan> ().ReverseMap();
            CreateMap<TenureVM, CustomerLoanTxn>().ReverseMap();
            CreateMap<CustomerLoan,CustomerLoanVM > ().ForMember(dest => dest.CustomerName, source => source.MapFrom(src => src.Customer.Firstname + " " + src.Customer.Middlename + " " + src.Customer.Lastname)).
                ForMember(dest => dest.CreatedByName, source => source.MapFrom(src => src.CreatedByNavigation.Firstname + " " + src.CreatedByNavigation.Middlename + " " + src.CreatedByNavigation.Lastname));

          //CreateMap<InstalmentTxnVM,CustomerLoanTxn>().ReverseMap();
          CreateMap<CustomerLoanTxn,InstalmentTxnVM>().ForMember(dest => dest.CustomerName, source => source.MapFrom(src => src.CustomerLoan.Customer.Firstname + " " + src.CustomerLoan.Customer.Middlename + " " + src.CustomerLoan.Customer.Lastname));
      //ForMember(dest => dest.DepositBy, source => source.MapFrom(src => src.Agent.Firstname + " " + src.Agent.Middlename + " " + src.Agent.Lastname));
      //CreateMap<List<LoginCredentials>, List<CustomerViewModel>>();
    }

    }
}
