using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class PhoneNumberLettersProblem
    {
        // Iterative Solution
        public IList<string> LetterCombinations(string digits)
        {
            IList<string> result = new List<string>();
            Dictionary<char, string> letterDictionary = new Dictionary<char, string>();
            letterDictionary.Add('2', "abc");
            letterDictionary.Add('3', "def");
            letterDictionary.Add('4', "ghi");
            letterDictionary.Add('5', "jkl");
            letterDictionary.Add('6', "mno");
            letterDictionary.Add('7', "pqrs");
            letterDictionary.Add('8', "tuv");
            letterDictionary.Add('9', "wxyz");
            if (digits == "")
            {
                return result;
            }
            letterDictionary.TryGetValue(digits[0], out string val);
            for (int i = 0; i < val.Length; i++)
            {
                result.Add(val[i].ToString());
            }
            for(int i = 1; i < digits.Length; i++)
            {
                IList<string> tempList = new List<string>();
                foreach (string temp in result)
                {
                    letterDictionary.TryGetValue(digits[i], out string value);
                    for(int j = 0; j < value.Length; j++)
                    {
                        tempList.Add(temp + value[j].ToString());
                    }
                    
                }
                result = tempList;
            }
            return result;
        }
        // Recursive Method
        //public IList<string> HelperMethod(string data, IList<string> Result)
        //{

        //}
    }
}
