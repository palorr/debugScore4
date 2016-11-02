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
            //State test = new State(1);
            //test.toGraph();
            //Console.WriteLine(test.isTerminal());
            s4Player red = new s4Player(1, 1);
            s4Player yellow = new s4Player(1, 2);
            State state = new State(1);

            state.toGraph();
            while (!state.isTerminal())
            {
                Console.WriteLine();
                switch (state.getPlayer())
                {
                    case 2:
                        Console.WriteLine("red moves");
                        Move RedMove = red.MiniMax(state);
                        state.makeMove(RedMove.getRow(), RedMove.getCol(), 1);
                        break;
                    case 1:
                        Console.WriteLine("yellow moves");
                        Move YellowMove = yellow.MiniMax(state);
                        state.makeMove(YellowMove.getRow(), YellowMove.getCol(), 2);
                        break;
                    default:
                        break;
                }
                state.toGraph();

            }
            Console.Read();
        }
    }
}
