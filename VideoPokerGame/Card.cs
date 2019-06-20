using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class Card
    {
        private enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
        private enum Suit { Diamonds, Clubs, Spades, Hearts };

        public int CardRank { get; private set; }
        public int CardSuit { get; private set; }

        public Card(int rank, int suit)
        {
            if (rank >= 1 && rank <= 13)
                CardRank = rank;
            else
                throw new WrongCardRankException(rank);

            if (suit >= 0 && suit <= 3)
                CardSuit = suit;
            else
                throw new WrongCardSuitException(suit);
        }

        override public String ToString()
        {
            return Enum.GetName(typeof(Rank), CardRank) + " of " + Enum.GetName(typeof(Suit), CardSuit);
        }

    }
    
}
