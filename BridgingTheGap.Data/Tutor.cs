using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Data
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Display(Name = " Full Name")]
        public string FullName { get { return $"{FirstName } {LastName}"; } }
        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
    }
}
