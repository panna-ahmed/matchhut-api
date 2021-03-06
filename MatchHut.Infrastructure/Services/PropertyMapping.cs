using System.Collections.Generic;

namespace MatchHut.Infrastructure.Services
{
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary) => MappingDictionary = mappingDictionary;
    }
}