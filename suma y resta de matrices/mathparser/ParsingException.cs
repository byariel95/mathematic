using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionsCSharp.mathparser {

    public class ParsingException : Exception
    {
        private int column = 0;

        public ParsingException(string message) : base(message) { }

        public ParsingException(string message, int column)
            : base(message)
        {
            this.column = column;
        }

        public int Column
        {
            get
            {
                return column;
            }
        }
    }
}
