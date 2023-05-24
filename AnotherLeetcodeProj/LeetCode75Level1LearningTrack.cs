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
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, int> sMap = new Dictionary<char, int>();
            HashSet<char> tSet = new HashSet<char>();
            for(int i = 0; i < s.Length; i++)
            {
                if(sMap.TryGetValue(s[i], out int val))
                {
                    if (t[i] != t[val])
                        return false;
                }
                else
                {
                    if (tSet.Contains(t[i]))
                        return false;
                    sMap.Add(s[i], i);
                    tSet.Add(t[i]);
                }
            }
            return true;
        }

        public bool IsSubsequence(string s, string t)
        {
            if (s.Length > t.Length)
                return false;
            if (s == "")
                return true;
            int sPointer = 0;
            for(int i = 0; i < t.Length; i++)
            {
                if(t[i] == s[sPointer])
                    sPointer++;
                if (sPointer == s.Length)
                    return true;
            }
            return false;
        }
        #endregion

        #region Day 3
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null)
                return list2;
            while(list1.next != null)
            {
                
            }

            
        }




        #endregion
    }
}
