using Assignment.Core.Dtos.AuthDtos;
using Assignment.Core.Dtos.TaskModelDtos;
using Assignment.Core.Entities;
using Assignment.Core.Entities.Identity;
using AutoMapper;

namespace Assignment.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, UserInfoDto>();
            CreateMap<AppUser, UserFullInfoDto>();


            CreateMap<TaskModelDto, TaskModel>().ReverseMap();
            CreateMap<TaskModel, TaskModelReturnDto>()
                .ForMember(dist => dist.StatusLabel, o => o.MapFrom(t => t.Status.ToString()))
                .ForMember(dist => dist.PriorityLabel, o => o.MapFrom(t => t.Priority.ToString()));
            CreateMap<TaskModelUpdateDto, TaskModel>()
                   .ForMember(t => t.Id, o => o.Ignore());

        }
    }
}
