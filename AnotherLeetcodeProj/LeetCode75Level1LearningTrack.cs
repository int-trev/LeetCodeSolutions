using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class LeetCode75Level1LearningTrack
    {
        #region Day 1
        public int[] RunningSum(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] = nums[i - 1] + nums[i];
            }
            return nums;
        }

        public int PivotIndex(int[] nums)
        {
            int leftSum = 0;
            int rightSum = nums.Sum();
            for (int i = 0; i < nums.Length; i++)
            {
                rightSum -= nums[i];
                if (leftSum == rightSum)
                    return i;
                else
                    leftSum += nums[i];
            }
            return -1;
        }
        #endregion
        #region Day 2
        #endregion
    }
}
