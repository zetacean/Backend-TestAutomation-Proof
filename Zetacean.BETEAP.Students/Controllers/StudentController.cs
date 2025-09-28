using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Services;

namespace Zetacean.BETEAP.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(
        IValidator<StudentInsertDto> studentInsertValidator,
        IValidator<StudentUpdateDto> studentUpdateValidator,
        [FromKeyedServices("studentService")]
            ICommonService<StudentDto, StudentInsertDto, StudentUpdateDto> studentService
    ) : ControllerBase
    {
        private readonly ICommonService<
            StudentDto,
            StudentInsertDto,
            StudentUpdateDto
        > _studentService = studentService;
        private readonly IValidator<StudentInsertDto> _studentInsertValidator =
            studentInsertValidator;
        private readonly IValidator<StudentUpdateDto> _studentUpdateValidator =
            studentUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get() => await _studentService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var studentDto = await _studentService.GetById(id);

            return studentDto == null ? NotFound() : Ok(studentDto);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> Add(StudentInsertDto studentInsertDto)
        {
            var validationResult = await _studentInsertValidator.ValidateAsync(studentInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_studentService.Validate(studentInsertDto))
            {
                return BadRequest(_studentService.Errors);
            }

            var studentDto = await _studentService.Add(studentInsertDto);

            return CreatedAtAction(
                nameof(GetById).ToLower(),
                new { id = studentDto.Id },
                studentDto
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Update(
            int id,
            StudentUpdateDto studentUpdateDto
        )
        {
            var validationResult = await _studentUpdateValidator.ValidateAsync(studentUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            if (!_studentService.Validate(id, studentUpdateDto))
            {
                return BadRequest(_studentService.Errors);
            }

            var studentDto = await _studentService.Update(id, studentUpdateDto);

            return studentDto == null ? NotFound() : Ok(studentDto);
        }

        [HttpDelete]
        public async Task<ActionResult<StudentDto>> Delete(int id) =>
            (await _studentService.Delete(id)) is { } beerDto ? Ok(beerDto) : NotFound();
    }
}
