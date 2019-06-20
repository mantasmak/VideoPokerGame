using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            Cards = new List<Card>();

            for (int i = 1; i <= 13; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    Cards.Add(new Card(i, j));
                }
            }
        }

        public void Shuffle()
        {
            List<Card> randomizedCards = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;
            while (Cards.Count > 0)
            {
                randomIndex = r.Next(0, Cards.Count);
                randomizedCards.Add(Cards[randomIndex]);
                Cards.RemoveAt(randomIndex);
            }

            Cards = randomizedCards;
        }

        public void RemoveCard()
        {
            Cards.RemoveAt(0);
        }
    }
}
