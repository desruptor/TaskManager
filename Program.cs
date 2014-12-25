using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mephi.Cybernetics.Nm.TaskManager
{
    class Program
    {

        static void Main(string[] args)
        {
            int p = 1;
            int o = 1;
            TaskQueue tq = new TaskQueue();
            TaskRE zTaskRe = new TaskRE(String.Format("task1"), argumnets: new object[]{p,o}, 
            d: new Func<int,int, int>((int k,int l) => 
            { 
                return k+l;
            }));
            tq.AddTask(zTaskRe);
            while (!zTaskRe.ResultValue.HasValue());
            var j = zTaskRe.ResultValue.Value;
            Console.WriteLine(j);
            Console.ReadLine();
        }
    }
}
