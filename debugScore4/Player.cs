using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debugScore4
{
    class Player
    {
        private int maxDepth;
        private int player; //the player we want to autoplay (1 for red , 2 for yellow)

        public Player(int maxDepth, int player)//ctor
        {
            this.maxDepth = maxDepth;
            this.player = player;
        }

        public Move MiniMax(State state)
        {
            //If the RED plays then it wants to MAXimize the heuristics value
            if (player == 1)
            {
                return max(new State(state), 0);
            }
            //If the YELLOW plays then it wants to MINimize the heuristics value
            else
            {
                return min(new State(state), 0);
            }
        }

        public Move max(State state, int depth)
        {
            Random r = new Random();
            
            if ((state.isTerminal()) || (depth == this.maxDepth))
            {
                Move lastMove = new Move(state.getLastCol(), state.getScore());
                return lastMove;
            }
            //The children-moves of the state are calculated
            List<State> children = new List<State>(state.GetChildren());
            Move maxMove = new Move(Int32.MinValue);
            foreach (State child in children)
            {
                //And for each child min is called, on a lower depth
                Move move = min(child, depth + 1);
                //The child-move with the greatest value is selected and returned by max
                if (move.getValue() >= maxMove.getValue())
                {
                    if ((move.getValue() == maxMove.getValue()))
                    {
                        //If the heuristic has the same value then we randomly choose one of the two moves
                        if (r.Next(2) == 0)
                        {
                            maxMove.setCol(child.getLastCol());
                            maxMove.setValue(move.getValue());
                        }
                    }
                    else
                    {
                        maxMove.setCol(child.getLastCol());
                        maxMove.setValue(move.getValue());
                    }
                }
            }
            return maxMove;
        }
        public Move min(State state, int depth)
        {
            Random r = new Random();

            if ((state.isTerminal()) || (depth == this.maxDepth))
            {
                Move lastMove = new Move(state.getLastCol(), state.getScore());
                return lastMove;
            }
            List<State> children = new List<State>(state.GetChildren());
            Move minMove = new Move(Int32.MaxValue);
            foreach (State child in children)
            {
                Move move = max(child, depth + 1);
                if (move.getValue() <= minMove.getValue())
                {
                    if ((move.getValue() == minMove.getValue()))
                    {
                        if (r.Next(2) == 0)
                        {
                            minMove.setCol(child.getLastCol());
                            minMove.setValue(move.getValue());
                        }
                    }
                    else
                    {
                        minMove.setCol(child.getLastCol());
                        minMove.setValue(move.getValue());
                    }
                }
            }
            return minMove;
        }

    }
}
