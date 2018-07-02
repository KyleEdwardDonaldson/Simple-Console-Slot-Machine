using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;

namespace SlotMachine
{
    public class Game
    {
        #region Properties

        public Settings CurrentSettings { get; set; }
        public double Balance { get; set; }

        #endregion

        #region Constructors

        public Game(Settings settings, double balance)
        {
            CurrentSettings = settings;
            Balance = balance;
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            string playAgainInput;

            Console.WriteLine("\nTime to play!\n");
            do
            {
                this.Play();

                do
                {
                    Console.WriteLine("Would you like to play again? 'Y' or 'N'");
                    playAgainInput = Console.ReadLine()?.ToUpper();
                } while (playAgainInput != "Y" && playAgainInput != "N");
            } while (playAgainInput == "Y");
        }

        #endregion

        #region Private Methods

        private void Play()
        {
            int stake = this.GetStakeFromUser();
            IEnumerable<Row> rows = this.Roll();
            double winnings = 0.0;

            Console.WriteLine("\n\nRESULTS:\n");

            foreach (Row row in rows)
            {
                string rowToDisplay = string.Empty;
                winnings += row.CalculateWin(stake);

                foreach (Cell cell in row)
                {
                    rowToDisplay += cell.Symbol;
                }

                Console.WriteLine(rowToDisplay);
            }

            this.Balance += winnings;

            Console.WriteLine("\nYou have won: " + winnings);
            Console.WriteLine("Current balance is: " + this.Balance);
        }

        private IEnumerable<Row> Roll()
        {
            List<Row> rows = new List<Row>(CurrentSettings.SymbolsPerRow);
            Row row = new Row();

            do
            {
                if (row.Count == CurrentSettings.SymbolsPerRow)
                {
                    rows.Add(new Row(row.ToList()));
                    row.Clear();
                }
                else
                {
                    row.Add(GetRandomCell());
                }

            } while (rows.Count != CurrentSettings.Rows);

            return rows;
        }

        private int GetStakeFromUser()
        {
            string userStakeInput;
            int userStake = 0;

            Console.WriteLine("Please enter the amount of money you'd like to stake:");

            do
            {
                if (userStake > this.Balance)
                    Console.WriteLine("You do not have enough money for this stake, please enter a lower amount:");

                userStakeInput = Console.ReadLine();
            } while (!Int32.TryParse(userStakeInput, out userStake) || userStake > this.Balance);

            return userStake;
        }

        private Cell GetRandomCell()
        {
            Cell selectedCell = null;
            Random random = new Random();
            double randomResult = random.NextDouble();
            double cumulative = 0.0;


            foreach (Cell cell in CurrentSettings.PossibleCells)
            {
                cumulative += cell.AppearanceChance;

                if (randomResult < cumulative)
                {
                    selectedCell = cell;
                    break;
                }

            }

            return selectedCell;
        }

        #endregion

        #region Public Static Methods

        public static int GetDepositFromUser()
        {
            string userDepositInput;
            int deposit;

            Console.WriteLine("\nPlease enter the amount of money you'd like to deposit:");
            do
            {
                userDepositInput = Console.ReadLine();
            } while (!Int32.TryParse(userDepositInput, out deposit));

            return deposit;
        }

        #endregion
    }
}
