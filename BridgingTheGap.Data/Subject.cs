using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Data
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; } = new HashSet<Tutor>();
        
        public virtual ICollection<Student> Students { get; set; }
        public Subject() 
        {
            this.Students= new HashSet<Student>(); 
        }
    }
}
