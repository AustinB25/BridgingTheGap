using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class TutorEdit
    {
        public int TutorId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = " Your name must be at least two characters. ")]
        [MaxLength(64, ErrorMessage = " Your name can not be longer than 64 characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = " Your name must be at least two characters. ")]
        [MaxLength(64, ErrorMessage = " Your name can not be longer than 64 characters")]
        public string LastName { get; set; }
        
    }
}
