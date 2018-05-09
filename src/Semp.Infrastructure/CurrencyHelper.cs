using System.Collections.Generic;
using System.Globalization;

namespace Semp.Infrastructure
{
    public static class CurrencyHelper
    {
        public static bool IsZeroDecimalCurrencies()
        {
            var zeroDecimalCurrencies = new List<string> { "BIF", "DJF", "JPY", "KRW", "PYG", "VND", "XAF", "XPF", "CLP", "GNF", "KMF", "MGA", "RWF", "VUV", "XOF" };
            var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            if (zeroDecimalCurrencies.Contains(regionInfo.ISOCurrencySymbol))
            {
                return true;
            }

            return false;
        }
    }
}
