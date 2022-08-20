using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LongestSubstringWithoutRepeats tester = new LongestSubstringWithoutRepeats();
            Console.WriteLine(tester.LengthOfLongestSubstring("dvdf"));

            RomanToIntProblem tester2 = new RomanToIntProblem();
            Console.WriteLine(tester2.RomanToInt("MMCCCXCIX"));

            Console.Read();
            
        }
    }
}
