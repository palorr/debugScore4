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
        private Move lastMove;//tha skasei 
        private const int ROWS_NUM = 6;
        private const int COLS_NUM = 7;
        public const int PLAYER_RED = 1;
        public const int PLAYER_YELLOW = 2;
        //
        public State ( int player ) //standard initialization; choose only who plays first
        {
            this.score = 0;
            this.lastColPlayed = 0; 
            this.player = player;
            //
            lastMove = new Move();
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
        public State(State state)  // copy ctor 
        {
            this.score = state.score;
            this.player = state.player;
            this.lastColPlayed = state.lastColPlayed;
            this.lastRowPlayed = state.lastRowPlayed;
            this.Cells = new int[ROWS_NUM, COLS_NUM];
            this.CellsNames = new string[ROWS_NUM, COLS_NUM];
            //
            lastMove = state.lastMove;
            //
            for (int i = 0; i < ROWS_NUM; i++)//row
            {
                for (int j = 0; j < COLS_NUM; j++)//col
                {
                    CellsNames[i, j] = "c" + i + j;
                    this.Cells[i, j] = state.Cells[i, j];
                }
            }
        }
        //
        public State( int[,] Cells , int player ) // ctor MONO GIA THN GET CHILDREN
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
            this.heuristic();
        }
        //Access Modifiers
        public int getPlayer ()
        {
            return player;
        } 
        public void setPlayer(int player)
        {
            this.player = player;
        }
        public Move getLastMove()
        {
            return lastMove;
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
        /// this is the try hard part of the asingment 
        /// </summary>
        public int heuristic()
        {
            this.score += rowScore() + colScore() + mainDScore() +secondaryDScore();
            //Console.WriteLine("row :" + rowScore());
            //Console.WriteLine("column :" + colScore());
            //Console.WriteLine("main diagonal:" + mainDScore());
            //Console.WriteLine("secondary diagonal:" + secondaryDScore());
            //Console.WriteLine(this.score);
            return this.score;
        }
        public int rowScore()
        {
            int horizontalCount = 1;
            int sum = 0; 
            for (int i = 0; i < ROWS_NUM; i++)
            {
                horizontalCount = 1;
                for (int j = 0; j < COLS_NUM - 1; j++)
                {
                    if (Cells[i, j] == Cells[i, j + 1])
                    {
                        horizontalCount++;
                        if (horizontalCount == 3)//if 3 in a row 
                        {
                            if (Cells[i, j + 1] == 1)//player 2 = min 
                                sum += 10;
                            else if (Cells[i, j + 1] == 2)//player 2 = min 
                                sum -= 10;
                        }
                        else if (horizontalCount == 4)//if 4 in a row 
                        {
                            if (Cells[i, j + 1] == 1)
                                sum += 90;
                            else if (Cells[i, j + 1] == 2)
                                sum -= 90;
                        }
                    }
                    else
                        horizontalCount = 1;
                }
            }
            return sum; 
        }
        public int colScore()
        {
            int sum = 0;
            int verticalCount = 1;
            for (int j = 0; j < COLS_NUM; j++)
            {
                verticalCount = 1;
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
                        {
                            if (Cells[i + 1, j] == 1)//player 1 = max 
                                sum += 10;
                            else if (Cells[i + 1, j] == 2)//player 2 = min 
                                sum -= 10;
                        }
                           
                        else if (verticalCount == 4)
                        {
                            if (Cells[i + 1, j] == 1)//player 1 = max 
                                sum += 90;
                            else if (Cells[i + 1, j] == 2)//player 2 = min 
                                sum -= 90;
                        }
                            
                    }
                    else
                        verticalCount = 1;
                }
            }
            return sum; 
        }
        public int mainDScore()
        {
            //Main diagonal
            int mainDiagonalCount = 1;
            int sum = 0;
            int[,] starters = new int[8, 2];
            starters[0, 0] = 3; starters[0, 1] = 0;
            starters[1, 0] = 2; starters[1, 1] = 0;
            starters[2, 0] = 1; starters[2, 1] = 0;
            starters[3, 0] = 0; starters[3, 1] = 0;
            starters[4, 0] = 0; starters[4, 1] = 1;
            starters[5, 0] = 0; starters[5, 1] = 2;
            starters[6, 0] = 0; starters[6, 1] = 3;
            starters[7, 0] = 0; starters[7, 1] = 4;
            int i, j;
            for (int k = 0; k < 8; k++)
            {
                i = starters[k, 0]; j = starters[k, 1];
                mainDiagonalCount = 1;
                do
                {
                    if (Cells[i, j] != 0)   //we only care for non-zero cells
                    {
                        if (Cells[i, j] == Cells[i + 1, j + 1])
                        {
                            mainDiagonalCount++;
                            //set sum
                            if (mainDiagonalCount == 3)
                            {
                                if (Cells[i + 1, j + 1] == 1)//player 1 = max 
                                    sum += 10;
                                else if (Cells[i + 1, j + 1] == 2)//player 2 = min 
                                    sum -= 10;
                            }
                            else if (mainDiagonalCount == 4)
                            {
                                if (Cells[i + 1, j + 1] == 1)//player 1 = max 
                                    sum += 90;
                                else if (Cells[i + 1, j + 1] == 2)//player 2 = min 
                                    sum -= 90;
                            }
                        }
                    }
                    i++; j++;
                    if (i == 5 || j == 6)
                        break;
                } while (true);
            }
            return sum;
        }
        public int mainDScoreV2()
        {
            //Main diagonal
            int mainDiagonalCount = 1;
            int sum = 0;
            int i = 3, j = 0;   //start from Cell[3, 0] : first main diagonal with 3 elements
            while (i != 2 || j != COLS_NUM - 1)      //stop at Cell[2, 6] :  last main diagonal with 3 elements
            {

                if (rachedDiagonalsEnd(i, j))
                {
                    //reset i, j  to diagonal's beginning
                    int i_prev = i;
                    i = (i - j > 0) ? i - j : 0;
                    j = (j - i_prev > 0) ? j - i_prev : 0;
                    //Console.WriteLine("Back to " + i +  "," + j);
                    if (i == 0)  //if at first row...
                    {
                        j++;    //next diagonal begins at the right of the current cell
                    }
                    else
                    {   //when at first col...
                        i--;    //next diagonal begins on top of the current cell
                    }
                    mainDiagonalCount = 1;
                    continue;
                }
                if (Cells[i, j] != 0)   //we only care for non-zero cells
                {
                    if (Cells[i, j] == Cells[i + 1, j + 1])
                    {

                        mainDiagonalCount++;
                        //set sum
                        if (mainDiagonalCount == 3)
                        {
                            if (Cells[i + 1, j + 1] == 1)//player 1 = max 
                                sum += 10;
                            else if (Cells[i + 1, j + 1] == 2)//player 2 = min 
                                sum -= 10;
                        }
                        else if (mainDiagonalCount == 4)
                        {
                            if (Cells[i + 1, j + 1] == 1)//player 1 = max 
                                sum += 90;
                            else if (Cells[i + 1, j + 1] == 2)//player 2 = min 
                                sum -= 90;

                        }

                    }
                }
                i++; j++;
            }
            return sum;
        } 
        public bool rachedDiagonalsEnd(int i, int j)
        {
            return (i == ROWS_NUM - 1) || (j == COLS_NUM - 1);
        }
        public int secondaryDScore()
        {
            //Secondry diagonal
            int secondaryDiagonalCount = 1;
            int sum = 0;
            int[,] starters = new int[8, 2];
            starters[0, 0] = 2; starters[0, 1] = 0;
            starters[1, 0] = 3; starters[1, 1] = 0;
            starters[2, 0] = 4; starters[2, 1] = 0;
            starters[3, 0] = 5; starters[3, 1] = 0;
            starters[4, 0] = 5; starters[4, 1] = 1;
            starters[5, 0] = 5; starters[5, 1] = 2;
            starters[6, 0] = 5; starters[6, 1] = 3;
            starters[7, 0] = 5; starters[7, 1] = 4;
            int i, j;
            for (int k = 0; k < 8; k++)
            {
                i = starters[k, 0]; j = starters[k, 1];
                secondaryDiagonalCount = 1;
                do
                {
                    if (Cells[i, j] != 0)   //we only care for non-zero cells
                    {
                        if (Cells[i, j] == Cells[i - 1, j + 1])
                        {
                            secondaryDiagonalCount++;
                            //set sum
                            if (secondaryDiagonalCount == 3)
                            {
                                if (Cells[i - 1, j + 1] == 1)//player 1 = max 
                                    sum += 10;
                                else if (Cells[i - 1, j + 1] == 2)//player 2 = min 
                                    sum -= 10;
                            }
                            else if (secondaryDiagonalCount == 4)
                            {
                                if (Cells[i - 1, j + 1] == 1)//player 1 = max 
                                    sum += 90;
                                else if (Cells[i - 1, j + 1] == 2)//player 2 = min 
                                    sum -= 90;
                            }
                        }
                    }
                    i--; j++;
                    if (i == 0 || j == 6)
                        break;
                } while (true);
            }
            return sum;
        }
        //
        //
        public void makeMove(int row, int col, int player)
        {
            push(col, player);
            lastMove = new Move(row, col);
            this.lastColPlayed = col;
            this.lastRowPlayed = row;
            this.player = player;
        }
        public bool push(int col, int nextPlayer)
        {
            bool pushDone = false;
            if (Cells[0, col] != 0)
            {
                pushDone = false;
            }
            lastColPlayed = col;
            for (int i = ROWS_NUM - 1; i > 0; i--)
            {
                if (Cells[i, col] == 0)
                {
                    Cells[i, col] = nextPlayer;
                    lastRowPlayed = i;
                    pushDone = true;
                    this.lastMove = new Move(lastRowPlayed, lastColPlayed);  
                    break;
                }
            }
            return pushDone;
        }
        public List<State> getChildren()
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
        
    }
}
