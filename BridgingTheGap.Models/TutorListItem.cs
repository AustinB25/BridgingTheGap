using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class TutorListItem
    {
        public int TutorId { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Tutor Name")]
        public string FullName { get { return $"{FirstName} { LastName}"; } }
        [Display(Name = "Teaches # of subjects")]
        public int NumberOfSubjects { get; set; }
    }
}
