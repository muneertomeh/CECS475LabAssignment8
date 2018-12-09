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
        public async Task<IActionResult> Create([Bind("ProfessorID, Name")]Professor theProfessor)
        {
            if (ModelState.IsValid)
            {
                _db.Professors.Add(theProfessor);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
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
            //Since the professor has been found...
            return View(new Review());
        }


        [HttpPost, Route("AddReview/{id}")]
        public async Task<IActionResult> AddReview(int id, [Bind("Author, Content", "Title")]Review r)
        {

            if(ModelState.IsValid == true)
            {

                var reviewsCollection = from review in _db.Reviews select review;
                var reviewsList = reviewsCollection.ToList();
                int count = reviewsList.Count;

                r.ReviewID = count + 1;
                r.ProfessorID = id;
                r.Posted = DateTime.Now.ToUniversalTime();
                _db.Reviews.Add(r);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            
            return View(r);
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

            if(professor.ReviewCollection != null)
            {
                return View(professor);
            }

            return RedirectToAction("Index");


        }
    }
}
