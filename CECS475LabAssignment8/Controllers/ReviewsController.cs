using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.DAL;
using CECS475LabAssignment8.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CECS475LabAssignment8.Controllers
{
    [Route("api/posts/{professorKey}/comments")]
    public class ReviewsController : Controller
    {
        private readonly AcademicContext _db;

        public ReviewsController(AcademicContext db)
        {
            _db = db;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IQueryable<Review> Get(int id)
        {
            return _db.Reviews.Where(r=>r.ProfessorID == id);
        }

        // POST api/<controller>
        [HttpPost]
        public Review Post(int id, [FromBody]Review r)
        {
            var professor = _db.Professors.Where(p=>p.ProfessorID == id);

            if(professor!= null)
            {
                r.Posted = DateTime.Now;
                r.ProfessorID = id;
                _db.Reviews.Add(r);
                _db.SaveChanges();
                
            }
            return r;


        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
