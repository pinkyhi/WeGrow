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
                .ForMember(d => d.Subject, opt => { opt.MapFrom(s => s.Module.Subject); })
                .ForAllOtherMembers(x => x.MapAtRuntime());
            CreateMap<SystemInstance, SystemInstanceViewModel>()
                .ForMember(d => d.ModuleInstances, opt => { opt.Ignore(); })
                .ForMember(d => d.ModuleSchedules, opt => { opt.Ignore(); })
                .ForMember(d => d.LastGrow, opt => { opt.Ignore(); })
                .ForAllOtherMembers(x => x.MapAtRuntime());
        }
    }
}
