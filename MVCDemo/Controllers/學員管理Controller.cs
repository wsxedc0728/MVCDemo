using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class 學員管理Controller : Controller
    {
        private readonly ContosoUniversityContext _context;

        public 學員管理Controller(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET: 學員管理
        public async Task<IActionResult> IndexAsync()
        {
            // LINQ 方法語法
            //var data = await _context.People
            //    .Where(d => d.HireDate != null && d.EnrollmentDate != null)
            //    .Select(d => new PersonList
            //    {
            //        Id = d.Id,
            //        LastName = d.LastName,
            //        FirstName = d.FirstName,
            //        HireDate = d.HireDate,
            //        EnrollmentDate = d.EnrollmentDate,
            //        Discriminator = d.Discriminator
            //    })
            //    .ToListAsync();

            // LINQ 查詢運算式
            var data = await (from person in _context.People
                        where person.HireDate != null && person.EnrollmentDate != null
                      select new PersonList
                      {
                          Id = person.Id,
                          LastName = person.LastName,
                          FirstName = person.FirstName,
                          HireDate = person.HireDate,
                          EnrollmentDate = person.EnrollmentDate,
                          Discriminator = person.Discriminator
                      }).ToListAsync();

            return View(data);
        }

        // GET: 學員管理/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: 學員管理/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: 學員管理/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,HireDate,EnrollmentDate,Discriminator")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: 學員管理/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: 學員管理/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,HireDate,EnrollmentDate,Discriminator")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: 學員管理/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: 學員管理/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
