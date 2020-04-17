using AutoMapper;

namespace RestWebApplication.Services.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}