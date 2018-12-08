using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CECS475LabAssignment8.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewID { get; set; }   

        public DateTime Posted { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public int ProfessorID { get; set; }
        public virtual Professor Professor { get; set;}
    }
}
