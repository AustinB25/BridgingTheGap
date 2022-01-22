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
        public virtual IEnumerable<Student> Students { get; set; } = new List<Student>();

    }
}
