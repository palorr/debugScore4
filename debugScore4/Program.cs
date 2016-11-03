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
            State s = new State();
            Player red = new Player(4, 1);
            Player yellow = new Player(4, 2);
            s.toGraph();
            Console.WriteLine(s.isTerminal());
            while (!s.isTerminal())
            {
                switch (s.getPlayer())
                {
                    case 1:
                        Console.WriteLine("red moves");
                        Move redMove = red.MiniMax(s);
                        s.push(redMove.getCol());
                        break;
                    case 2:
                        Console.WriteLine("yellow moves");
                        Move yellowMove = yellow.MiniMax(s);
                        s.push(yellowMove.getCol());
                        break;

                    default:
                        break;
                }
                Console.WriteLine(s.isTerminal());
                s.toGraph();
                
            }
            Console.WriteLine("this is the terminal state");
            Console.WriteLine(s.isTerminal());
            s.toGraph();
            Console.Read();


        }
    }
}
