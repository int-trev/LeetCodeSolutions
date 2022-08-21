using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class ValidPalindrome
    {
        public bool IsPalindrome(string s)
        {
            StringBuilder newStringBuilder = new StringBuilder();

            foreach(var character in s)
            {
                if(Char.IsLetter(character) || Char.IsDigit(character))
                {
                    newStringBuilder.Append(character);
                }
            }

            string newString = newStringBuilder.ToString().ToLower();

            int beginning = 0;
            int end = newString.Length - 1;

            while (beginning < end)
            {
                if (newString[beginning] != newString[end])
                    return false;
                beginning++;
                end--;
            }
            return true;
        }
    }
}
