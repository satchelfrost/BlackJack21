using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack21
{
    class DealCards : Deck
    {
        private Card[] playerHand;
        private Card[] dealerHand;
        private bool faceUp     = false; // dealer card face up
        private bool dBust      = false; // dealer bust
        private bool pBust      = false; // player bust
        private bool bJack      = false;
        private int dNumCards;           // dealer number of cards
        private int pNumCards;           // player number of cards
        private int pScore;              // player score
        private int dScore;              // dealer score

        public int playerScore {
            get { return pScore;  }
        }

        public int dealerScore {
            get { return dScore;  }
        }

        public bool revealDealerCard {
            set { faceUp = value;  }
        }

        public bool dealerBust {
            get { return dBust; }
        }

        public bool playerBust {
            get { return pBust; }
        }

        public bool blackJack {
            get { return bJack; }
        }

        public DealCards()
        {
            playerHand = new Card[11];
            dealerHand = new Card[11];
            dNumCards  =            2;
            pNumCards  =            2;
            pScore     =            0;
            dScore     =            0;
        }

        public void Deal()
        {
            setUpDeck();
            getHand();
            evaluatePlayer();
            displayCards();
            checkBlackJack(); // you only check on the first deal
        }

        public void getHand()
        {
            // each player gets 11 cards, though in the game it appears
            // as if they have two to start.
            for (int i = 0; i < 11; i++)
                playerHand[i] = getDeck[i];

            for (int i = 11; i < 22; i++)
                dealerHand[i - 11] = getDeck[i];
        }

        public void displayCards()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            drawDealerHand();
            drawPlayerHand();
        }

        public int evaluateHand(Card[] hand, int numCards)
        {
          int score = 0;
            for (int i = 0; i < numCards; i++) {
                if ((int) hand[i].MyValue < 10) {
                    score += (int)hand[i].MyValue;
                } else if (hand[i].MyValue == VALUE.ACE) {
                    int aceValue = (score + 11 > 21) ? 1 : 11;
                    score += aceValue; 
                } else {
                    score += 10;
                }
            }

            // if score is a bust 
            // re-evaluate to check edge case with Ace != 11 
            // needs to be checked if there are multiple Aces
            if (score > 21) {
                score = 0;
                for (int i = 0; i < numCards; i++) {
                    if ((int) hand[i].MyValue < 10)
                        score += (int)hand[i].MyValue;
                    else if (hand[i].MyValue == VALUE.ACE)
                        score += 1;
                    else
                        score += 10;
                }
            }
            return score; 

        }

        public bool hit()
        {
            pNumCards++;
            evaluatePlayer();
            if (pScore > 21)
                pBust = true;
            return pScore < 22; // game on?
        }

        public void evaluatePlayer()
        {
            pScore = evaluateHand(playerHand, pNumCards);
        }

        public void evaluateDealer()
        {
            dScore = evaluateHand(dealerHand, dNumCards);
        }

        public void dealerHitStay()
        {
            evaluateDealer();

            if (pBust) return;

            while (dealerScore < 17) {
                dNumCards++;
                evaluateDealer();
            }

            if (dealerScore > 21)
                dBust = true; 
        }

        public void checkBlackJack()
        {
            if (playerScore == 21)
                bJack = true;
        }

        public void drawDealerHand()
        {
            int x = 0;
            int y = 1;

            // if dealer card is face up, calculate dealer score
            if (faceUp) {
                dealerHitStay();
                Console.WriteLine("Dealers's Hand. Score: {0}", dealerScore);
            }

            else {
                Console.WriteLine("Dealers's Hand");
            }

            for (int i = 0; i < dNumCards; i++) {
                if (i == 0 && !faceUp) {
                    DrawCards.DrawCardBorder(x, y, faceUp);
                }
                else { 
                    DrawCards.DrawCardBorder(x, y, true); // Card always face up
                    DrawCards.DrawCardSuitValue(dealerHand[i], x, y);
                }
                x++;
            }

        }

        public void drawPlayerHand()
        {
            // Set cursor for player
            int x = 0;
            int y = 12;
            Console.SetCursorPosition(x, y - 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Your Hand. Score: {0}", pScore);
            for (int i = 0; i < pNumCards; i++) {
                DrawCards.DrawCardBorder(x, y, true); // card always face up
                DrawCards.DrawCardSuitValue(playerHand[i], x, y);
                x++;
            }
        }
    }
}
