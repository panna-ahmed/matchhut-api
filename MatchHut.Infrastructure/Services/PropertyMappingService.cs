using MatchHut.Core.Dtos;
using MatchHut.Core.Dtos.Configuration;
using MatchHut.Core.Dtos.Role;
using MatchHut.Core.Dtos.RoleClaim;
using MatchHut.Core.Dtos.UserRole;
using MatchHut.Core.Models;
using MatchHut.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchHut.Infrastructure.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _configurationPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "Id" }) },
                { "configCode", new PropertyMappingValue(new List<string>() { "ConfigCode" }) },
                { "configValue", new PropertyMappingValue(new List<string>() { "ConfigValue" }) },
                { "description", new PropertyMappingValue(new List<string>() { "Description" }) },
                { "status", new PropertyMappingValue(new List<string>() { "Status" }) },
                { "createdDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }) }
            };        

        private readonly Dictionary<string, PropertyMappingValue> _roleInfoPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "RoleId" }) },
                { "roleName", new PropertyMappingValue(new List<string>() { "RoleName" }) },
                { "description", new PropertyMappingValue(new List<string>() { "Description" }) },
                { "status", new PropertyMappingValue(new List<string>() { "Status" }) },
                { "createdDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }) }
            };

        private readonly Dictionary<string, PropertyMappingValue> _userInfoPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
                { "id", new PropertyMappingValue(new List<string>() { "UserId" }) },
                { "username", new PropertyMappingValue(new List<string>() { "Username" }) },
                { "password", new PropertyMappingValue(new List<string>() { "Password" }) },
                { "displayName", new PropertyMappingValue(new List<string>() { "DisplayName" }) },
                { "fullName", new PropertyMappingValue(new List<string>() { "FullName" }) },
                { "designation", new PropertyMappingValue(new List<string>() { "Designation" }) },
                { "contactNo", new PropertyMappingValue(new List<string>() { "ContactNo" }) },
                { "email", new PropertyMappingValue(new List<string>() { "Email" }) },
                { "address", new PropertyMappingValue(new List<string>() { "Address" }) },
                { "status", new PropertyMappingValue(new List<string>() { "Status" }) },
                { "createdDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }) }
           };

        private readonly Dictionary<string, PropertyMappingValue> _userRolePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "UserRoleId" }) },
                { "userName", new PropertyMappingValue(new List<string>() { "User.UserName" }) },
                { "roleName", new PropertyMappingValue(new List<string>() { "Role.RoleName" }) },
                { "status", new PropertyMappingValue(new List<string>() { "Status" }) },
                { "createdDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }) }
            };

        private readonly Dictionary<string, PropertyMappingValue> _userRoleClaimPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "Id" }) },
                { "roleName", new PropertyMappingValue(new List<string>() { "Role.RoleName" }) },
                { "claimType", new PropertyMappingValue(new List<string>() { "ClaimType" }) },
                { "claimValue", new PropertyMappingValue(new List<string>() { "ClaimValue" }) },
                { "status", new PropertyMappingValue(new List<string>() { "Status" }) },
                { "createdDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }) }
            };

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<ConfigurationDto, Configuration>(_configurationPropertyMapping));
            propertyMappings.Add(new PropertyMapping<RoleDto, Role>(_roleInfoPropertyMapping));
            propertyMappings.Add(new PropertyMapping<UserDto, User>(_userInfoPropertyMapping));
            propertyMappings.Add(new PropertyMapping<UserRoleDto, UserRole>(_userRolePropertyMapping));
            propertyMappings.Add(new PropertyMapping<RoleClaimDto, RoleClaim>(_userRoleClaimPropertyMapping));

        }

        private readonly IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            var matchMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            if (matchMapping.Count() == 1)
            {
                return matchMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var trimmedField = fields;

            var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase);
            var propertyName = indexOfFirstSpace == -1 ?
                trimmedField : trimmedField.Remove(indexOfFirstSpace);

            // find the matching property
            if (!propertyMapping.ContainsKey(propertyName))
            {
                return false;
            }

            return true;
        }
    }
}