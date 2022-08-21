using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class FirstUniqueCharInString
    {
        public int FirstUniqChar(string s)
        {
            Dictionary<char, int> uniqueCharacters = new Dictionary<char, int>();
            List<int> indexes = new List<int>();
            for(int i = 0; i < s.Length; i++)
            {
                if(uniqueCharacters.ContainsKey(s[i]))
                {
                    indexes.Remove(uniqueCharacters[s[i]]);
                    uniqueCharacters[s[i]] = -1;
                }
                else
                {
                    uniqueCharacters.Add(s[i], i);
                    indexes.Add(i);
                }
            }

            if (indexes.Count == 0)
                return -1;
            return indexes[0];
        }

        public char RepeatedCharacter(string s)
        {
            HashSet<char> uniqueCharacters = new HashSet<char>();
            foreach(char c in s)
            {
                if(!uniqueCharacters.Contains(c))
                {
                    uniqueCharacters.Add(c);
                }
                else
                {
                    return c;
                }
            }
            return ' ';
        }

        public int LongestPalindrome(string s)
        {
            //add every character seen to a hash map
            //add up all the hashes that are mod 2 and then if there is any with mod 1 add +1
            Dictionary<char, int> countCharacters = new Dictionary<char, int>();
            int countLongest = 0;
            bool hasOdd = false;
            foreach (char c in s)
            {
                if(countCharacters.ContainsKey(c))
                {
                    countCharacters[c]++;
                }
                else
                {
                    countCharacters.Add(c, 1);
                }
            }


            foreach(var count in countCharacters.Values)
            {
                if(count >= 2)
                {
                    if (count % 2 == 0)
                    {
                        countLongest += count;
                    }
                    else
                    {
                        countLongest += count - 1;
                        hasOdd = true;
                    }
                }
                else
                {
                    hasOdd = true;
                }
            }


            return hasOdd? ++countLongest : countLongest;
        }
    }
}
