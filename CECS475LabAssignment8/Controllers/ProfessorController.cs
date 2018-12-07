using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CECS475LabAssignment8.Controllers
{
    [Route("professor")]
    public class ProfessorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Review> reviewCollec = new List<Review>();
            reviewCollec.Add(new Review
            {

                Content = "Cool thing Boss",
                Posted = DateTime.Now.ToUniversalTime(),
                Author = "Muneer Tomeh"
            }
            );

            var professors = new[]
            {


             new Professor()
            {
                Name = "Phuong Nguyen",
                ProfessorID = 1,
            },



            new Professor()
            {
                Name = "Muneer Tomeh",
                ProfessorID = 2,
                ReviewCollection = reviewCollec


            },
            };
            return View(professors);
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
        [Route("{Create}")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("{AddReview}/{id}")]
        public IActionResult AddReview(int? id)
        {
            return View();
        }

        [Route("{Reviews}/{id}")]
        public IActionResult Reviews(int? id)
        {
            List<Review> reviewCollec = new List<Review>();
            reviewCollec.Add(new Review
            {

                Content = "Cool thing Boss",
                Posted = DateTime.Now.ToUniversalTime(),
                Author = "Muneer Tomeh"
            }
            );

            Professor theUltimate = new Professor()
            {
                Name = "Phuong Nguyen",
                ProfessorID = 1,
                ReviewCollection = reviewCollec
            };

            return View(theUltimate);
        }
    }
}
