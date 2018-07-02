using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlotMachine
{
    public class Settings
    {
        #region Fields

        private static readonly Cell Wildcard = new Cell('*', 0, 0.05);
        public static readonly Settings Default = new Settings(4, 3, new List<Cell>
        {
            new Cell('A', 0.4, 0.45),
            new Cell('B', 0.6, 0.35),
            new Cell('P', 0.8, 0.15)
        }, true);

        #endregion

        #region Properties

        public int Rows { get; set; }
        public int SymbolsPerRow { get; set; }
        public List<Cell> PossibleCells { get; set; }

        #endregion

        #region Constructors

        public Settings(int rows, int symbolsPerRow, List<Cell> cellOptions, bool includeWildcard)
        {
            Rows = rows;
            SymbolsPerRow = symbolsPerRow;
            PossibleCells = cellOptions;

            if (includeWildcard)
                PossibleCells.Add(Wildcard);
        }

        #endregion

        #region Public Static Methods

        public static Settings GetFromUser()
        {
            int rows;
            int symbolsPerRow;
            bool includeWildcard;

            string rowsInput;
            string symbolsPerRowInput;
            string includeWildcardInput;

            Console.Clear();
            Console.WriteLine("Game Setup");
            Console.WriteLine("\nDo you want to include a wildcard cell? 'Y' or 'N' ");
            do
            {
                includeWildcardInput = Console.ReadLine()?.ToUpper();
            } while (includeWildcardInput != "Y" && includeWildcardInput != "N");
            includeWildcard = includeWildcardInput == "Y";

            List<Cell> cellOptions = GetCellsFromUser(includeWildcard);

            Console.WriteLine("Please enter the number of rows: ");
            do
            {
                rowsInput = Console.ReadLine();
            } while (!Int32.TryParse(rowsInput, out rows));

            Console.WriteLine("\nPlease enter the number of symbols per row: ");
            do
            {
                symbolsPerRowInput = Console.ReadLine();
            } while (!Int32.TryParse(symbolsPerRowInput, out symbolsPerRow));

            Console.WriteLine("\nSetup complete!\n");

            return new Settings(rows, symbolsPerRow, cellOptions, includeWildcard);
        }

        #endregion

        #region Private Static Methods

        private static List<Cell> GetCellsFromUser(bool isWildcardUsed)
        {
            List<Cell> cells = new List<Cell>();
            double remainingAppearanceChance = 1;

            if (isWildcardUsed)
            {
                cells.Add(Wildcard);
                remainingAppearanceChance -= Wildcard.AppearanceChance;
            }

            Console.Clear();
            Console.WriteLine("Cell Creator");

            do
            {
                char symbol;
                double coefficient;
                double appearanceChance;

                string symbolResult;
                string coefficientResult;
                string appearanceChanceResult;


                string cellString = string.Join("", cells.Select(c => c.Symbol + " "));
                if (cellString.Length > 0)
                    Console.WriteLine("\nExisting Cells: " + cellString);
                else
                    Console.WriteLine("\nNo cells exist");

                Console.WriteLine("Remaining % Chance: " + remainingAppearanceChance);
                Console.WriteLine("\nNew Cell:");
                Console.WriteLine("What do you want your cell symbol to be? (One character, no asterisks)");
                
                do
                {
                    symbolResult = Console.ReadLine();
                } while (!Char.TryParse(symbolResult, out symbol) || cells.Any(c => c.Symbol.Equals(symbol)) || symbol == '*');

                Console.WriteLine("\nWhat coefficient do you want?");
                do
                {
                    coefficientResult = Console.ReadLine();
                } while (!Double.TryParse(coefficientResult, out coefficient));

                Console.WriteLine("\nWhat appearance chance do you want? (Decimal form | Remaining % Chance: " + remainingAppearanceChance.ToString() + ")");
                do
                {
                    appearanceChanceResult = Console.ReadLine();
                } while (!Double.TryParse(appearanceChanceResult, out appearanceChance) || appearanceChance > remainingAppearanceChance);
                remainingAppearanceChance -= appearanceChance;

                cells.Add(new Cell(symbol, coefficient, appearanceChance));
                Console.WriteLine(symbolResult + " cell created!");
            } while (remainingAppearanceChance > 0);

            Console.WriteLine("\nAll cells created!\n");

            return cells;
        }

        #endregion
    }
}
