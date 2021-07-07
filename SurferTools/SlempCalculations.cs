using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurferTools
{
    public partial class SpecialCalculations
    {
        public bool CalculateSlemp(string outGrid, string gwt)
        {
            var ontwatering = CalculateOntwatering(gwt);

            // Lutum, CaCO3, OS, pH, GWT


            return false;
        }

        private static int CalculateOntwatering(string gwt)
        {
            return gwt switch
            {
                "I" => 1,
                "II" => 1,
                "III+" => 1,
                "V+" => 1,
                "III-" => 2,
                "V-" => 3,
                "IV" => 3,
                "VI" => 4,
                _ => 5
            };
        }
    }
}
