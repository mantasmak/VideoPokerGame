using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPokerGame
{
    class ConsoleController
    {
        public void PrintBalance(int balance)
        {
            Console.WriteLine();
            Console.WriteLine("Balance: " + balance);
        }

        public void PrintDealtCards(List<Card> dealtCards)
        {
            Console.WriteLine();
            Console.WriteLine("YOUR HAND");
            foreach (Card card in dealtCards)
                Console.WriteLine(card);
        }

        public void AskToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to discard any cards?");
            Console.WriteLine("Type Y or N");
            Console.WriteLine();
        }

        public String EvaluateDiscardAnswer()
        {
            String answer = Console.ReadLine().ToUpper();

            return answer;
        }

        public void AskHowManyCardsToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("How many cards would you like to discard?");
            Console.WriteLine("Type a number from 1 to 5");
            Console.WriteLine();
        }

        public int EvaluateDiscardNumberAnswer()
        {
            String answer = Console.ReadLine();
            int parsedAnswer;

            if (Int32.TryParse(answer, out parsedAnswer))
            {
                if (parsedAnswer > 0 && parsedAnswer < 6)
                    return parsedAnswer;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }

        public void AskWhichCardToDiscard()
        {
            Console.WriteLine();
            Console.WriteLine("Which card would you like to discard?");
            Console.WriteLine("Type a number from 1 to 5");
            Console.WriteLine();
        }

        public void PrintWrongInput()
        {
            Console.WriteLine();
            Console.WriteLine("Wrong input!");
            Console.WriteLine();
        }

        public void PrintWinningHand(int prize)
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

    }
}
