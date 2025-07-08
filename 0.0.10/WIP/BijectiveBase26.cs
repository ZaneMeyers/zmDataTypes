using System;
using System.Text.RegularExpressions;

namespace zmDataTypes.Parsers
{
   public class BijectiveBase26 : IComparable<BijectiveBase26>
   {
      public int Value { get; private set; }

      // Constructor that accepts an integer
      public BijectiveBase26(int number)
      {
         if (number <= 0)
         {
            throw new ArgumentException("Number must be greater than zero.");
         }
         Value = number;
      }

      // Constructor that accepts a string
      public BijectiveBase26(string str)
      {
         str = str.ToUpper();
         if (Regex.IsMatch(str, "^[A-Z]+$"))
         {
            int number = StringToInt(str);
            Value = number;
         }
         else
         {
            throw new ArgumentException("Input must be a valid bijective base-26 string (only A-Z allowed).");
         }
      }

      // Converts the stored integer value to a string
      public override string ToString()
      {
         return IntToString(Value);
      }

      // Converts an integer to a bijective base-26 string
      public static string IntToString(int number)
      {
         string result = string.Empty;
         while (number > 0)
         {
            number--; // Adjust for 1-based index
            int remainder = number % 26;
            result = (char)('A' + remainder) + result;
            number /= 26;
         }
         return result;
      }

      // Converts a bijective base-26 string to an integer
      public static int StringToInt(string str)
      {
         int number = 0;
         int length = str.Length;

         for (int i = 0; i < length; i++)
         {
            int charValue = str[i] - 'A' + 1;
            number += charValue * (int)Math.Pow(26, length - i - 1);
         }
         return number;
      }

      // Implementing IComparable for sorting
      public int CompareTo(BijectiveBase26 other)
      {
         if (other == null) return 1; // Consider nulls as less than any instance
         return this.Value.CompareTo(other.Value);
      }
   }
}
