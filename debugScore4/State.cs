using System;
using System.Collections.Generic;

namespace debugScore4
{
    class State
    {
        private int player;//player whο has to play 
        public bool terminal;//shows us if this state of the game is terminal
        private int lastcol;//holds the last col
        private int[,] Cells; 
        private string[,] CellsNames;
        private int score;
        private const int ROWS_NUM = 6;
        private const int COLS_NUM = 7;
        private const int PLAYER_RED = 1;
        private const int PLAYER_YELLOW = 2;
        // Constructors 
        public State () //initial state
        {
            this.player = 1; //always player red plays first 
            this.terminal = false; 
            this.score = 0;
            this.lastcol = -1; 
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
        public State(State state)  // copy ctor 
        {
            this.score = state.score;
            this.player = state.player;
            this.terminal = state.terminal;
            this.Cells = new int[ROWS_NUM, COLS_NUM];
            this.CellsNames = new string[ROWS_NUM, COLS_NUM];
            //
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
        public int getPlayer ()//sets the player and returns him // always player 1(player red) plays first
        {
            int sum1 = 0; int sum2 = 0; 
            for (int i = 0; i < ROWS_NUM; i++)
            {
                for (int j = 0; j < COLS_NUM; j++)
                {
                    switch (Cells[i,j])
                    {
                        case 0:
                            continue;
                        case 1:
                            sum1++;
                            continue;
                        case 2:
                            sum2++;
                            continue;
                        default:
                            Console.WriteLine("error in get player");
                            break;
                    }
                }
            }
            if (sum1 == sum2)
                this.player = 1;
            else
                this.player = 2;

            return player;
        } 
        //
        public bool isTerminal()
        {
            this.terminal = false; 
            this.heuristic(); //we call this method only because we need her line scanners to find 4 in a row ,also we update the score var 
            return this.terminal;
        }
        /// All the methods i need for calculating the score of each state
        public void heuristic()//creates a sum of our score
        {
            this.score = 0; 
            this.score += rowScore() + colScore() + mainDScore() +secondaryDScore() +center();
            //Console.WriteLine("row :" + rowScore());
            //Console.WriteLine("column :" + colScore());
            //Console.WriteLine("main diagonal:" + mainDScore());
            //Console.WriteLine("secondary diagonal:" + secondaryDScore());
            //Console.WriteLine(this.score);
        } 
        public int rowScore()//founds 3 in a row or 4 in a row 
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
                            
                            if (Cells[i, j + 1] == 1) {
                                this.terminal = true;
                                sum += 90;
                            }
                            
                            else if (Cells[i, j + 1] == 2)
                            {
                                this.terminal = true;
                                sum -= 90;
                            }
                                
                        }
                    }
                    else
                        horizontalCount = 1;
                }
            }
            return sum; 
        } 
        public int colScore()//founds 3 in a row or 4 in a row
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
                            
                            if (Cells[i + 1, j] == 1)
                            {
                                this.terminal = true;
                                sum += 90;
                            }
                                
                            else if (Cells[i + 1, j] == 2)
                            {
                                this.terminal = true;
                                sum -= 90;
                            } 
                        }
                            
                    }
                    else
                        verticalCount = 1;
                }
            }
            return sum; 
        }
        public int mainDScore()//founds 3 in a row or 4 in a row
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
                                 
                                if (Cells[i + 1, j + 1] == 1)
                                {
                                    this.terminal = true;
                                    sum += 90;
                                }
                                    
                                else if (Cells[i + 1, j + 1] == 2)
                                {
                                    this.terminal = true;
                                    sum -= 90;
                                }
                                    
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
        public int secondaryDScore()//founds 3 in a row or 4 in a row
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
                                
                                if (Cells[i - 1, j + 1] == 1)
                                {
                                    this.terminal = true;
                                    sum += 90;
                                }
                                    
                                else if (Cells[i - 1, j + 1] == 2)
                                {
                                    this.terminal = true;
                                    sum -= 90;
                                }
                                    
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
        public int center()//gives an extra point to moves in the central col 
        {
            int sum = 0;
            for (int i = ROWS_NUM-1; i <= 0; i--)
            {
                if (Cells[i, 3] == 0)
                    break;
                else if (Cells[i, 3] == 1)
                    sum+=1;
                else if (Cells[i, 3] == 2)
                    sum-=1;
            }
            return sum; 
        }
        //the methods i need for children
        public bool push(int col)
        {
            bool pushDone = false;
            if (Cells[0, col] != 0)
            {
                pushDone = false;
            }
            for (int i = ROWS_NUM - 1; i >= 0; i--)
            {
                if (Cells[i, col] == 0)
                {
                    Cells[i, col] = this.getPlayer();
                    this.heuristic(); //each time this state has a change calculate the score 
                    this.lastcol = col; 
                    pushDone = true;
                    break;
                }
            }
            return pushDone;
        }
        public List<State> GetChildren()
        {
            List<State> children = new List<State>();
            
            //
            State child = new State(this);
            if (child.push(0))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(1))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(2))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(3))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(4))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(5))
            {
                children.Add(child);
                child = new State(this);
            }
            if (child.push(6))
            {
                children.Add(child);
                child = new State(this);
            }
            return children;
        }
        //getters 
        public int getScore()
        {
            this.heuristic();
            return this.score; 
        }
        public int getLastCol()
        {
            return this.lastcol;
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
