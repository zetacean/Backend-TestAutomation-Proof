using FluentValidation;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Helpers;

namespace Zetacean.BETEAP.Students.Validators
{
    public class StudentInsertValidator : AbstractValidator<StudentInsertDto>
    {
        public StudentInsertValidator()
        {
            RuleFor(x => x.DocumentNumber)
                .NotEmpty()
                .WithMessage("El número de documento es obligatorio")
                .Matches("^[0-9]+$")
                .WithMessage("El número de documento solo debe contener números")
                .Length(8, 10)
                .WithMessage("El número de documento debe tener entre 8 y 10 caracteres");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es obligatoria")
                .Must(d => DateHelper.TryParseDate(d, out _))
                .WithMessage($"El formato de la fecha no es válido, debe ser 'dd/MM/yyyy'")
                .Must(d => DateHelper.TryParseDate(d, out var date) && date <= DateTime.Today)
                .WithMessage("La fecha ingresada no puede ser una fecha futura")
                .Must(d =>
                    DateHelper.TryParseDate(d, out var date)
                    && date <= DateTime.Today.AddYears(Constants.MinAge)
                )
                .WithMessage("El estudiante debe tener 4 años o más");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("El nombre del estudiante es obligatorio")
                .Matches("^[A-Za-zÁÉÍÓÚáéíóúÑñÜü]+(?: [A-Za-zÁÉÍÓÚáéíóúÑñÜü]+)*$")
                .WithMessage("El nombre del estudiante solo debe contener letras")
                .Length(2, 50)
                .WithMessage("El nombre del estudiante debe tener entre 2 y 50 caracteres");

            RuleFor(x => x.MiddleName)
                .Matches("^[A-Za-zÁÉÍÓÚáéíóúÑñÜü]+(?: [A-Za-zÁÉÍÓÚáéíóúÑñÜü]+)*$")
                .WithMessage("El segundo nombre del estudiante solo debe contener letras")
                .Length(2, 50)
                .WithMessage("El segundo nombre del estudiante debe tener entre 2 y 50 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("El apellido del estudiante es obligatorio")
                .Matches("^[A-Za-zÁÉÍÓÚáéíóúÑñÜü]+(?: [A-Za-zÁÉÍÓÚáéíóúÑñÜü]+)*$")
                .WithMessage("El apellido del estudiante solo debe contener letras")
                .Length(2, 50)
                .WithMessage("El apellido del estudiante debe tener entre 2 y 50 caracteres");

            RuleFor(x => x.SecondLastName)
                .Matches("^[A-Za-zÁÉÍÓÚáéíóúÑñÜü]+(?: [A-Za-zÁÉÍÓÚáéíóúÑñÜü]+)*$")
                .WithMessage("El segundo apellido del estudiante solo debe contener letras")
                .Length(2, 50)
                .WithMessage(
                    "El segundo apellido del estudiante debe tener entre 2 y 50 caracteres"
                );
        }
    }
}
