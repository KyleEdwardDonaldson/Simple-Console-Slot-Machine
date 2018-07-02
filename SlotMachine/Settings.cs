using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlotMachine
{
    public class Settings
    {
        #region Fields

        private static readonly Cell Wildcard = new Cell('*', 0, 0.05m);
        public static readonly Settings Default = new Settings(4, 3, new List<Cell>
        {
            new Cell('A', 0.4m, 0.45m),
            new Cell('B', 0.6m, 0.35m),
            new Cell('P', 0.8m, 0.15m)
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
            decimal remainingAppearanceChance = 1;

            if (isWildcardUsed)
            {
                cells.Add(Wildcard);
                remainingAppearanceChance -= (decimal)Wildcard.AppearanceChance;
            }

            Console.Clear();
            Console.WriteLine("Cell Creator");

            do
            {
                char symbol;
                decimal coefficient;
                decimal appearanceChance;

                string symbolResult;
                string coefficientResult;


                string cellString = string.Join("", cells.Select(c => c.Symbol + " "));
                if (cellString.Length > 0)
                    Console.WriteLine("\nExisting Cells: " + cellString);
                else
                    Console.WriteLine("\nNo cells exist");

                Console.WriteLine("\nNew Cell:");
                Console.WriteLine("What do you want your cell symbol to be? (One character, no asterisks)");
                
                do
                {
                    symbolResult = Console.ReadLine();
                } while (!Char.TryParse(symbolResult, out symbol) || cells.Any(c => c.Symbol.Equals(symbol))); //Keeps getting input until input is a char that doesn't exist already

                Console.WriteLine("\nWhat coefficient do you want?");
                do
                {
                    coefficientResult = Console.ReadLine();
                } while (!Decimal.TryParse(coefficientResult, out coefficient));

                bool appearanceChanceIsDecimal;
                Console.WriteLine("\nWhat appearance chance do you want? (0-100% | Remaining Chance: " + (int)(remainingAppearanceChance * 100) + "%)");
                do
                {
                    string appearanceChanceResult = Console.ReadLine()?.Replace("%", "");
                    appearanceChanceIsDecimal = Decimal.TryParse(appearanceChanceResult, out appearanceChance);

                    if (appearanceChanceIsDecimal)
                        appearanceChance = appearanceChance / 100; //Converts percentage given by user into a decimal

                } while (!appearanceChanceIsDecimal || appearanceChance > remainingAppearanceChance); //Keeps getting input until it is a decimal and less than or equal to remaining chance

                remainingAppearanceChance -= appearanceChance; //Updates remaining appearance chance

                cells.Add(new Cell(symbol, coefficient, appearanceChance));

                Console.WriteLine(symbolResult + " cell created!");
            } while (remainingAppearanceChance > 0); //Keep creating cells until you've covered 100% of cases

            Console.WriteLine("\nAll cells created!\n");

            return cells;
        }

        #endregion
    }
}
