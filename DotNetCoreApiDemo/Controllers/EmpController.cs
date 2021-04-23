using DotNetCoreApiDemo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiDemo.Controllers
{
    [Route("api/Emp")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class EmpController : ControllerBase
    {
        private readonly EmpEntities _context;
        public EmpController(EmpEntities e)
        {
            _context = e;
        }
        [HttpGet]
        public ActionResult<IQueryable<Emp>> GetEmp()
        {
            Console.WriteLine("Emps");
            return _context.Emps;
        }




        [HttpGet("{id}")]
        public ActionResult<Emp> GetByID(int? id)
        {
            
            if (id == null)
            {
                return BadRequest();
            }

            var res = _context.Emps.Find(id);
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        [HttpPost]
        public ActionResult<Emp> AddEmp(Emp e)
        {
            Console.WriteLine("inside");
            if (e == null)
            {
                Console.WriteLine("bad");
                return BadRequest();
            }
            try
            {
                Console.WriteLine(e);
                _context.Emps.Add(e);
                _context.SaveChanges();
                
            }
            catch(DbUpdateException)
            {
                return Conflict();
            }
            return  e;
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveEmp(int? id)
        {
            Console.WriteLine("delete");
            if (id == null)
            {
                return BadRequest();
            }
            var emp = _context.Emps.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            _context.Emps.Remove(emp);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmp(int? id,Emp e)
        {
            if (id == null || id!=e.ID)
            {
                return BadRequest();
            }
            _context.Emps.Update(e);
            try
            {
                _context.SaveChanges();

            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
            return CreatedAtAction("GetByID", new { id = e.ID }, e);
        }

    }
}
