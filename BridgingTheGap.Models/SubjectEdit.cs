using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
   public class SubjectEdit
    {
        public int SubjectId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "Subject")]
        [MinLength(2, ErrorMessage = " The subject must be at least two characters. ")]
        [MaxLength(64, ErrorMessage = " The subject can not be longer than 64 characters")]
        public string Name { get; set; }

    }
}
