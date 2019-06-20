using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class WrongCardRankException : Exception
    {
        public WrongCardRankException()
        {

        }

        public WrongCardRankException(int value)
            : base(String.Format("Wrong card rank value: {0}", value))
        {

        }
    }
}
