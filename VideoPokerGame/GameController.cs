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

        private Deck Deck { get; set; }
        private List<Card> DealtCards { get; set; }
        private int Balance { get; set; }

        public GameController()
        {
            Balance = 10;
            StartGame();
        }

        private void StartGame()
        {
            while (Balance > 0)
            {
                List<int> discardIndices = new List<int>();
                Deck = new Deck();
                DealCards();
                PrintBalance();
                PrintDealtCards();
                Boolean correctInput = false;

                while (correctInput == false)
                {
                    AskToDiscard();
                    String discardAnswer = EvaluateDiscardAnswer();

                    if (discardAnswer == "Y")
                    {
                        correctInput = true;
                        AskHowManyCardsToDiscard();
                        int numOfDiscards = EvaluateDiscardNumberAnswer();

                        if (numOfDiscards <= 0 || numOfDiscards > 5)
                        {
                            while (numOfDiscards == 0)
                            {
                                PrintWrongInput();
                                AskHowManyCardsToDiscard();
                                numOfDiscards = EvaluateDiscardNumberAnswer();
                            }
                        }

                        while (numOfDiscards > 0)
                        {
                            AskWhichCardToDiscard();
                            int discardIndex = EvaluateDiscardNumberAnswer();
                            if (discardIndex > 0 && !discardIndices.Contains(discardIndex - 1))
                            {
                                discardIndex--;
                                discardIndices.Add(discardIndex);
                                numOfDiscards--;
                            }
                            else
                                PrintWrongInput();
                        }
                        var sortedIndices = discardIndices.OrderBy(o => o).ToList();

                        for (int i = 0; i < sortedIndices.Count; i++)
                        {
                            sortedIndices[i] -= i;
                            DiscardCard(sortedIndices[i]);
                        }

                        PrintDealtCards();
                        int prize = EvaluateHand();
                        Balance += prize;
                        PrintWinningHand(prize);
                    }
                    else if (discardAnswer == "N")
                    {
                        correctInput = true;
                        int prize = EvaluateHand();
                        Balance += prize;
                        PrintWinningHand(prize);
                    }
                    else
                    {
                        correctInput = false;
                        PrintWrongInput();
                    }
                }
            }
        }

        private int EvaluateHand()
        {
            if (CheckIfRoyalFlush())
                return 800;

            if (CheckIfStraightFlush())
                return 50;

            if (CheckIfFourOfAKind())
                return 25;

            if (CheckIfFullHouse())
                return 9;

            if (CheckIfFlush())
                return 6;

            if (CheckIfStraight())
                return 4;

            if (CheckIfThreeOfAKind())
                return 3;

            if (CheckIfTwoPairs())
                return 2;

            if (CheckIfJacksOrBatter())
                return 1;

            return 0;
        }
        
        private void DiscardCard(int index)
        {
            DealtCards.RemoveAt(index);
            DealtCards.Add(Deck.Cards[0]);
            Deck.RemoveCard();
        }

        private void DealCards()
        {
            ShuffleDeck();
            DealtCards = PickCardsFromDeck();
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

        private void PrintBalance()
        {
            Console.WriteLine();
            Console.WriteLine("Balance: " + Balance);
        }

        private void PrintDealtCards()
        {
            Console.WriteLine("YOUR HAND");
            foreach (Card card in DealtCards)
                Console.WriteLine(card);
        }

        private void AskToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to discard any cards?");
            Console.WriteLine("Type Y or N");
            Console.WriteLine();
        }

        private String EvaluateDiscardAnswer()
        {
            String answer = Console.ReadLine().ToUpper();
            
            return answer;
        }

        private void AskHowManyCardsToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("How many cards would you like to discard?");
            Console.WriteLine("Type a number from 1 to 5");
            Console.WriteLine();
        }

        private int EvaluateDiscardNumberAnswer()
        {
            String answer = Console.ReadLine();
            int parsedAnswer;

            Boolean isNumeric = Int32.TryParse(answer, out parsedAnswer);
            
            if (parsedAnswer > 0 && parsedAnswer < 6 && isNumeric)
                return parsedAnswer;
            else
                return 0;
        }

        private void AskWhichCardToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("Which card would you like to discard?");
            Console.WriteLine("Type a number from 1 to 5");
            Console.WriteLine();
        }

        private void PrintWrongInput()
        {
            Console.WriteLine();
            Console.WriteLine("Wrong input!");
            Console.WriteLine();
        }

        private void PrintWinningHand(int prize)
        {
            Console.WriteLine();
            if (prize == 800)
            {
                Console.WriteLine("Royal Flush! You won 800.");
                return;
            }

            if (prize == 50)
            {
                Console.WriteLine("Straight Flush! You won 50.");
                return;
            }

            if (prize == 25)
            {
                Console.WriteLine("Four of a kind! You won 25.");
                return;
            }

            if (prize == 9)
            {
                Console.WriteLine("Full House! You won 9.");
                return;
            }

            if (prize == 6)
            {
                Console.WriteLine("Flush! You won 6.");
                return;
            }

            if (prize == 4)
            {
                Console.WriteLine("Straight! You won 4.");
                return;
            }

            if (prize == 3)
            {
                Console.WriteLine("Three of a kind! You won 3.");
                return;
            }

            if (prize == 2)
            {
                Console.WriteLine("Two Pair! You won 2.");
                return;
            }

            if (prize == 1)
            {
                Console.WriteLine("Jacks or Better! You won 1.");
                return;
            }

            if (prize == 0)
            {
                Console.WriteLine("You lost!");
                return;
            }
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
