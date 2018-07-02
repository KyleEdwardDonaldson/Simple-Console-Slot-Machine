using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public class Cell
    {
        #region Properties

        public char Symbol { get; set; }
        public decimal Coefficient { get; set; }
        public decimal AppearanceChance { get; set; }

        #endregion

        #region Constructors

        public Cell(char symbol, decimal coefficient, decimal appearanceChance)
        {
            Symbol = symbol;
            Coefficient = coefficient;
            AppearanceChance = appearanceChance;
        }

        #endregion
    }
}
