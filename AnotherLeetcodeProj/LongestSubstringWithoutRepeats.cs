using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class LongestSubstringWithoutRepeats
    {
        public int LengthOfLongestSubstring(string s)
        {
            //do a sliding window
            int leftCounter = 0;
            int rightCounter = 0;
            int longest = 0;
            int currentCount = 0;
            Dictionary<Char, int> myDict = new Dictionary<Char, int>();

            while (leftCounter < s.Length)
            {
                if (rightCounter >= s.Length)
                {
                    currentCount = rightCounter - leftCounter;
                    leftCounter = rightCounter;
                    if (currentCount > longest)
                    {
                        longest = currentCount;
                    }
                    break;
                }
                if (!myDict.TryGetValue(s[rightCounter], out int value))
                {
                    myDict.Add(s[rightCounter], rightCounter);
                    rightCounter++;
                }
                else if(value < leftCounter)
                {
                    myDict[s[rightCounter]] = rightCounter;
                    rightCounter++;
                }
                else
                {
                    myDict.TryGetValue(s[rightCounter], out int val);
                    myDict[s[rightCounter]] = rightCounter;
                    currentCount = rightCounter - leftCounter;
                    leftCounter = val + 1;
                    rightCounter++;
                    if (currentCount > longest)
                    {
                        longest = currentCount;
                    }
                }
            }

            return longest;


            //runtime...O(n) runtime, where n is the amount of of characters in the string.
        }
    }
}
