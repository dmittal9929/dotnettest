using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiDemo.Models
{
    public class EmpEntities:DbContext
    {
        public EmpEntities(DbContextOptions<EmpEntities> options): base(options)
        {

        }

        public DbSet<Emp> Emps { get; set; }
    }
}
