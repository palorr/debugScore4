using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debugScore4
{
    class s4Player
    {
        private int maxDepth;
        private int player;
        //ctors
        public s4Player()
        {
            maxDepth = 5;
            this.player = State.PLAYER_RED; 
        }
        public s4Player(int maxDepth, int player)
        {
            this.maxDepth = maxDepth;
            this.player = player;
        }
        //
        public Move MiniMax(State state)
        {
            //If the red plays then it wants to MAXimize the heuristics value
            if (player == State.PLAYER_RED)
            {
                return max(new State(state), 0);
            }
            //If the yellow plays then it wants to MINimize the heuristics value
            else
            {
                return min(new State(state), 0);
            }
        }

        public Move max(State state, int depth)
        {
            Random r = new Random();

            /* If MAX is called on a state that is terminal or after a maximum depth is reached,
             * then a heuristic is calculated on the state and the move returned.
             */
            if ((state.isTerminal()) || (depth == maxDepth))
            {
                Move lastMove = new Move(state.getLastMove().getRow(), state.getLastMove().getCol(), state.evaluate());
                return lastMove;
            }
            //The children-moves of the state are calculated
            List<State> children = new List<State>(state.getChildren());
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
                            maxMove.setCol(child.getLastMove().getCol());
                            maxMove.setValue(move.getValue());
                        }
                    }
                    else
                    {
                        maxMove.setCol(child.getLastMove().getCol());
                        maxMove.setValue(move.getValue());
                    }
                }
            }
            return maxMove;
        }



    }
}
