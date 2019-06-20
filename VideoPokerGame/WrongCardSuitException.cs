using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class WrongCardSuitException : Exception
    {
        public WrongCardSuitException()
        {

        }

        public WrongCardSuitException(int value)
            : base(String.Format("Wrong card suit value: {0}", value))
        {

        }
    }
}
