using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiDemo.Models
{
        public class Emp
        {
            [Key]
            public int ID { get; set; }
            public string name { get; set; }
        
            public int sal { get; set; }
        }
}
