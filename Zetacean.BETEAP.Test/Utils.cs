using Bogus;
using Zetacean.BETEAP.Students.Models;

namespace Zetacean.BETEAP.Test
{
    internal static class Utils
    {
        private static int Id = 1;
        private static readonly Faker<Student> FakeStudents = new Faker<Student>();

        public static Student GenerateRandomStudent()
        {
            FakeStudents
                .RuleFor(s => s.Id, _ => Id++)
                .RuleFor(s => s.BirthDate, f => f.Date.Past(f.Random.Int(4, 16), DateTime.Now))
                .RuleFor(s => s.DocumentNumber, f => f.Finance.Account())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName());

            return FakeStudents.Generate();
        }
    }
}
