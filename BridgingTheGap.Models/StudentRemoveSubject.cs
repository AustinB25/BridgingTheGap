using BridgingTheGap.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class StudentRemoveSubject
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SubjectId { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
