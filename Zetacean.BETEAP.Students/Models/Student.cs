using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zetacean.BETEAP.Students.Models
{
    [Index(nameof(DocumentNumber), IsUnique = true)]
    [Table("students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(10)]
        [Column("document_number")]
        public string DocumentNumber { get; set; }

        [Column("birth_date", TypeName = "timestamp without time zone")]
        public DateTime BirthDate { get; set; }

        [MaxLength(10)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(10)]
        [Column("middle_name")]
        public string MiddleName { get; set; }

        [MaxLength(10)]
        [Column("last_name")]
        public string LastName { get; set; }

        [MaxLength(10)]
        [Column("second_last_name")]
        public string SecondLastName { get; set; }
    }
}
