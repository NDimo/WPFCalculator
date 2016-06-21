using System;
using System.Windows;

namespace Calculator
{
    public static class Calc
    {
        public static decimal Add(decimal value1, decimal value2)
        {
            return value1 + value2;
        }

        public static decimal Add(string value1, string value2)
        {
            return Calc.Add(Calc.ConverToDecimal(value1), Calc.ConverToDecimal(value2));
        }

        public static decimal Subtrack(decimal value1, decimal value2)
        {
            return value1 - value2;
        }
        public static decimal Subtrack(string value1, string value2)
        {
            return Calc.Subtrack(Calc.ConverToDecimal(value1), Calc.ConverToDecimal(value2));
        }
        public static decimal Multiply(decimal value1, decimal value2)
        {
            return value1 * value2;
        }
        public static decimal Multiply(string value1, string value2)
        {
            return Calc.Multiply(Calc.ConverToDecimal(value1), Calc.ConverToDecimal(value2));
        }
        public static decimal Divide(decimal value1, decimal value2)
        {
            decimal result = 0;
            try
            {
                if (value2 != 0)
                {
                    result = value1 / value2;
                }
                else
                {
                    throw new DivideByZeroException();
                }
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        public static decimal Divide(string value1, string value2)
        {
            return Calc.Divide(Calc.ConverToDecimal(value1), Calc.ConverToDecimal(value2));
        }

        public static double Sqrt(string value)
        {
            if (value[0] == '-')
            {
                MessageBox.Show("Invalid input");
                return 0;
            }
            decimal val = Calc.ConverToDecimal(value);
            double valueDouble = Decimal.ToDouble(val);
            return Math.Sqrt(valueDouble);
        }

        public static decimal ConverToDecimal(string value)
        {
            decimal result = 0;
            try
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value[value.Length - 1] == '.')
                    {
                        value.Replace(".", "");
                    }
                    Decimal.TryParse(value, out result);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
    }

}