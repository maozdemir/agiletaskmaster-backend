using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Task, TaskDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap(); 
        }
    }
}
