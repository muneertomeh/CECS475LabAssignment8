using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CECS475LabAssignment8.Models
{
    public class Professor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfessorID { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Review> ReviewCollection { get; set; }
    }
}
