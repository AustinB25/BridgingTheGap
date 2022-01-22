using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class StudentDetail
    {
        public int StudentId { get; set; }
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName}{ LastName}"; } }
        public int NumberOfSubjects { get; set; }
    }
}
