﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnotherLeetcodeProj
{
    internal class MiscProblems
    {
        public bool IsAnagram(string s, string t)
        {
            Dictionary<char, int> countS = new Dictionary<char, int>();
            Dictionary<char, int> countT = new Dictionary<char, int>();
            //adding all the chars from s into the hash map
            foreach(char c in s)
            {
                if(countS.ContainsKey(c))
                {
                    countS[c]++;
                }
                else
                {
                    countS.Add(c, 1);
                }
            }

            //now, need to loop through T to see if the inputs match
            foreach(char c in t)
            {
                if (countT.ContainsKey(c))
                {
                    countT[c]++;
                }
                else
                {
                    countT.Add(c, 1);
                }
            }

            //now, compare both hash maps. If there is a difference, reject. Else, accept. This solution should accept all types of characters
            //as long as they are in the charset

            //this if will be quicker than looking through T to see the remaining because these counts are both O(1)
            if (countS.Count != countT.Count)
                return false;
            foreach(var a in countS)
            {
                if(countT.ContainsKey(a.Key))
                {
                    if (countT[a.Key] != a.Value)
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            return null;
        }

        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> trackingCount = new Dictionary<int, int>();
            foreach(int num in nums)
            {
                if(!trackingCount.ContainsKey(num))
                {
                    trackingCount.Add(num, 1);
                }
                else
                {
                    trackingCount[num]++;
                }
            }
            return trackingCount.OrderByDescending(x => x.Value).Select(x => x.Key).Take(k).ToArray();
        }

        public int[] ProductExceptSelfWithDivision(int[] nums)
        {
            //make the output array
            //loop through nums 1 to n-1 once, and put that mult in new[0]
            //loop through new, and mult new[i-1] * old[i-1] / old[i], put that in new[i] done
            int[] newNums = new int[nums.Length];
            int multWith0s = 1;
            int allmult = 1;
            int count0 = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0 && count0 == 0)
                {
                    count0++;
                }
                else if (nums[i] == 0)
                {
                    allmult = 0;
                    count0++;
                }
                else
                {
                    allmult = allmult * nums[i];
                }
                multWith0s = multWith0s * nums[i];
            }





            for(int i = 0; i < newNums.Length; i++)
            {
                if (nums[i] == 0 && count0 == 1)
                    newNums[i] = allmult;
                else if (nums[i] == 0 && count0 != 1)
                    newNums[i] = multWith0s;
                else if(count0 >= 1)
                    newNums[i] = multWith0s;
                else
                    newNums[i] = allmult / nums[i];
            }

            return newNums;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            //work from the front and calculate the prefixes
            //work from the back and calculate the suffixes
            //go through the array and multiply the prefix and then the suffix
            //add that result to a new array
            //done

            int[] newNums = new int[nums.Length];
            int[] prefixes = new int[nums.Length];
            int[] suffixes = new int[nums.Length];
            prefixes[0] = 1;
            for(int i = 1; i < nums.Length; i++)
            {
                prefixes[i] = prefixes[i - 1] * nums[i-1];
            }
            suffixes[nums.Length - 1] = 1;
            for(int i = nums.Length - 2; i >= 0; i--)
            {
                suffixes[i] = suffixes[i + 1] * nums[i + 1];
            }

            for(int i = 0; i < nums.Length; i++)
            {
                newNums[i] = prefixes[i] * suffixes[i];
            }

            return newNums;
        }

        public bool IsValidSudoku(char[][] board)
        {
            //make an array of 9 hashmaps for the rows
            //make an array of 9 hashmaps for the columns
            //make an array of 9 hashmaps for the 3x3 grids

            bool[][] rowHashing = new bool[9][];
            bool[][] colHashing = new bool[9][];
            bool[][][] gridHashing = new bool[3][][];
            for(int i = 0; i < 9; i++)
            {
                rowHashing[i] = new bool[9] { false, false, false, false, false, false, false, false, false };
                colHashing[i] = new bool[9] { false, false, false, false, false, false, false, false, false };
            }
            for(int i = 0; i < 3; i++)
            {
                gridHashing[i] = new bool[3][];
                for(int j = 0; j < 3; j++)
                {
                    gridHashing[i][j] = new bool[9] { false, false, false, false, false, false, false, false, false };
                }
            }

            //this makes sure that all the rows themselves are valid, but now the columns need to be checked
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (board[i][j] != '.')
                    {
                        //if the hashmap in row[i] contains the val, return false
                        if (rowHashing[i][board[i][j] - 49])
                            return false;
                        //if the hashmap in column[i] contains the val, return false
                        if (colHashing[j][board[i][j] - 49])
                            return false;
                        //if the hashmap of the corresponding 3x3 grid contains the val, return false
                        if (gridHashing[mappingindex(i)][mappingindex(j)][board[i][j] - 49])
                            return false;
                        rowHashing[i][board[i][j] - 49] = true;
                        colHashing[j][board[i][j] - 49] = true;
                        gridHashing[mappingindex(i)][mappingindex(j)][board[i][j] - 49] = true;
                    }
                }
            }
            return true;
        }

        public int LongestConsecutive(int[] nums)
        {
            if (nums.Length == 0)
                return 0;
            Dictionary<int, Tuple<int, int>> beginningMap = new Dictionary<int, Tuple<int, int>>();
            Dictionary<int, Tuple<int, int>> endingMap = new Dictionary<int, Tuple<int, int>>();
            //keep track of the highest and lowest of each sequence. If a number gets added that is part of 2 different sequences, merge the two
            //how would I keep track of both sides in 0(1) time?
            //have a begining hash map and an endind hash map?
            //have both map from <int> to <tuple>, where tuple is the sequence?
            //when I change a sequence, add its key and change the sequence up?
            foreach(var num in nums)
            {
                if(endingMap.ContainsKey(num) || beginningMap.ContainsKey(num))
                {
                    continue;
                }
                else if(endingMap.ContainsKey(num - 1) && beginningMap.ContainsKey(num + 1))
                {
                    //if this occurs, take the key from ending, make that the start, change the tuple at ending key and beginning key to the new tuple of <endingmapindex, beginningmapvalue.end>
                    var newTup = new Tuple<int, int>(endingMap[num-1].Item1, beginningMap[num+1].Item2);
                    beginningMap[endingMap[num - 1].Item1] = newTup;
                    endingMap[beginningMap[num + 1].Item2] = newTup;
                    
                    
                }
                else if(endingMap.ContainsKey(num - 1))
                {
                    //if it just has something that needs to be added, add a new ending key with the new tuple being <endinMap[num-1].first, num>
                    endingMap.Add(num, new Tuple<int, int>(endingMap[num - 1].Item1, num));
                    beginningMap[endingMap[num - 1].Item1] = new Tuple<int, int>(endingMap[num - 1].Item1, num);
                }
                else if(beginningMap.ContainsKey(num + 1))
                {
                    //if it just has something that needs to be added, add a new ending key with the new tuple being <num, beginningmap[num + 1].ending>
                    beginningMap.Add(num, new Tuple<int, int>(num, beginningMap[num + 1].Item2));
                    endingMap[beginningMap[num + 1].Item2] = new Tuple<int, int>(num, beginningMap[num + 1].Item2);
                }
                else
                {
                    //add to both the start and the ending maps the num, and then the tuple being <num, num>
                    endingMap.Add(num, new Tuple<int, int>(num, num));
                    beginningMap.Add(num, new Tuple<int, int>(num, num));

                }
            }

            int countMax = 0;
            foreach(var tuple in beginningMap.Values)
            {
                if (Math.Abs(tuple.Item2 - tuple.Item1) > countMax)
                    countMax = tuple.Item2 - tuple.Item1;
            }

            foreach (var tuple in endingMap.Values)
            {
                if (Math.Abs(tuple.Item2 - tuple.Item1) > countMax)
                    countMax = tuple.Item2 - tuple.Item1;
            }


            return countMax + 1;
        }

        public int[] TwoSum(int[] numbers, int target)
        {
            int[] answer = new int[2];
            int beginning = 0;
            int ending = numbers.Length - 1;
            while (numbers[beginning] + numbers[ending] != target)
            {
                if (numbers[beginning] + numbers[ending] < target)
                {
                    beginning++;
                }
                else
                    ending--;
            }
            answer[0] = beginning + 1;
            answer[1] = ending + 1;
            return answer;
        }



        public int mappingindex(int input)
        {
            if (input < 3)
                return 0;
            if (input < 6)
                return 1;
            return 2;
        }

        public class MinStack
        {
            //would need some way to implement a normal stack
            //would need some way to implement the tracking of minimums

            List<int> ints1;
            
            int minimum = int.MaxValue;

            public MinStack()
            {
               ints1 = new List<int>();
            }

            public void Push(int val)
            {
                ints1.Add(val);
                //when you push a new one, need to see if it beats the min
                if(val < minimum)
                {
                    minimum = val;
                }
            }

            public void Pop()
            {
                ints1.RemoveAt(ints1.Count - 1);
                //when you pop, need to see what is the new min
                if(ints1.Count == 0)
                {
                    minimum = int.MaxValue;
                }
            }

            public int Top()
            {
                return ints1[ints1.Count - 1];
            }

            public int GetMin()
            {
                //returns the min
                return minimum;
            }
        }

        public int MaxArea(int[] height)
        {
            
        }

        public int EvalRPN(string[] tokens)
        {
            Stack<int> ints = new Stack<int>();
            foreach(var token in tokens)
            {
                switch(token)
                {
                    case "+":
                        var first = ints.Pop();
                        var second = ints.Pop();
                        ints.Push(first + second);
                        break;
                    case "-":
                        first = ints.Pop();
                        second = ints.Pop();
                        ints.Push(second - first);
                        break;
                    case "*":
                        first = ints.Pop();
                        second = ints.Pop();
                        ints.Push(second * first);
                        break;
                    case "/":
                        first = ints.Pop();
                        second = ints.Pop();
                        ints.Push(second / first);
                        break;
                    default:
                        ints.Push(Int16.Parse(token));
                        break;

                }
            }
            return ints.Pop();
        }

        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] ints = new int[temperatures.Length];
            Stack<int> temps = new Stack<int>();
            Stack<int> indexEntered = new Stack<int>();
            for(int i = 0; i < temperatures.Length; i++)
            {
                while(temps.Count > 0 && temps.Peek() < temperatures[i])
                {
                    int popped = temps.Pop();
                    int indexLeft = indexEntered.Pop();
                    ints[indexLeft] = i - indexLeft;
                    //pop and add with the counter
                }
                temps.Push(temperatures[i]);
                indexEntered.Push(i);
            }

            while (temps.Count > 0)
            {
                var indexLeft = indexEntered.Pop();
                ints[indexLeft] = 0;
                temps.Pop();
            }

            return ints;
        }

        public int Search(int[] nums, int target)
        {
            return HelperSearch(nums, target, 0, nums.Length - 1);
        }

        public int HelperSearch(int[] nums, int target, int startingBound, int endingBound)
        {
            int middle = (endingBound - startingBound) / 2;
            if (nums[middle] == target)
                return target;
            else if (startingBound == endingBound)
                return -1;
            else if (nums[middle] > target)
                return HelperSearch(nums, target, startingBound, middle-1);
            else
                return HelperSearch(nums, target, middle+1, endingBound);
        }

        public IList<string> GenerateParenthesis(int n)
        {
            
        }

        public IList<string> HelperFunctionGenerateParenthesis(int n, Stack<char> chars, StringBuilder input)
        {
            if(n == 0)
            {
                //pop all the chars off the stack
                while()
                //add ')' to the string builder for each char that was popped off
                //return the string builder
            }
            else
            {
                //two cases here, can either pop one off or add another to the stack

                //if there is a poppable
                if(chars.Count != 0)
                {
                    //pop a char off the stack
                    //add ')' to the string builder
                    //add the return to the list of strings with the n, the stack, and the string builder with added stuff
                }

               //push an open onto the stack
               //n--
               //add the return to the list of string with the new n, the new stack, and the string builder


            }
        }

        public int MaxProfit(int[] prices)
        {
            if (prices.Length == 1)
                return 0;
            int bestOutput = 0;
            int minIndex = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                //this line checks if our current spot is better than the previous best with the LOWEST NUMBER POSSIBLE
                if (prices[i] - prices[minIndex] > bestOutput)
                {
                    bestOutput = prices[i] - prices[minIndex];
                }
                //this line replaces the lowest number possible after we do that last calc. This works as well because it wont fire on the LAST INDEX
                //if the min happens on the last index it wont matter because there is no sale date later than the last
                if (prices[i] < prices[minIndex])
                {
                    minIndex = i;
                }
            }
            return bestOutput;
        }

        public int LengthOfLongestSubstring(string s)
        {
            HashSet<char> matches = new HashSet<char>();
            int longestWindow = 0;
            int startingPoint = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(matches.Contains(s[i]))
                {
                    //remove from the hashset and add to startingPoint until we reach where the last character was
                    while (s[startingPoint] != s[i])
                    {
                        matches.Remove(s[startingPoint]);
                        startingPoint++;
                    }
                    startingPoint++;
                }
                else
                {
                    matches.Add(s[i]);
                }
                if (i - startingPoint + 1 > longestWindow)
                    longestWindow = i - startingPoint + 1;
            }
            return longestWindow;
        }

        public int CharacterReplacement(string s, int k)
        {
            //a window has to contain n amount of one character, and k amount of different characters
            //im considering a hash map that holds a char and an int, an int with the amount of occurances
            
        }

        public bool CheckInclusion(string s1, string s2)
        {
            //easy implementation
            //create a hash map for s1
            Dictionary<char, int> s1Hash = new Dictionary<char, int>();
            Dictionary<char, int> s2Hash = new Dictionary<char, int>();
            foreach(char val in s1)
            {
                if (!s1Hash.ContainsKey(val))
                    s1Hash.Add(val, 1);
                else
                    s1Hash[val]++;
            }


            //loop through s2
            int beginningWindow = 0;
            for(int i = 0; i < s2.Length; i++)
            {
                //this means that we are not in a streak at all
                if (!s1Hash.ContainsKey(s2[i]))
                {
                    while (beginningWindow != i)
                    {
                        s2Hash[s2[beginningWindow]]--;
                        if (s2Hash[s2[beginningWindow]] < 0)
                            s2Hash.Remove(s2[beginningWindow]);
                        beginningWindow++;
                    }
                    beginningWindow++;
                }
                //this means that the value was found in there
                else
                {
                    //if the one we are currently on, when incremented by + 1, will be more than we need for that letter
                    if (s2Hash.ContainsKey(s2[i]) && s2Hash[s2[i]] + 1 > s1Hash[s2[i]])
                    {
                        //go to the first occurance of the s[2] in the window
                        while (s2[beginningWindow] != s2[i])
                        {
                            s2Hash[s2[beginningWindow]]--;
                            if (s2Hash[s2[beginningWindow]] < 0)
                                s2Hash.Remove(s2[beginningWindow]);
                            beginningWindow++;
                        }
                    }
                    else if (s2Hash.ContainsKey(s2[i]) && beginningWindow - i + 1 == s1.Length)
                    {
                        return true;
                    }
                    else if (s2Hash.ContainsKey(s2[i]))
                    {
                        s2Hash[s2[i]]++;
                    }
                    else
                    {
                        s2Hash.Add(s2[i], 1);
                    }
                }
            }

            return false;
            //if hash does not contain the val, move window to current pointer and start over 
            //if the hash DOES and we go over the alloted value, go to the last occurance of said value and go from there
            //if reach the length of s1 with no hiccups, return true

            //return false
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            //first need to make a list to hold the list of strings
            //next, I need to write a loop to loop through strs
            //in this loop, it needs to compare strs to each of the different cases in the list of lists first index
            List<List<string>> returnedList = new List<List<string>>();
            List<Dictionary<char, int>> numOccurances = new List<Dictionary<char, int>>();
            foreach(var str in strs)
            {
                Dictionary<char, int> numOccuranceStr = new Dictionary<char, int>();
                foreach(char c in str)
                {
                    if(numOccuranceStr.ContainsKey(c))
                    {
                        numOccuranceStr[c]++;
                    }
                    else
                        numOccuranceStr.Add(c, 1);
                }
                for(int i = 0; i < returnedList.Count; i++)
                {
                    if()
                    foreach(var keypair in numOccurances[i])
                    {
                        
                    }
                }
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return null;
            Stack<ListNode> newStack = new Stack<ListNode>();
            while(head != null)
            {
                newStack.Push(head);
                head = head.next;
            }

            ListNode newHead = newStack.Pop();
            ListNode listNode = newHead;
            while (newStack.Count > 0)
            {
                listNode.next = newStack.Pop();
                listNode = listNode.next;
            }
            listNode.next = null;
            return newHead;
        }

        public void ReorderList(ListNode head)
        {
            Stack<ListNode> newStack = new Stack<ListNode>();
            ListNode node = head;
            while(node != null)
            {
                newStack.Push(node);
                node = node.next;
            }
            node = head;

            var counter = newStack.Count;
            for (int i = 0; i < counter / 2; i++)
            {
                var tempNode = newStack.Pop();
                var othertempNode = node.next;
                node.next = tempNode;
                tempNode.next = othertempNode;
                node = node.next;
            }
            node.next = null;
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode trackingNode = head;
            for(int i = 0; i < n; i++)
            {
                trackingNode = trackingNode.next;
            }

            if (trackingNode == null)
                return head.next;

            ListNode newTrackingNode = head;
            while(trackingNode != null)
            {
                trackingNode = trackingNode.next;
                if(trackingNode == null)
                {
                    newTrackingNode.next = newTrackingNode.next.next;
                    break;
                }
                newTrackingNode = newTrackingNode.next;
            }


            return head;
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {

        }

        public int MaxDepth(TreeNode root)
        {
            return MaxDepthHelper(root, 0);
        }

        public int MaxDepthHelper(TreeNode root, int counter)
        {
            if (root == null)
                return counter;
            else return Math.Max(MaxDepthHelper(root.left, counter + 1), MaxDepthHelper(root.right, counter + 1));
        }

        public int DiameterOfBinaryTree(TreeNode root)
        {
            //I have a feeling that the diameter is really just an addition of the max depth of the left and the right of the root
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}
