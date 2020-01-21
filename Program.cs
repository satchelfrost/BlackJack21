using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Console mode? (y/n) ");
            char answer = Convert.ToChar(Console.ReadLine().ToUpper());
            if (answer.Equals('Y')) {
                GameLogic game;
                do {
                    game = new GameLogic();
                } while (game.playGame());
            }
            
            /* The below code is only here because I'm testing out SFML */
            /* But I may or may not continue with this */ 
            //else {
            //    Console.WriteLine("Press ESC to close window");
            //    var window = new SimpleWindow();
            //    window.Run();
            //}

        }
    }
}
