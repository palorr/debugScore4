using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debugScore4
{
    class Move
    {
        private int col;
        private int row;
        private int value;

        public Move()
        {
            col = -1;
            value = 0;
        }

        public Move(int row, int col)
        {
            this.col = col;
            this.row = row;
            this.value = -1;
        }

        public Move(int value)
        {
            this.col = -1;
            this.value = value;
        }

        public Move(int row, int col, int value)
        {
            this.col = col;
            this.row = row;
            this.value = value;
        }

        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public int getValue()
        {
            return value;
        }

        public void setRow(int row)
        {
            this.row = row;
        }

        public void setCol(int col)
        {
            this.col = col;
        }

        public void setValue(int value)
        {
            this.value = value;
        }
    }
}
