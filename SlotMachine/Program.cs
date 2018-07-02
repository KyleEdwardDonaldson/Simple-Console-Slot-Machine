using System;

namespace SlotMachine
{
    class Program
    {
        static void Main()
        {
            string setupOptionInput;
            Console.WriteLine("Welcome to the slot machine!");
            Console.WriteLine("How to play:\n1. Deposit money\n2. Stake money\n3. Rows of symbols will appear, if an entire row is the same symbol you win" +
                              "\n4. Winnings are calculated by the sum of the coefficients of the symbols on the line multipled by the stake" +
                              "\n5. Wildcards (*) can be any symbol but do not add to the sum that is multiplied by the stake to get the winnings");

            Console.WriteLine("\nDefault Cell Details:");
            foreach (Cell cell in Settings.Default.PossibleCells)
            {
                Console.WriteLine("\nSymbol: " + cell.Symbol);
                Console.WriteLine("Coefficient: " + cell.Coefficient);
                Console.WriteLine("Appearance Chance: " + cell.AppearanceChance);
            }

            Console.WriteLine("\nTo begin please enter: \n'0' to start the game with default settings\n'1' for admin mode");
            do
            {
                setupOptionInput = Console.ReadLine();
            } while (setupOptionInput != "0" && setupOptionInput != "1");

            Game game = setupOptionInput == "1" ? new Game(Settings.GetFromUser(), Game.GetDepositFromUser()) : new Game(Settings.Default, Game.GetDepositFromUser());
            game.Start();
        }
    }
}
