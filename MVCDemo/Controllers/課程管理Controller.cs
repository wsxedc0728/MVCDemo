using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class 課程管理Controller : Controller
    {
        private ContosoUniversityContext db;

        public 課程管理Controller(ContosoUniversityContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await db.Courses.Select(d => new CourseList
            {
                CourseId = d.CourseId,
                Title = d.Title,
                Credits = d.Credits,
                Description = d.Description,
                Slug = d.Slug,
                StartDate = d.StartDate
            }).ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseCreate data)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(new Course
                {
                    Title = data.Title,
                    Credits = data.Credits
                });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}
