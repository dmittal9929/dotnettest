using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiDemo.Models
{
    public interface IMyLogger
    {
        void Log(string msg);
    }

    class MyLogger : IMyLogger
    {
        int i = 1;
        public void Log(String msg)
        {
            Console.WriteLine($"My Logger {i++}:{msg} at {DateTime.Now}");
        }
    }
}
