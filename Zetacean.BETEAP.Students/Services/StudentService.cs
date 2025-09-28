using AutoMapper;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Models;
using Zetacean.BETEAP.Students.Repository;

namespace Zetacean.BETEAP.Students.Services
{
    public class StudentService(IRepository<Student> studentRepository, IMapper mapper)
        : ICommonService<StudentDto, StudentInsertDto, StudentUpdateDto>
    {
        private readonly IRepository<Student> _studentRepository = studentRepository;
        private readonly IMapper _mapper = mapper;

        public IList<string> Errors => [];

        public async Task<IEnumerable<StudentDto>> Get()
        {
            var students = await _studentRepository.Get();

            return students.Select(s => _mapper.Map<StudentDto>(s));
        }

        public async Task<StudentDto> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);

            return student != null ? _mapper.Map<StudentDto>(student) : null;
        }

        public async Task<StudentDto> Add(StudentInsertDto studentInsertDto)
        {
            var student = _mapper.Map<Student>(studentInsertDto);

            await _studentRepository.Add(student);
            await _studentRepository.Save();

            return _mapper.Map<Student, StudentDto>(student);
        }

        public async Task<StudentDto> Update(int id, StudentUpdateDto studentUpdateDto)
        {
            var student = await _studentRepository.GetById(id);

            if (student != null)
            {
                student = _mapper.Map<StudentUpdateDto, Student>(studentUpdateDto, student);
                _studentRepository.Update(student);
                await _studentRepository.Save();
            }
            return null;
        }

        public async Task<StudentDto> Delete(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student != null)
            {
                var studentDto = _mapper.Map<StudentDto>(student);

                _studentRepository.Delete(student);
                await _studentRepository.Save();

                return studentDto;
            }
            return null;
        }

        public bool Validate(StudentInsertDto studentInsertDto)
        {
            if (
                _studentRepository
                    .Search(s => s.DocumentNumber == studentInsertDto.DocumentNumber)
                    .Any()
            )
            {
                Errors.Add("El documento de identidad ya se encuentra registrado");
            }
            return true;
        }

        public bool Validate(int id, StudentUpdateDto studentUpdateDto)
        {
            if (
                _studentRepository
                    .Search(s => s.DocumentNumber == studentUpdateDto.DocumentNumber && s.Id != id)
                    .Any()
            )
            {
                Errors.Add(
                    "El documento de identidad ya se encuentra registrado por otro estudiante"
                );
            }
            return true;
        }
    }
}
