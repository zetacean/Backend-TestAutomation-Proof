using Microsoft.EntityFrameworkCore;
using Zetacean.BETEAP.Students.Models;

namespace Zetacean.BETEAP.Students.Repository
{
    public class StudenRepository(StudentContext context) : IRepository<Student>
    {
        private readonly StudentContext _context = context;

        public async Task<IEnumerable<Student>> Get() => await _context.Students.ToListAsync();

        public async Task<Student> GetById(int id) => await _context.Students.FindAsync(id);

        public async Task Add(Student student) => await _context.Students.AddAsync(student);

        public void Update(Student student)
        {
            _context.Students.Attach(student);
            _context.Students.Entry(student).State = EntityState.Modified;
        }

        public void Delete(Student student) => _context.Students.Remove(student);

        public async Task Save() => await _context.SaveChangesAsync();

        public IEnumerable<Student> Search(Func<Student, bool> filter) =>
            [.. _context.Students.Where(filter)];
    }
}
