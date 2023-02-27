using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class DataStructureLearningTrack
    {
        #region Day1
        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> duplicateCheck = new HashSet<int>();
            foreach(int num in nums)
            {
                if (duplicateCheck.Contains(num))
                    return true;
                else
                   duplicateCheck.Add(num);
            }
            return false;
        }

        public int MaxSubArray(int[] nums)
        {
            int sum = int.MinValue;
            int sumCounter = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] < 0)
                {
                    if (sumCounter + nums[i] >= 0)
                        sumCounter += nums[i];
                    else
                        sumCounter = 0;
                }
                else
                {
                    sumCounter += nums[i];
                }
                if(sumCounter > sum)
                    sum = sumCounter;
            }
            return sum;
        }
        #endregion
    }
}
