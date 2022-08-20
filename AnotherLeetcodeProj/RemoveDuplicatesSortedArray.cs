using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class RemoveDuplicatesSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            int index = 0;
            int moving = 1;

            while(moving < nums.Length)
            {
                if (nums[moving] != nums[index])
                {
                    int temp = nums[moving];
                    nums[moving] = nums[index + 1];
                    nums[index + 1] = temp;
                    index++;
                }
                moving++;
            }
            return ++index;
        }
    }
}
