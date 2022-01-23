using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Models
{
    public class SubjectListItem
    {
        public int SubjectId { get; set; }
        [Display(Name = "Subject")]
        public string Name { get; set; }
    }
}
