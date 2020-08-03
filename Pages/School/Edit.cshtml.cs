using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Model;

namespace School.Pages.School
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Student Student { get; set; }
        public async Task OnGet(int id)
        {
            Student = await _db.Student.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var StudentFromDB = await _db.Student.FindAsync(Student.Id);
                StudentFromDB.FirstName = Student.FirstName;
                StudentFromDB.LastName = Student.LastName;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

    }
}