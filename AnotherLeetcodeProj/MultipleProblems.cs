using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class MultipleProblems
    {
        public int FirstUniqChar(string s)
        {
            Dictionary<char, int> uniqueCharacters = new Dictionary<char, int>();
            List<int> indexes = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (uniqueCharacters.ContainsKey(s[i]))
                {
                    indexes.Remove(uniqueCharacters[s[i]]);
                    uniqueCharacters[s[i]] = -1;
                }
                else
                {
                    uniqueCharacters.Add(s[i], i);
                    indexes.Add(i);
                }
            }

            if (indexes.Count == 0)
                return -1;
            return indexes[0];
        }

        public char RepeatedCharacter(string s)
        {
            HashSet<char> uniqueCharacters = new HashSet<char>();
            foreach (char c in s)
            {
                if (!uniqueCharacters.Contains(c))
                {
                    uniqueCharacters.Add(c);
                }
                else
                {
                    return c;
                }
            }
            return ' ';
        }

        public int LongestPalindrome(string s)
        {
            //add every character seen to a hash map
            //add up all the hashes that are mod 2 and then if there is any with mod 1 add +1
            Dictionary<char, int> countCharacters = new Dictionary<char, int>();
            int countLongest = 0;
            bool hasOdd = false;
            foreach (char c in s)
            {
                if (countCharacters.ContainsKey(c))
                {
                    countCharacters[c]++;
                }
                else
                {
                    countCharacters.Add(c, 1);
                }
            }


            foreach (var count in countCharacters.Values)
            {
                if (count >= 2)
                {
                    if (count % 2 == 0)
                    {
                        countLongest += count;
                    }
                    else
                    {
                        countLongest += count - 1;
                        hasOdd = true;
                    }
                }
                else
                {
                    hasOdd = true;
                }
            }


            return hasOdd ? ++countLongest : countLongest;
        }

        // This one determines if there is a cycle
        public bool HasCycle(ListNode head)
        {
            HashSet<ListNode> visitedNodes = new HashSet<ListNode>();
            while(head != null)
            {
                if(visitedNodes.Contains(head))
                {
                    return true;
                }
                visitedNodes.Add(head);
                head = head.next;
            }
            return false;
        }

        public int StrStr(string haystack, string needle)
        {
            //sliding window

            return -1;
        }

        // This one returns the node where the cycle started
        public ListNode DetectCycle(ListNode head)
        {
            HashSet<ListNode> visitedNodes = new HashSet<ListNode>();
            while (head != null)
            {
                if (visitedNodes.Contains(head))
                {
                    return head;
                }
                visitedNodes.Add(head);
                head = head.next;
            }
            return head;
        }

        public int FindDuplicate(int[] nums)
        {
            HashSet<int> duplicates = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (duplicates.Contains(nums[i]))
                {
                    return nums[i];
                }
                duplicates.Add(nums[i]);
            }
            return -1;
        }

        public int SingleNumber(int[] nums)
        {
            HashSet<int> intLeft = new HashSet<int>();
            foreach(int i in nums)
            {
                if(intLeft.Contains(i))
                {
                    intLeft.Remove(i);
                }
                else
                {
                    intLeft.Add(i);
                }
            }
            return intLeft.First();
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }

    }
}
