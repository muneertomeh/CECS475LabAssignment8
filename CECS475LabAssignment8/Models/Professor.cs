using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CECS475LabAssignment8.Models
{
    public class Professor
    {
        public string Name { get; set; }
        public int ProfessorID { get; set; }
        public List<Review> ReviewCollection { get; set; }
    }
}
