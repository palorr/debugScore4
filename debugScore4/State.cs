using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debugScore4
{
    class State
    {
        private int player;
        private int[,] Cells; //TODO explain used values 0,1,2
        private string[,] CellsNames; //TODO ...
        private int score;
        private int lastColPlayed; //used for smart checking in isTerminal function
        private int lastRowPlayed; //used for smart checking in isTerminal function
        private const int ROWS_NUM = 6;
        private const int COLS_NUM = 7;
        private const int PLAYER_RED = 1;
        private const int PLAYER_YELLOW = 2;
        //
        public State ( int player ) //standard initialization; choose only who plays first
        {
            this.score = 0;
            this.lastColPlayed = -1; 
            this.player = player;
            //
            this.Cells = new int[ROWS_NUM, COLS_NUM];
            this.CellsNames = new string[ROWS_NUM, COLS_NUM];
            for (int i = 0; i < ROWS_NUM; i++)//row
            {
                for (int j = 0; j < COLS_NUM; j++)//col
                {
                    Cells[i, j] = 0;
                    CellsNames[i, j] = "c" + i + j;
                }
            }
        }
        //
        public State( int[,] Cells , int player )
        {
            this.score = 0;
            this.player = player;
            //
            this.Cells = new int[ROWS_NUM, COLS_NUM];
            this.CellsNames = new string[ROWS_NUM, COLS_NUM];
            for (int i = 0; i < ROWS_NUM; i++)//row
            {
                for (int j = 0; j < COLS_NUM; j++)//col
                {
                    CellsNames[i, j] = "c" + i + j;
                    this.Cells[i, j] = Cells[i, j];
                }
            }
        }
        //
        //inserts the token at the given column; iterates bottom-up to avoid unnecessary checks
        public bool push(int col, int test)
        {
            bool pushDone = false;
            if (Cells[0, col] != 0)
            {
                pushDone = false;
            }
            lastColPlayed = col;
            for (int i = ROWS_NUM-1; i > 0; i--)
            {
                if (Cells[i, col] == 0)
                {
                    Cells[i, col] = test;
                    lastRowPlayed = i;
                    pushDone = true;
                    break;
                }
            }
            return pushDone;
        }
        //Access Modifiers; Setters and Getters
        public int getPlayer ()
        {
            return player;
        } 
        //
        public void setPlayer(int player)
        {
            this.player = player;
        }
        //
        public bool isTerminal()
        {
            //Horizontal checking
            int count = 1; int i, j;
            for ( j = 0 ; j < COLS_NUM-1; j++) //check the played row
            {
                if ( Cells[lastRowPlayed, j] == 0 ) continue;   //apparently, we don't care for empty cells
                if ( Cells[lastRowPlayed, j] == Cells[lastRowPlayed, j+1] )
                {
                    count++;
                    if( count == 4 )
                    {
                        return true;
                    } 
                }
                else
                {
                    count = 1;
                } 
            }
            //Vertical checking
            count = 1;
            for ( i = 0; i < ROWS_NUM - 1; i++) //check the played column
            {
                if ( Cells[i, lastColPlayed] == 0 ) continue;   //apparently, we don't care for empty cells  
                if ( Cells[i, lastColPlayed] == Cells[i +1, lastColPlayed] )
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }
            //Secondary diagonal checking
            int coordsSum = lastColPlayed + lastRowPlayed;
            count = 1;
            i = ROWS_NUM -1;
            j = (coordsSum - i >= 0) ? coordsSum - i : 0;
            for ( ; i >= 1 && j < COLS_NUM -1  ; i--, j++) //check the played main diagonal
            {
                if ( Cells[i, j] == 0 ) continue;   //apparently, we don't care for empty cells
                //Console.WriteLine("Comparing cells:" + i + ", " + j);
                if ( Cells[i, j] == Cells[i - 1, j +1] )
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }
            //Main diagonal checking
            int coordsDiff = lastRowPlayed - lastColPlayed;
            count = 1;
            i = (coordsDiff > 0) ? coordsDiff: 0 ;
            j = (-coordsDiff > 0) ? -coordsDiff: 0;
            //Console.WriteLine(i + "," + j);
            for (; i < ROWS_NUM-1 && j < COLS_NUM - 1; i++, j++) //check the played secondary diagonal
            {
                if (Cells[i, j] == 0) continue;   //apparently, we don't care for empty cells
                //Console.WriteLine("Comparing cells:" + i + ", " + j);
                if (Cells[i, j] == Cells[i + 1, j + 1])
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }
            return false;
        }
        /// <summary>
        /// all the methods i need for calculating the score of each state
        /// </summary>
        public void heuristic()
        {
            
        }
        public int rowScore()
        {
            int horizontalCount = 1;
            int sum = 0; 
            for (int i = 0; i < ROWS_NUM; i++)
            {
                for (int j = 0; j < COLS_NUM - 1; j++)
                {
                    if (Cells[i, j] == Cells[i, j + 1])
                    {
                        horizontalCount++;
                        if (horizontalCount == 3)
                            score += 10;
                        else if (horizontalCount == 4)
                            score += 90;    
                    }
                    else
                        horizontalCount = 1;
                }
            }
        }
        public int colScore()
        {
            int verticalCount = 1;
            for (int j = 0; j < COLS_NUM; j++)
            {
                for (int i = 0; i < ROWS_NUM - 1; i++)
                {
                    if (Cells[i, j] == 0)
                    {
                        continue;
                    }
                    if (Cells[i, j] == Cells[i + 1, j])
                    {
                        verticalCount++;
                        if (verticalCount == 3)
                            score += 10;
                        else if (verticalCount == 4)
                            score += 90;
                    }
                    else
                        verticalCount = 1;
                }
            }
        }
        public int mainDScore()
        {
            return 0; 
        }
        public int secondaryDScore()
        {
            return 0; 
        }
        //
        public List<State> GetChildren()
        {
            int nextPlayer;
            List<State> children = new List<State>();
            if (this.player == PLAYER_RED)
                nextPlayer = PLAYER_YELLOW;
            else
                nextPlayer = PLAYER_RED;
            //
            State child = new State(this.Cells, nextPlayer);
            if (child.push(0, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(1, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(2, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(3, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(4, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(5, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            if (child.push(6, nextPlayer))
            {
                children.Add(child);
                child = new State(this.Cells, nextPlayer);
            }
            return children;
        }
        //
        public void toGraph()
        {
            for (int i = 0; i < ROWS_NUM; i++)//row
            {
                for (int j = 0; j < COLS_NUM; j++)//col
                {
                    Console.Write("|");
                    Console.Write( Cells[i, j] ); 
                }
                Console.WriteLine("|");

            }
            Console.WriteLine("");
            Console.WriteLine("**************************");
            Console.WriteLine("");
        }
        //
        public override bool Equals(object obj)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (this.Cells[i, j] != ((State)obj).Cells[i, j])
                        return false;
                }
            }
            return true; 
        }
        //  
        public override int GetHashCode()
        {
            // Which is preferred?

            return base.GetHashCode();

            //return this.FooId.GetHashCode();
        }
        //@Override
        //public int hashCode()
        //{
        //    return this.emptyTileRow + this.emptyTileColumn + this.dimension;
        //}
        //@Override
        ////We override the compareTo function of this class so only the heuristic scores are compared
        //public int compareTo(State s)
        //{
        //    return Double.compare(this.score, s.score);
        //}
    }
}
