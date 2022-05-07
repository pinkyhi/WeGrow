using AutoMapper;
using WeGrow.DAL.Entities;
using WeGrow.Models.SystemInstances;

namespace WeGrow.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ModuleInstance, ModuleInstanceViewModel>()
                .ForMember(d => d.ModuleName, opt => { opt.MapFrom(s => s.Module.Name); })
                .ForAllOtherMembers(x => x.MapAtRuntime());
            CreateMap<SystemInstance, SystemInstanceViewModel>()
                .ForMember(d => d.ModuleInstances, opt => { opt.Ignore(); })
                .ForAllOtherMembers(x => x.MapAtRuntime());
        }
    }
}
