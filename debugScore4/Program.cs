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
            //List<State> test = new List<State>() ;
            //test = s.GetChildren();

            ////for (int i = 0; i < test.Count; i++)
            ////{
            ////    test[i].toGraph(); 
            ////}
            //State tmp = test[3];
            //test.Clear();
            //test = tmp.GetChildren();
            //for (int i = 0; i < test.Count; i++)
            //{
            //    test[i].toGraph();
            //}
            //Console.Read();

            s.toGraph();
            //Console.WriteLine("s.isTerminal():", s.isTerminal());

            s.push(0, 1);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(1, 2);
            s.push(1, 1);
            s.push(1, 1);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(2, 1);
            s.push(2, 1);
            s.push(2, 2);
            s.push(2, 1);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(3, 2);
            s.push(3, 2);
            s.push(3, 2);
            s.push(3, 1);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(4, 2);
            s.push(4, 1);
            s.push(4, 1);
            s.toGraph();
            s.heuristic();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(5, 1);
            s.push(5, 1);
            s.push(5, 1);
            s.push(5, 2);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            s.push(6, 1);
            s.toGraph();
            Console.WriteLine("s.isTerminal():" + s.isTerminal());

            Console.Read();
        }
    }
}
