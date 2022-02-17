using BridgingTheGap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{   
    public class SubjectDetail
    {
        [Display(Name = "Subject Id: ")]
        public int SubjectId { get; set; }
        public Guid OwnerId { get; set; }
        [Display(Name = "Subject: ")]
        public string Name { get; set; }
        [Display(Name = "Stundents in class: ")]
         
        public List<Student> Students { get; set; }
        
        public List<Tutor> Tutors { get; set; }
    }
}
