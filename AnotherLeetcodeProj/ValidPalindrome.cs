using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class ValidPalindrome
    {
        //public uint reverseBits(uint n)
        //{
        //    uint reversedNum = 0;
        //    uint comparer = 1;
        //    for (int i = 0; i < 32; i++)
        //    {
        //        if((n & comparer) == 0)
        //        {
        //            reversedNum += comparer;
        //        }
        //        comparer *= 2;
        //    }
        //    return reversedNum;
        //}
        public uint reverseBits(uint n)
        {
            uint reversedNum = 0;
            //the spot it adds is 2^31
            uint startPoint = 1;
            uint comparer = 2147483648;
            for(int i = 0; i < 32; i++)
            {
                if((n & comparer) != 0)
                {
                    reversedNum += startPoint;
                }
                startPoint *= 2;
                comparer /= 2;
            }
            return reversedNum;
        }

    }
}
