using System;
using System.IO;

namespace SPHealthChequeConverter.Helpers
{
    public class ChequeValueConverter : IChequeValueConverter
    {
        private long _amountInCents;
        const string Hundred = "Hundred";
        const string Billion = "Billion";
        const string Million = "Million";
        const string Thousand = "Thousand";

        const long BillionOfCents = 100000000000;
        const int MillionOfCents = 100000000;
        const int ThousandsOfCents = 100000;
        const int HundredsOfCents = 10000;
        const int DollarsOfCents = 100;
        
        public string Convert(double amount)
        {
            if (amount < 0) throw new InvalidDataException("Amount needs to be greater than 0");
            if (amount.Equals(0)) return "Zero";

            _amountInCents = (long)Math.Round((amount * 100), 2);

            var result = string.Empty;                       
            CalculateSection(BillionOfCents, ref result, Billion);
            CalculateSection(MillionOfCents, ref result, Million);
            CalculateSection(ThousandsOfCents, ref result, Thousand);
            CalculateSection(HundredsOfCents, ref result, Hundred);

            result = CalculateDollars(result);

            if (_amountInCents > 0)
                result = (AddAnd(result) + CalcAmount(_amountInCents, "Cent"));

            return result;
        }

        private string CalculateDollars(string result)
        {
            var dollars = _amountInCents / DollarsOfCents;
            if (dollars == 0)
            {
                if (!string.IsNullOrEmpty(result))
                    result += " Dollars";
            }
            else
            {
                result = AddAnd(result) + CalcAmount(dollars, "Dollar");
                _amountInCents %= DollarsOfCents;
            }
            return result;
        }

        private  void CalculateSection(long sectionInCents, ref string result, string sectionName)
        {
            var sectionAmount = _amountInCents / sectionInCents;
            
            if (sectionAmount <= 0) return;
            
            var hundredsOf = sectionAmount / 100;
            if (hundredsOf > 0)
            {                
                result = AddAnd(result) + CalcAmount(hundredsOf, Hundred, false);
                sectionAmount %= 100;
            }

            if (sectionAmount == 0)
                result += " " + sectionName;
            else
                result = AddAnd(result) + CalcAmount(sectionAmount, sectionName, false);

             _amountInCents %= sectionInCents;            
        }


        private static string AddAnd(string result)
        {
            if (result.Length > 0)
                result += " and ";
            return result;
        }

        private static string CalcAmount(long amount, string amountName, bool hasPural = true)
        {                        
            var pural = ((amount > 1 && hasPural) ? "s" : "");

            var  calcAmountName = amountName + pural;

            if (amount < 20)
                return NumberToText(amount) + " " + calcAmountName;
            
            var calcAmount = amount % 10;
                        
            var digit = calcAmountName;
            
            if (calcAmount > 0)
                digit = NumberToText(calcAmount) + " " + calcAmountName;

            return  TensToString(amount / 10) + " " + digit;            
        }

        private static string TensToString(long number)
        {
            switch (number)
            {                
                case 2:
                    return "Twenty";
                case 3:
                    return "Thirty";
                case 4:
                    return "Forty";
                case 5:
                    return "Fifty";
                case 6:
                    return "Sixty";
                case 7:
                    return "Seventy";
                case 8:
                    return "Eighty";
                default:
                    return "Ninety";
            }            
        }
        private static string NumberToText(long number)
        {
            switch (number)
            {                
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                case 10:
                    return "Ten";
                case 11:
                    return "Eleven";
                case 12:
                    return "Twelve";
                case 13:
                    return "Thirteen";
                case 14:
                    return "Fourteen";
                case 15:
                    return "Fifteen";
                case 16:
                    return "Sixteen";
                case 17:
                    return "Seventeen";
                case 18:
                    return "Eighteen";
                default:
                    return "Nineteen";
            }            
        }
    }
}