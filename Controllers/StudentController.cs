using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace School.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Student.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var StudentFromDb = await _db.Student.FirstOrDefaultAsync(u => u.Id == id);
            if (StudentFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Student.Remove(StudentFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
