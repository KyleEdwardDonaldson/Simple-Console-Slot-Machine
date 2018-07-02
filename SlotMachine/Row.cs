using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SlotMachine
{
    public class Row : Collection<Cell>
    {
        #region Constructors

        public Row()
        {

        }

        public Row(IList<Cell> cells) : base(cells)
        {
             
        }

        #endregion

        #region Public Methods

        public decimal CalculateWin(int stake) //Calculates the winnings for a row, returns 0 if there is no win
        {
            decimal result = 0;
            int distinctCount = Items.Distinct().Count();

            if (distinctCount <= 2) //If there's more than two distinct items, it can't be a winning line
            {
                if (distinctCount == 1 || Items.Any(c => c.Symbol == '*')) //If there's only one distinct item or one of them is a wildcard then it's a win
                {
                    List<Cell> nonWildcards = Items.Where(c => c.Symbol != '*').ToList();
                    decimal winCellCoefficient = nonWildcards.First().Coefficient;
                    decimal totalCoefficient = 0.0m;

                    for (int i = 0; i < nonWildcards.Count(); i++)
                    {
                        totalCoefficient += winCellCoefficient;
                    }

                    result = totalCoefficient * stake;
                }
            }

            return result;
        }

        #endregion
    }
}
