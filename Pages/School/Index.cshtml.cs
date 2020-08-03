using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using School.Model;

namespace School.Pages.School
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Student> Students { get; set; }
        public async Task OnGet()
        {
            Students = await _db.Student.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Student = await _db.Student.FindAsync(id);
            if(Student == null)
            {
                return NotFound();
            }
            _db.Student.Remove(Student);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}