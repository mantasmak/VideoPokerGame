using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class Hand
    {
        private List<Card> dealtCards;

        public List<Card> DealtCards
        {
            get
            {
                return dealtCards;
            }
            set
            {
                dealtCards = value;
                EvaluateHand();
            }
        }

        public int Prize { get; private set; }

        public Hand()
        {
            Prize = 0;
        }

        public void AddCard(Card card)
        {
            DealtCards.Add(card);
            EvaluateHand();
        }

        public void RemoveCard(int index)
        {
            DealtCards.RemoveAt(index);
        }

        private void EvaluateHand()
        {
            if (CheckIfRoyalFlush())
            {
                Prize = 800;
                return;
            }

            if (CheckIfStraightFlush())
            {
                Prize = 50;
                return;
            }

            if (CheckIfFourOfAKind())
            {
                Prize = 25;
                return;
            }

            if (CheckIfFullHouse())
            {
                Prize = 9;
                return;
            }

            if (CheckIfFlush())
            {
                Prize = 6;
                return;
            }

            if (CheckIfStraight())
            {
                Prize = 4;
                return;
            }

            if (CheckIfThreeOfAKind())
            {
                Prize = 3;
                return;
            }

            if (CheckIfTwoPairs())
            {
                Prize = 2;
                return;
            }

            if (CheckIfJacksOrBatter())
            {
                Prize = 1;
                return;
            }

            Prize = 0;
        }

        private Boolean CheckIfJacksOrBatter()
        {
            var queryAce = DealtCards.Select(n => n)
                                       .Where(n => n.CardRank == 1)
                                       .ToList();
            if (queryAce.Count == 2)
                return true;

            var queryKings = DealtCards.Select(n => n)
                                       .Where(n => n.CardRank == 13)
                                       .ToList();
            if (queryKings.Count == 2)
                return true;

            var queryQueens = DealtCards.Select(n => n)
                                        .Where(n => n.CardRank == 12)
                                        .ToList();
            if (queryQueens.Count == 2)
                return true;

            var queryJacks = DealtCards.Select(n => n)
                                       .Where(n => n.CardRank == 11)
                                       .ToList();
            if (queryJacks.Count == 2)
                return true;

            return false;
        }

        private Boolean CheckIfTwoPairs()
        {
            List<int> listOfCards = new List<int>();
            int numOfPairs = 0;
            foreach (var card in DealtCards)
            {
                if (listOfCards.Contains(card.CardRank))
                    numOfPairs++;
                else
                    listOfCards.Add(card.CardRank);
            }

            return numOfPairs == 2 ? true : false;
        }

        private Boolean CheckIfThreeOfAKind()
        {
            var groupedCards = DealtCards.GroupBy(n => n.CardRank);
            List<int> numOfElements = new List<int>();

            foreach (var cardGroup in groupedCards)
                numOfElements.Add(cardGroup.Count());

            if (numOfElements.Contains(3) && numOfElements.Contains(1))
                return true;

            return false;
        }

        private Boolean CheckIfStraight()
        {
            var sortedCards = DealtCards.OrderBy(o => o.CardRank).ToList();

            int rankToCheck = sortedCards[0].CardRank;

            foreach (Card card in sortedCards)
            {
                if (card.CardRank != rankToCheck)
                    return false;

                rankToCheck++;
            }

            return true;
        }

        private Boolean CheckIfFlush()
        {
            int suitToCheck = DealtCards[0].CardSuit;

            foreach (Card card in DealtCards.Skip(1))
                if (card.CardSuit != suitToCheck)
                    return false;

            return true;
        }

        private Boolean CheckIfFullHouse()
        {
            var groupedCards = DealtCards.GroupBy(n => n.CardRank);
            List<int> numOfElements = new List<int>();

            foreach (var cardGroup in groupedCards)
                numOfElements.Add(cardGroup.Count());

            if (numOfElements.Contains(2) && numOfElements.Contains(3))
                return true;
            else
                return false;
        }

        private Boolean CheckIfFourOfAKind()
        {
            var groupedCards = DealtCards.GroupBy(n => n.CardRank);
            List<int> numOfElements = new List<int>();

            foreach (var cardGroup in groupedCards)
                numOfElements.Add(cardGroup.Count());

            if (numOfElements.Contains(4))
                return true;
            else
                return false;
        }

        private Boolean CheckIfStraightFlush()
        {
            var sortedCards = DealtCards.OrderBy(o => o.CardRank).ToList();

            int suitToCheck = sortedCards[0].CardSuit;
            int rankToCheck = sortedCards[0].CardRank;

            foreach (Card card in sortedCards)
            {
                if (card.CardRank != rankToCheck || card.CardSuit != suitToCheck)
                    return false;

                rankToCheck++;
            }

            return true;
        }

        private Boolean CheckIfRoyalFlush()
        {
            var sortedCards = DealtCards.OrderBy(o => o.CardRank).ToList();

            int suitToCheck = sortedCards[0].CardSuit;
            int rankToCheck = 10;

            if (sortedCards[0].CardRank != 1)
                return false;

            foreach (Card card in sortedCards.Skip(1))
            {
                if (card.CardSuit != suitToCheck || card.CardRank != rankToCheck)
                    return false;

                rankToCheck++;
            }

            return true;
        }
    }
}
