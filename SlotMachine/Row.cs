using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SlotMachine
{
    class Row : Collection<Cell>
    {
        public Row()
        {

        }

        public Row(IList<Cell> cells) : base(cells)
        {
             
        }

        public double CalculateWin(int stake)
        {
            double result = 0;
            int distinctCount = Items.Distinct().Count();

            if (distinctCount <= 2)
            {
                if (distinctCount == 1 || Items.Any(c => c.Symbol == '*'))
                {
                    List<Cell> nonWildcards = Items.Where(c => c.Symbol != '*').ToList();
                    double winCellCoefficient = nonWildcards.First().Coefficient;
                    double totalCoefficient = 0.0;

                    for (int i = 0; i < nonWildcards.Count(); i++)
                    {
                        totalCoefficient += winCellCoefficient;
                    }

                    result = totalCoefficient * stake;
                }
            }

            return result;
        }
    }
}
