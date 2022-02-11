using BridgingTheGap.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class TutorRemoveSubject
    {
        public int TutorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SubjectId { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
