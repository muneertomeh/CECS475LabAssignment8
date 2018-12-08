using CECS475LabAssignment8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CECS475LabAssignment8.DAL
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AcademicContext(serviceProvider.GetRequiredService<DbContextOptions<AcademicContext>>()))
            {
                if(context.Professors.Any() || context.Reviews.Any()) {


                    return;

                }
                var professors = new List<Professor>()
                {
                    new Professor{ Name = "Kratos" },
                    new Professor{Name = "Atreus"},
                    new Professor{Name = "Patrick the Star"},
                    new Professor{Name = "Serious Guy"}
                };

                professors.ForEach(p => context.Professors.Add(p));
                context.SaveChanges();

                var reviews = new List<Review>()
                {
                    new Review{Content = "This professor is fantastic", Author = "Muneer", Posted=  DateTime.Now.ToUniversalTime(), ProfessorID =1},
                    new Review{Content = "This professor is not fantastic", Author = "Jake", Posted=  DateTime.Now.ToUniversalTime(), ProfessorID =1},
                    new Review{Content = "This guy needs a hot pepper asap", Author = "Kobe", Posted=  DateTime.Now.ToUniversalTime(), ProfessorID =2},
                    new Review{Content = "No hot pepper for you!!!", Author = "LeBron", Posted=  DateTime.Now.ToUniversalTime(), ProfessorID =3}
                };

                reviews.ForEach(r => context.Reviews.Add(r));
                context.SaveChanges();

            }
              
        }
    }
}
