using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class HappyNumber
    {
        public bool IsHappy(int n)
        {
            //immediately I think of a hash set
            //save the values of all the ints that we use
            //if we see those ints again, return false
            //if we reach 1 sum, then true.
            HashSet<int> happy = new HashSet<int>();

            happy.Add(n);
            while(true)
            {
                n = SumHelper(n);
                if(n == 1)
                {
                    return true;
                }
                else if(happy.Contains(n))
                {
                    return false;
                }
                else
                {
                    happy.Add(n);
                }
            }
        }

        private int SumHelper(int num)
        {
            int sum = 0;
            while(num > 0)
            {
                int digit = num % 10;
                sum += digit * digit;
                num /= 10;
            }
            return sum;
        }
    }
}
