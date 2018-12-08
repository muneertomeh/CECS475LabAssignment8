using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.DAL;
using CECS475LabAssignment8.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CECS475LabAssignment8.Controllers
{
    [Route("Professor")]
    public class ProfessorController : Controller
    {

        private readonly AcademicContext _db;

        public ProfessorController(AcademicContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            var professors = from p in _db.Professors select p;
            return View(professors.ToList());


            //List<Review> reviewCollec = new List<Review>();
            //reviewCollec.Add(new Review
            //{

            //    Content = "Cool thing Boss",
            //    Posted = DateTime.Now.ToUniversalTime(),
            //    Author = "Muneer Tomeh"
            //}
            //);

            //var professors = new[]
            //{


            // new Professor()
            //{
            //    Name = "Phuong Nguyen",
            //    ProfessorID = 1,
            //},



            //new Professor()
            //{
            //    Name = "Muneer Tomeh",
            //    ProfessorID = 2,
            //    ReviewCollection = reviewCollec


            //},
            //};
            //return View(professors);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key) {



            var theReview = new Review()
            {
                Title = "This new Review",
                Posted = DateTime.Now.ToUniversalTime(),
                Author = "Muneer",
                Content = "This thing is super great"
            };
            return View(theReview);

        }

        [HttpGet, Route("Create")]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost, Route("Create")]
        public IActionResult Create(Professor theProfessor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            } 
                
            _db.Professors.Add(theProfessor);
            _db.SaveChanges();

            return View(theProfessor);
        }

        [HttpGet, Route("AddReview/{id}")]
        public IActionResult AddReview(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            var professor = (from p in _db.Professors where p.ProfessorID == id select p).FirstOrDefault();
            if(professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }


        [HttpPost, Route("AddReview/{id}")]
        public IActionResult AddReview(int? id, Review theReview)
        {
            _db.Reviews.Add(theReview);
            _db.SaveChanges();
            
            return View();
        }



        [Route("Reviews/{id}")]
        public IActionResult Reviews(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var professor = _db.Professors.Include("ReviewCollection").SingleOrDefault(x=>x.ProfessorID == id);
            if (professor == null)
            {
                return NotFound();
            }
            var reviews = from rev in _db.Reviews where rev.ProfessorID == id select rev;

            foreach (var rev in reviews)
            {
                professor.ReviewCollection.Add(rev);
            }
            return View(professor);




            //List<Review> reviewCollec = new List<Review>();
            //reviewCollec.Add(new Review
            //{

            //    Content = "Cool thing Boss",
            //    Posted = DateTime.Now.ToUniversalTime(),
            //    Author = "Muneer Tomeh"
            //}
            //);

            //Professor theUltimate = new Professor()
            //{
            //    Name = "Phuong Nguyen",
            //    ProfessorID = 1,
            //    ReviewCollection = reviewCollec
            //};


        }
    }
}
