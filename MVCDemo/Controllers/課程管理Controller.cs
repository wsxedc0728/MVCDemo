using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class 課程管理Controller : Controller
    {
        private ICourseRepository repo;

        public 課程管理Controller(ICourseRepository repo, IUnitOfWork uow)
        {
            this.repo = repo;
            this.repo.UnitOfWork = uow;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var data = await repo.FindAll().Select(d => new CourseList
            {
                CourseId = d.CourseId,
                Title = d.Title,
                Credits = d.Credits,
                Description = d.Description,
                Slug = d.Slug,
                StartDate = d.StartDate
            }).ToListAsync();

            return View(data);
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
                repo.Add(new Course
                {
                    Title = data.Title,
                    Credits = data.Credits
                });
                repo.UnitOfWork.Commit();

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

            var course = await repo.FindOneJoinDepartment(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}
