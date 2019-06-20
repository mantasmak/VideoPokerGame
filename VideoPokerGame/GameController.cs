using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class GameController
    {
        private const int NUM_OF_CARDS_TO_DEAL = 5;

        private ConsoleController ConCont { get; set; }
        private Deck Deck { get; set; }
        private Hand Hand { get; set; }
        private int Balance { get; set; }

        public GameController()
        {
            ConCont = new ConsoleController();
            Hand = new Hand();
            Balance = 10;
        }

        public void StartGame()
        {
            while (Balance > 0)
            {
                Deck = new Deck();
                DealCards();
                ConCont.PrintBalance(Balance);
                ConCont.PrintDealtCards(Hand.DealtCards);
                LoopGame();
            }
        }

        private void LoopGame()
        {
            bool correctInput = false;

            while (correctInput == false)
            {
                ConCont.AskToDiscard();
                String discardAnswer = ConCont.EvaluateDiscardAnswer();

                if (discardAnswer == "Y")
                {
                    correctInput = true;
                    DiscardCards();
                    EvaluateHand();
                }
                else if (discardAnswer == "N")
                {
                    correctInput = true;
                    EvaluateHand();
                }
                else
                {
                    correctInput = false;
                    ConCont.PrintWrongInput();
                }
            }
        }

        private void DiscardCards()
        {
            List<int> discardIndices = new List<int>();
            ConCont.AskHowManyCardsToDiscard();
            int numOfDiscards = ConCont.EvaluateDiscardNumberAnswer();

            if (numOfDiscards <= 0 || numOfDiscards > 5)
            {
                while (numOfDiscards == 0)
                {
                    ConCont.PrintWrongInput();
                    ConCont.AskHowManyCardsToDiscard();
                    numOfDiscards = ConCont.EvaluateDiscardNumberAnswer();
                }
            }

            while (numOfDiscards > 0)
            {
                ConCont.AskWhichCardToDiscard();
                int discardIndex = ConCont.EvaluateDiscardNumberAnswer();
                if (discardIndex > 0 && !discardIndices.Contains(discardIndex - 1))
                {
                    discardIndex--;
                    discardIndices.Add(discardIndex);
                    numOfDiscards--;
                }
                else
                    ConCont.PrintWrongInput();
            }
            var sortedIndices = discardIndices.OrderBy(o => o).ToList();

            for (int i = 0; i < sortedIndices.Count; i++)
            {
                sortedIndices[i] -= i;
                Hand.RemoveCard(sortedIndices[i]);
                Hand.AddCard(Deck.Cards[0]);
                Deck.RemoveCard();
            }

            ConCont.PrintDealtCards(Hand.DealtCards);
        }

        private void EvaluateHand()
        {
            int prize = Hand.Prize;
            Balance += prize;
            ConCont.PrintWinningHand(prize);
        }

        private void DealCards()
        {
            ShuffleDeck();
            Hand.DealtCards = PickCardsFromDeck();
            Balance--;
        }

        private List<Card> PickCardsFromDeck()
        {
            List<Card> pickedCards = new List<Card>();

            for (int i = 0; i < NUM_OF_CARDS_TO_DEAL; i++)
            {
                pickedCards.Add(Deck.Cards[0]);
                Deck.RemoveCard();
            }

            return pickedCards;
        }

        private void ShuffleDeck()
        {
            Deck.Shuffle();
        }
        
    }
}
