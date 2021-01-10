using AutoMapper;
using System.Linq;
using Jhipster.Client.Models;
using Jhipster.Dto;


namespace Jhipster.Client.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, UserModel>()
                .ForMember(userModel => userModel.Authorities,
                    opt => opt.MapFrom(userDto => userDto.Roles))
                .ReverseMap()
                .ForPath(userDto => userDto.Roles,
                    opt => opt.MapFrom(userModel => userModel.Authorities));

            CreateMap<ManagedUserDto, UserSaveModel>().ReverseMap();
            CreateMap<LoginDto, LoginModel>().ReverseMap();

            CreateMap<RegionModel, RegionDto>().ReverseMap();
            CreateMap<CountryModel, CountryDto>().ReverseMap();
            CreateMap<LocationModel, LocationDto>().ReverseMap();
            CreateMap<DepartmentModel, DepartmentDto>().ReverseMap();
            CreateMap<PieceOfWorkModel, PieceOfWorkDto>().ReverseMap();
            CreateMap<EmployeeModel, EmployeeDto>().ReverseMap();
            CreateMap<JobModel, JobDto>().ReverseMap();
            CreateMap<JobHistoryModel, JobHistoryDto>().ReverseMap();
            // jhipster-needle-add-dto-model-mapping - JHipster will add dto to model and model to dto mapping
        }
    }
}
