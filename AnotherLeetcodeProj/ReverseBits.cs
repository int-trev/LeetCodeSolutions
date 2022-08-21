using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class ReverseBits
    {
        //Old implementation, less space but more computation

        //public uint reverseBits(uint n)
        //{
        //    uint reversedNum = 0;
        //    uint startPoint = 1;
        //    uint comparer = 2147483648;
        //    for(int i = 0; i < 32; i++)
        //    {
        //        if((n & comparer) != 0)
        //        {
        //            reversedNum += startPoint;
        //        }
        //        startPoint *= 2;
        //        comparer /= 2;
        //    }
        //    return reversedNum;
        //}

        public uint reverseBits(uint n)
        {
            uint reversedNum = 0;
            uint[] uints = new uint[32];
            uints[0] = 1;

            for(int i = 1; i < 32; i++)
            {
                uints[i] = uints[i - 1] * 2;
            }


            for (int i = 0; i < 32; i++)
            {
                if ((n & uints[31 - i]) != 0)
                {
                    reversedNum += uints[i];
                }
            }
            return reversedNum;
        }
    }
}
