using BridgingTheGap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class TutorDetails
    {
        [Display(Name = "Tutor ID:")]
        public int TutorId { get; set; }
        public Guid OwnerId { get; set; }
        [Display(Name = "First name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last name:")]
        public string LastName { get; set; }
        [Display(Name = "Tutor:")]
        public string  FullName { get { return $"{FirstName}{ LastName}"; } }
        [Display(Name = "Subjects")]
        public List<Subject> Subjects { get; set; }
    }
}
