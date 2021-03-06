using AutoMapper;
using Humanizer;
using MatchHut.Core;
using MatchHut.Core.Dtos;
using MatchHut.Core.Dtos.Company;
using MatchHut.Core.Dtos.Configuration;
using MatchHut.Core.Dtos.Role;
using MatchHut.Core.Dtos.RoleClaim;
using MatchHut.Core.Dtos.UserRole;
using MatchHut.Core.Models;
using MatchHut.Dtos.User;
using System;
using System.Linq;

namespace MatchHut.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityBase, EntityBase>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore());

            CreateMap<Configuration, ConfigurationDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));
            CreateMap<CreateConfigurationDto, Configuration>();
            CreateMap<UpdateConfigurationDto, Configuration>();

            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RoleId));
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.UserId));
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.UserRoleId))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.RoleName, opts => opts.MapFrom(src => src.Role.RoleName));
            CreateMap<CreateUserRoleDto, UserRole>();
            CreateMap<UpdateUserRoleDto, UserRole>();

            CreateMap<RoleClaim, RoleClaimDto>()
                .ForMember(dest => dest.RoleName, opts => opts.MapFrom(src => src.Role.RoleName));
            CreateMap<CreateRoleClaimDto, RoleClaim>();
            CreateMap<UpdateRoleClaimDto, RoleClaim>();
            CreateMap<string, ClaimDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src));

            CreateMap<Company, CompanyDto>();
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>();
        }

        public class NullableTimezoneResolver: IMemberValueResolver<object, object, DateTime?, DateTime?>
        {
            private readonly UserCultureInfo _userCultureInfo;
            public NullableTimezoneResolver(UserCultureInfo userCultureInfo)
            {
                _userCultureInfo = userCultureInfo;
            }

            public DateTime? Resolve(object source, object destination, DateTime? sourceMember, DateTime? destMember, ResolutionContext context)
            {
                return _userCultureInfo.GetUserLocalTime(sourceMember);
            }
        }

        public class TimezoneResolver : IMemberValueResolver<object, object, DateTime, DateTime>
        {
            private readonly UserCultureInfo _userCultureInfo;
            public TimezoneResolver(UserCultureInfo userCultureInfo)
            {
                _userCultureInfo = userCultureInfo;
            }

            public DateTime Resolve(object source, object destination, DateTime sourceMember, DateTime destMember, ResolutionContext context)
            {
                return _userCultureInfo.GetUserLocalTime(sourceMember).GetValueOrDefault();
            }
        }

        private class CustomResolver : IMemberValueResolver<object, object, string, int>
        {
            public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
            {
                int.TryParse(sourceMember.Trim(), out int quantity);
                return quantity;
            }
        }

        //private static object Converter(Transaction value)
        //{
        //    return Enum.GetName(typeof(TransactionType), value.TransactionType.DehumanizeTo<TransactionType>()).Humanize();
        //}
    }
}