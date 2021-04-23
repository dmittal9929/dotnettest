using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCoreApiDemo.Models;

namespace DotNetCoreApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpsController : ControllerBase
    {
        private readonly EmpEntities _context;
        private readonly IMyLogger _myLogger;
        public EmpsController(EmpEntities context/*,IMyLogger my*/)
        {
            _context = context;
            //_myLogger = my;
        }

        // GET: api/Emps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emp>>> GetEmps([FromServices]IMyLogger my)
        {

/*            var services = HttpContext.RequestServices;
            var my = services.GetService(typeof(IMyLogger)) as IMyLogger;*/
            my.Log("My Method DI");
            return await _context.Emps.ToListAsync();
        }

        // GET: api/Emps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emp>> GetEmp(int id)
        {
            var emp = await _context.Emps.FindAsync(id);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        // PUT: api/Emps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmp(int id, Emp emp)
        {
            if (id != emp.ID)
            {
                return BadRequest();
            }

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Emps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emp>> PostEmp(Emp emp)
        {
            _context.Emps.Add(emp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmp", new { id = emp.ID }, emp);
        }

        // DELETE: api/Emps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmp(int id)
        {
            var emp = await _context.Emps.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            _context.Emps.Remove(emp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpExists(int id)
        {
            return _context.Emps.Any(e => e.ID == id);
        }
    }
}
