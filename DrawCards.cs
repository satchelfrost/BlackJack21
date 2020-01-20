using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack21
{
    class DrawCards
    {
        public static void DrawCardBorder(int xcoor, int ycoor, bool faceUp)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = xcoor * 12;
            int y = ycoor;

            Console.SetCursorPosition(x, y);
            Console.Write(" _________ \n"); // top edge of card

            int CARD_LENGTH = 7;
            for (int i = 0; i < CARD_LENGTH; i++) {
                Console.SetCursorPosition(x, y + 1 + i);
                if (i != CARD_LENGTH - 1 && faceUp)
                    Console.WriteLine("|         |"); // middle section of card
                else if (i != CARD_LENGTH - 1 && !faceUp)
                    Console.WriteLine("|XXXXXXXXX|"); // middle section of flipped card
                else if (i == CARD_LENGTH - 1 && !faceUp)
                    Console.WriteLine("|XXXXXXXXX|"); // bottom edge of card
                else
                    Console.WriteLine("|_________|"); // bottom edge of card
            }
        }
        
        public static void DrawCardSuitValue(Card card, int xcoor, int ycoor)
        {
            string cardSuit = " ";
            int x = xcoor * 12;
            int y = ycoor;

            switch (card.MySuit) {
            case Card.SUIT.HEARTS:
                cardSuit = "\u2665";
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Card.SUIT.DIAMONDS:
                cardSuit = "\u2666";
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Card.SUIT.SPADES:
                cardSuit = "\u2660";
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case Card.SUIT.CLUBS:
                cardSuit = "\u2663";
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            }

            Console.SetCursorPosition(x + 5, y + 3);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 4, y + 5);
            Console.Write(card.MyValue);
        }
    }
}
