using AutoMapper;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Helpers;
using Zetacean.BETEAP.Students.Models;

namespace Zetacean.BETEAP.Students.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentInsertDto, Student>()
                .ForMember(s => s.DocumentNumber, opt => opt.MapFrom(d => d.DocumentNumber.Trim()))
                .ForMember(
                    s => s.BirthDate,
                    opt => opt.MapFrom(d => DateHelper.ParseDate(d.BirthDate))
                )
                .ForMember(
                    s => s.FirstName,
                    opt => opt.MapFrom(d => d.FirstName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.MiddleName,
                    opt => opt.MapFrom(d => d.MiddleName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.LastName,
                    opt => opt.MapFrom(d => d.LastName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.SecondLastName,
                    opt => opt.MapFrom(d => d.SecondLastName.Trim().UppercaseFirstWord())
                );

            CreateMap<Student, StudentDto>();

            CreateMap<StudentUpdateDto, Student>()
                .ForMember(s => s.DocumentNumber, opt => opt.MapFrom(d => d.DocumentNumber.Trim()))
                .ForMember(
                    s => s.BirthDate,
                    opt => opt.MapFrom(d => DateHelper.ParseDate(d.BirthDate))
                )
                .ForMember(
                    s => s.FirstName,
                    opt => opt.MapFrom(d => d.FirstName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.MiddleName,
                    opt => opt.MapFrom(d => d.MiddleName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.LastName,
                    opt => opt.MapFrom(d => d.LastName.Trim().UppercaseFirstWord())
                )
                .ForMember(
                    s => s.SecondLastName,
                    opt => opt.MapFrom(d => d.SecondLastName.Trim().UppercaseFirstWord())
                );
        }
    }
}
