using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public class Cell
    {
        #region Properties

        public char Symbol { get; set; }
        public double Coefficient { get; set; }
        public double AppearanceChance { get; set; }

        #endregion

        #region Constructors

        public Cell(char symbol, double coefficient, double appearanceChance)
        {
            Symbol = symbol;
            Coefficient = coefficient;
            AppearanceChance = appearanceChance;
        }

        #endregion
    }
}
