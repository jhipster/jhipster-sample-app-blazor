using AutoMapper;
using System.Linq;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Dto;
using JhipsterSampleApplication.Dto.Authentication;


namespace JhipsterSampleApplication.Client.AutoMapper
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

            CreateMap<CountryModel, CountryDto>().ReverseMap();
            CreateMap<DepartmentModel, DepartmentDto>().ReverseMap();
            CreateMap<EmployeeModel, EmployeeDto>().ReverseMap();
            CreateMap<JobModel, JobDto>().ReverseMap();
            CreateMap<JobHistoryModel, JobHistoryDto>().ReverseMap();
            CreateMap<LocationModel, LocationDto>().ReverseMap();
            CreateMap<PieceOfWorkModel, PieceOfWorkDto>().ReverseMap();
            CreateMap<RegionModel, RegionDto>().ReverseMap();
            CreateMap<TimeSheetModel, TimeSheetDto>().ReverseMap();
            CreateMap<TimeSheetEntryModel, TimeSheetEntryDto>().ReverseMap();
            // jhipster-needle-add-dto-model-mapping - JHipster will add dto to model and model to dto mapping
        }
    }
}
