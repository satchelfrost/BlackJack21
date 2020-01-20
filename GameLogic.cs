using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack21
{
    class GameLogic
    {
                DealCards dc;
        private bool gameOn = true;

        public GameLogic()
        {
            dc = new DealCards();
        }

        public bool playGame()
        {
            dc.Deal();
            if (dc.blackJack)
                gameOn = false;

            while (gameOn) {
                questionPlayer();
                dc.displayCards();
            }

            if (dc.blackJack)
                printMsg("You Won! Black Jack!");
            else if (dc.dealerBust && !dc.playerBust)
                printMsg("You Won! The dealer went over 21.");
            else if (!dc.dealerBust && !dc.playerBust &&
                dc.playerScore > dc.dealerScore)
                printMsg("You Won! You beat the dealer!");
            else if (dc.playerScore == dc.dealerScore && !dc.playerBust)
                printMsg("It's a Tie.");
            else if (dc.playerScore == dc.dealerScore && dc.playerBust)
                printMsg("You Both Lost.");
            else if (dc.playerBust)
                printMsg("You Lost... You went over 21.");
            else
                printMsg("Dealer Won.");

            Console.Write("Do you want to play again? (y/n) ");
            char answer = Convert.ToChar(Console.ReadLine().ToUpper());
            return answer.Equals('Y');
        }

        public void questionPlayer()
        {
            printMsg("Hit or Stand? (h/s)");
            char selection = Convert.ToChar(Console.ReadLine().ToUpper());
            // hit
            if (selection.Equals('H')) {
                gameOn = dc.hit();
                if (!gameOn)
                    dc.revealDealerCard = true;
            }
            // stand
            else if (selection.Equals('S')) {
                gameOn = false;
                dc.revealDealerCard = true; 
            }
            // not available so loop 
            else
                Console.WriteLine("Not a valie option");
        }

        public void printMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 21);
            Console.WriteLine(msg);
        }
    }
}
