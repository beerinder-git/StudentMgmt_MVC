using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentMgmt.Data;
using StudentMgmt.Models;

namespace StudentMgmt.Controllers
{
    public class EnrolsController : Controller
    {
        private readonly StudentMgmtContext _context;

        public EnrolsController(StudentMgmtContext context)
        {
            _context = context;
        }

        // GET: Enrols
        public async Task<IActionResult> Index()
        {
            var studentMgmtContext = _context.Enrol.Include(e => e.Course).Include(e => e.Student);
            return View(await studentMgmtContext.ToListAsync());
        }

        // GET: Enrols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrol = await _context.Enrol
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrol == null)
            {
                return NotFound();
            }

            return View(enrol);
        }

        // GET: Enrols/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseName");
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentFname");
            return View();
        }

        // POST: Enrols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,CourseID")] Enrol enrol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", enrol.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", enrol.StudentID);
            return View(enrol);
        }

        // GET: Enrols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrol = await _context.Enrol.FindAsync(id);
            if (enrol == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseName", enrol.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentFname", enrol.StudentID);
            return View(enrol);
        }

        // POST: Enrols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudentID,CourseID")] Enrol enrol)
        {
            if (id != enrol.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolExists(enrol.ID))
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
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", enrol.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", enrol.StudentID);
            return View(enrol);
        }

        // GET: Enrols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrol = await _context.Enrol
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrol == null)
            {
                return NotFound();
            }

            return View(enrol);
        }

        // POST: Enrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrol = await _context.Enrol.FindAsync(id);
            _context.Enrol.Remove(enrol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolExists(int id)
        {
            return _context.Enrol.Any(e => e.ID == id);
        }
    }
}
