using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class RemoveElementEasy
    {
        public int RemoveElement(int[] nums, int val)
        {
            int index = nums.Length - 1;
            int moving = nums.Length - 2;

            if(nums.Length == 0 || (nums.Length == 1 && nums[0] == val))
                return 0;
            while(moving >= 0 && index >= 0)
            {
                if (nums[index] == val)
                {
                    index--;
                    moving--;
                    continue;
                }
                if (nums[moving] == val)
                {
                    nums[moving] = nums[index--];
                }
                moving--;
            }

            return nums[index] == val? index : ++index;
        }
    }
}
