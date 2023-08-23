using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using Model;

namespace Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Student, StudentDTO>().ReverseMap(); // necemo obostrano jer kod edita trazi edit Id a to ne zelimo

            CreateMap<Student, StudentDTO>();

            CreateMap<StudentDTO, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegisteredOn, opt => { opt.Condition(src => src.RegisteredOn != default(DateTime)); opt.MapFrom(src => src.RegisteredOn); });

        }
    }
}
