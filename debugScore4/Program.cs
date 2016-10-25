using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debugScore4
{
    class Program
    {
        static void Main(string[] args)
        {
            State s = new State(1);
            s.toGraph();
            s.push(0, 1);
            s.toGraph();
            s.push(1, 2);
            s.toGraph();
            s.push(1, 1);
            s.toGraph();
            s.push(1, 2);
            s.toGraph();
            s.push(0, 1);
            s.toGraph();
            s.push(2, 2);
            s.toGraph();
            s.push(0, 1);
            s.toGraph();
            s.push(0, 1);
            s.toGraph();
            s.push(2, 2);
            s.toGraph();
            s.push(2, 2);
            s.toGraph();
            s.push(3, 2);
            s.toGraph();
            s.push(1, 1);
            s.toGraph();
            s.push(2, 1);
            s.toGraph();
            s.push(4, 2);
            s.toGraph();
            s.push(3, 1);
            s.toGraph();
            s.push(3, 2);
            s.toGraph();
            s.push(4, 1);
            s.toGraph();
            s.push(4, 1);
            s.toGraph();
            s.push(4, 2);
            s.toGraph();
            s.heuristic();
            

            //s.push(2, 1);
            //s.toGraph();
            //s.push(3, 2);
            //s.toGraph();
            //s.push(3, 2);
            //s.toGraph();
            //s.push(3, 2);
            //s.toGraph();
            //s.push(3, 1);
            //s.toGraph();
            //s.push(4, 2);
            //s.toGraph();
            //s.push(4, 1);
            //s.toGraph();
            //s.push(4, 1);
            //s.toGraph();
            //s.push(5, 1);
            //s.toGraph();
            //s.push(5, 1);
            //s.toGraph();
            //s.push(5, 1);
            //s.toGraph();
            //s.push(5, 2);
            //Console.WriteLine(s.isTerminal());

            //s.toGraph();
            //s.push(6, 1);
            //Console.WriteLine(s.isTerminal());
            //s.toGraph();
            //
            Console.Read();
        }
    }
}
