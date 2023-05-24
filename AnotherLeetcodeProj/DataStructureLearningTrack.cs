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
        #region Day 2
        public int[] TwoSum(int[] nums, int target)
        {
            int[] indexes = new int[2];
            Dictionary<int, int> IntAndIndex = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length; i++)
            {
                if(IntAndIndex.ContainsKey(target - nums[i]))
                {
                    indexes[0] = IntAndIndex[target - nums[i]];
                    indexes[1] = i;
                    break;
                }
                else
                {
                    if(!IntAndIndex.ContainsKey(nums[i]))
                        IntAndIndex.Add(nums[i], i);
                }
            }
            return indexes;
        }

        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int pointer1 = 0;
            int pointer2 = 0;
            
        }
        #endregion
    }
}
