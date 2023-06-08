using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
            int lowPointer = 0;
            int highPointer = height.Length - 1;
            int biggestArea = 0;
            while(lowPointer != highPointer)
            {
                var min = Math.Min(height[lowPointer], height[highPointer]);
                if(min * (highPointer - lowPointer) > biggestArea)
                {
                    biggestArea = min * (highPointer - lowPointer);
                }
                if(height[lowPointer] < height[highPointer])
                    lowPointer++;
                else
                    highPointer--;
            }
            return biggestArea;
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
            int middle = startingBound + (endingBound - startingBound) / 2;
            if (nums[middle] == target)
                return middle;
            else if (startingBound >= endingBound)
                return -1;
            else if (nums[middle] > target)
                return HelperSearch(nums, target, startingBound, middle);
            else
                return HelperSearch(nums, target, middle + 1, endingBound);
        }

        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rowIndex = FindRowIndexHelper(matrix, target, 0, matrix.Length - 1);
            if (rowIndex == -1)
                return false;
            
            return HelperSearchFind(matrix[rowIndex], target, 0, matrix[rowIndex].Length - 1);


        }

        private int FindRowIndexHelper(int[][] matrix, int target, int lowRow, int highRow)
        {
            //breaking condition is that it doesnt belong in that row...

            //if belongs in middle row
            //return middleIndex
            int middle = lowRow + (highRow - lowRow) / 2;
            if (matrix[middle][0] <= target && matrix[middle][matrix[middle].Length - 1] >= target)
                return middle;
            else if (lowRow >= highRow)
                return -1;
            //return searchforlower
            else if (matrix[middle][0] > target)
                return FindRowIndexHelper(matrix, target, lowRow, middle);
            else
                return FindRowIndexHelper(matrix, target, middle + 1, highRow);

            //if higher
            //return search for higher
        }

        public bool HelperSearchFind(int[] nums, int target, int startingBound, int endingBound)
        {
            int middle = startingBound + (endingBound - startingBound) / 2;
            if (nums[middle] == target)
                return true;
            else if (startingBound >= endingBound)
                return false;
            else if (nums[middle] > target)
                return HelperSearchFind(nums, target, startingBound, middle);
            else
                return HelperSearchFind(nums, target, middle + 1, endingBound);
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

        public int[] GetConcatenation(int[] nums)
        {
            int[] ans = new int[nums.Length * 2];
            for(int i = 0; i < nums.Length; i++)
            {
                ans[i] = nums[i];
                ans[i + nums.Length] = nums[i];
            }
            return ans;
        }

        public int CalPoints(string[] operations)
        {
            Stack<int> scores = new Stack<int>();
            foreach(var operation in operations)
            {
                if(operation == "+")
                {
                    var save = scores.Pop();
                    var adding = scores.Peek();
                    scores.Push(save);
                    scores.Push(save + adding);
                }
                else if(operation == "C")
                {
                    scores.Pop();
                }
                else if(operation == "D")
                {
                    scores.Push(scores.Peek() * 2);
                }
                else
                {
                    scores.Push(Int32.Parse(operation));
                }
            }

            int counter = 0;
            while(scores.Count > 0)
            {
                counter += scores.Pop();
            }

            return counter;
        }

        public class MyLinkedList
        {
            public class MyDoublePointNode
            {
                public int val;
                public MyDoublePointNode next;
                public MyDoublePointNode prev;
                public MyDoublePointNode(int val)
                {
                    this.val = val;
                    prev = null;
                    next = null;
                }
            }

            public MyDoublePointNode head;
            public MyDoublePointNode tail;
            public int length;

            public MyLinkedList()
            {
                head = null;
                tail = null;
                length = 0;
            }

            public int Get(int index)
            {
                if (index >= length)
                    return -1;
                var tempNode = head;
                for (int i = 0; i < index; i++)
                {
                    tempNode = tempNode.next;
                }
                return tempNode.val;
            }

            public void AddAtHead(int val)
            {
                if(head == null)
                {
                    length++;
                    var newNode = new MyDoublePointNode(val);
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    var newNode = new MyDoublePointNode(val);
                    head.prev = newNode;
                    newNode.next = head;
                    head = newNode;
                    length++;
                }
            }

            public void AddAtTail(int val)
            {
                if (head == null)
                {
                    length++;
                    var newNode = new MyDoublePointNode(val);
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    var newNode = new MyDoublePointNode(val);
                    tail.next = newNode;
                    newNode.prev = tail;
                    tail = newNode;
                    length++;
                }
            }

            public void AddAtIndex(int index, int val)
            {
                if (index > length)
                    return;
                else if (index == length && length == 0)
                {
                    var newNode1 = new MyDoublePointNode(val);
                    tail = newNode1;
                    head = newNode1;
                    length++;
                    return;
                }
                else if(index == length)
                {
                    var newNode1 = new MyDoublePointNode(val);
                    tail.next = newNode1;
                    newNode1.prev = tail;
                    tail = newNode1;
                    length++;
                    return;
                }
                else
                {
                    var newNode = new MyDoublePointNode(val);
                    var tempNode = head;
                    for (int i = 0; i < index; i++)
                    {
                        tempNode = tempNode.next;
                    }


                    //now need to set the new stuff
                    newNode.prev = tempNode.prev;
                    newNode.next = tempNode;
                    if (tempNode.prev != null)
                    {
                        tempNode.prev.next = newNode;
                    }

                    if (index == 0)
                        head = newNode;
                    length++;
                }
            }

            public void DeleteAtIndex(int index)
            {
                if (index >= length)
                    return;
                if (length == 0)
                    return;

                var tempNode = head;
                for (int i = 0; i < index; i++)
                {
                    tempNode = tempNode.next;
                }
                if (tempNode.prev != null)
                    tempNode.prev.next = tempNode.next;
                if(tempNode.next != null)
                    tempNode.next.prev = tempNode.prev;

                if (index == 0)
                    head = tempNode.next;
                if (index == length - 1)
                    tail = tempNode.prev;
                length--;
            }

            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                bool carrydigit = false;
                ListNode head = new ListNode();
                head.val = l1.val + l2.val;
                if(head.val >=  10)
                {
                    carrydigit = true;
                    head.val = head.val % 10;
                }
                l1 = l1.next;
                l2 = l2.next;

                ListNode temp = head;
                
                while(l1 != null && l2 != null)
                {
                    var newNode = new ListNode();
                    int num = 0;
                    if (carrydigit)
                    {
                        carrydigit = false;
                        num = 1;
                    }

                    
                    if (l1 == null)
                    {
                        newNode.val = l2.val + num;
                        if(newNode.val >= 10)
                        {
                            newNode.val = newNode.val % 10;
                            carrydigit = true;
                        }
                        l1 = l1.next;
                    }
                    else if (l2 == null)
                    {
                        
                    }
                    else
                    {
                        
                        
                    }

                    
                }
                return head;
            }

            public IList<IList<int>> Subsets(int[] nums)
            {
                //add a character, check
                //if not, remove
                //go on to the next for loop for that full set

                //initializing the holder list
                IList<IList<int>> ListOfLists = new List<IList<int>>();
                ListOfLists.Add(new int[0]);
                HelperFunctionSubsets(nums, new List<int>(), ListOfLists);
                return ListOfLists;
            }

            public void HelperFunctionSubsets(int[] nums, List<int> newList, IList<IList<int>> ints)
            {
                if (newList.Count == nums.Length)
                    //return
                    return;
                foreach (var num in nums)
                {
                    var newListInner = new List<int>();
                    foreach (var temp in newList)
                    {
                        newListInner.Add(temp);
                    }
                    //add it to the newList
                    if (newListInner.Contains(num))
                        return;
                    newListInner.Add(num);
                    newListInner.Sort();
                    //check if newList is valid
                    if (!IsValid(ints, newListInner))
                        return;
                    //if not, return
                    //else, add it to the list and make the recursive call again
                    ints.Add(newListInner);
                    HelperFunctionSubsets(nums, newListInner, ints);
                }
            }

            public bool IsValid(IList<IList<int>> ints, List<int> newList)
            {
                foreach (var l in ints)
                {
                    if (l == newList.ToArray())
                    {
                        return false;
                    }
                }
                return true;
            }

            public IList<IList<int>> CombinationSum(int[] candidates, int target)
            {
                
            }


            public void CombinationSumBackTrack(int[] candidates, int target, List<int> innerList, int sum, List<List<int>> result)
            {
                //if the sum is equal to the target, we have found our spot and we cut the line
                if(sum == target)
                {
                    foreach(var tempResult in result)
                    {
                        if (tempResult==innerList)
                            return;
                    }
                    result.Add(innerList);
                    return;
                }

                //if the sum is greater than the target, we went too far, cut the line
                else if(sum > target)
                {
                    return;
                }

                //if the sum is less, we continue going
                else
                {
                    
                }
                return;
            }

            public int Trap(int[] height)
            {

            }

            public IList<IList<int>> Permute(int[] nums)
            {
                List<int[]> ints = new List<int[]>();
                PermuteHelper(nums, ints, new List<int>());
                return ints.ToArray();
            }

            public void PermuteHelper(int[] nums, IList<int[]> ints, List<int> tempList)
            {
                if (tempList.Count == nums.Length)
                {
                    ints.Add(tempList.ToArray());
                }
                else
                {
                    //need to be able to skip certain elements
                    for(int i = 0; i < nums.Length;i++)
                    {
                        if (tempList.Contains(nums[i]))
                            continue;
                        tempList.Add(nums[i]);
                        PermuteHelper(nums, ints, tempList);
                        tempList.Remove(nums[i]);
                    }
                }
                return;
            }


            //edit this to only check certain spots, probably with a visited array? need some way to backtrack appropriately
            public bool Exist(char[][] board, string word)
            {
                return ExistsHelper(board, word, 0, 0, 0);
            }

            public bool ExistsHelper(char[][] board, string word, int wordIndex,int r, int c)
            {
                //base case--acceptance
                if (wordIndex == word.Length)
                    return true;
                //base case--non acceptance
                if (r >= board.Length || c >= board[0].Length || r < 0 || c < 0)
                    return false;
                //backtracking, need to see if the letter at r,c is the same as the one in the word at the word index

                return;
            }

            #region 1-D Dynamic Programming
            public int ClimbStairs(int n)
            {
                int[] intHolder = new int[n];
                if (n == 1)
                    return 1;
                if (n == 2)
                    return 2;
                intHolder[0] = 1;
                intHolder[1] = 2;
                for(int i = 2; i < n; i++)
                {
                    intHolder[i] = intHolder[i - 1] + intHolder[i - 2];
                }
                return intHolder[n - 1];

            }

            public int MinCostClimbingStairs(int[] cost)
            {
                int[] costHolder = new int[cost.Length];
                costHolder[0] = cost[0];
                costHolder[1] = cost[1];

                for(int i = 2; i < cost.Length;i++)
                {
                    costHolder[i] = cost[i] + Math.Min(costHolder[i-1], costHolder[i-2]);
                }

                return Math.Min(costHolder[cost.Length - 1], costHolder[cost.Length - 2]);
            }

            //need to think about this one a bit more
            public int Rob(int[] nums)
            {
                int[] newNums = new int[nums.Length];
                if (nums.Length == 1)
                    return nums[0];
                if (nums.Length == 2)
                    return Math.Max(nums[0], nums[1]);
                newNums[0] = nums[0];
                newNums[1] = nums[1];
                newNums[2] = nums[2] + nums[0];
                for (int i = 3; i < nums.Length; i++)
                {
                    newNums[i] = Math.Max(newNums[i - 2], newNums[i-3]) + nums[i];
                }
                return Math.Max(newNums[nums.Length - 1], newNums[nums.Length - 2]);
            }

            public int CoinChange(int[] coins, int amount)
            {
                return RecursiveCoinChange(coins, amount, 0, 0, 0);
            }

            public int RecursiveCoinChange(int[] coins, int amount, int numChosen, int counter, int index)
            {
                if (index >= coins.Length)
                    return -1;
                if (counter > amount)
                    return -1;
                if (counter == amount)
                    return numChosen;
                var chooseThis = RecursiveCoinChange(coins, amount, numChosen + 1, counter + coins[index], index);
                var chooseOther = RecursiveCoinChange(coins, amount, numChosen, counter + coins[index], index + 1);
                if (chooseThis < 0 || chooseOther < 0)
                    return Math.Max(chooseThis, chooseOther);
                return Math.Min(chooseThis, chooseOther);
            }

            public int Tribonacci(int n)
            {
                if (n == 0 || n == 1)
                    return n;
                if (n == 2)
                    return 1;
                int first = 0;
                int second = 1;
                int third = 1;
                for (int i = 3; i <= n; i++)
                {
                    int temp = first + second + third;
                    first = second;
                    second = third;
                    third = temp;
                }
                return third;
            }


            #endregion

            #region Trees
            public int GoodNodes(TreeNode root)
            {
                return HelperGoodNodes(root, int.MinValue);
            }

            public int HelperGoodNodes(TreeNode root, int max)
            {
                if (root == null)
                    return 0;
                if(root.val >= max)
                {
                    return 1 + HelperGoodNodes(root.left, root.val) + HelperGoodNodes(root.right, root.val);
                }
                else
                    return HelperGoodNodes(root.left, max) + HelperGoodNodes(root.right, max);
            }

            public bool IsSameTree(TreeNode p, TreeNode q)
            {
                return HelperIsSameTree(p, q);
            }

            public bool HelperIsSameTree(TreeNode p, TreeNode q)
            {
                if ((p == null && q != null) || (p != null && q == null))
                    return false;
                else if ((p == null && q == null))
                    return true;
                else if ((p.val != q.val))
                    return false;
                else
                    return (HelperIsSameTree(p.left, q.left) && HelperIsSameTree(p.right, q.right));
            }


            public bool IsValidBST(TreeNode root)
            {
                //need to have some way to track maxes
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while(queue.Count > 0)
                {
                    var temp = queue.Dequeue();
                    if (temp == null || (temp.left == null && temp.right == null))
                        continue;
                    if(temp.left != null && temp.left.val >= temp.val)
                    {
                        return false;
                    }

                    if (temp.right != null && temp.right.val <= temp.val)
                    {
                        return false;
                    }

                    queue.Enqueue(temp.left);
                    queue.Enqueue(temp.right);
                }
                return true;
            }




            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                //make a recursive method that checks if p and q are found in any of its children
                //return the upper most node where this occurs
            }

            public IList<int> RightSideView(TreeNode root)
            {
                //basically, at every depth level, have to check what the right most value at that depth is
                //could use a queue based system
                //queue of tuples with tree node and depth?
                if (root == null)
                    return new int[0];
                List<int> tracker = new List<int> ();
                Queue<Tuple<TreeNode, int>> queue = new Queue<Tuple<TreeNode, int>>();
                queue.Enqueue(new Tuple<TreeNode, int>(root, 0));
                while(queue.Count > 0)
                {
                    var tempNode = queue.Dequeue();
                    if (tracker.Count == tempNode.Item2)
                        tracker.Add(tempNode.Item1.val);
                    else
                    {
                        tracker[tempNode.Item2] = tempNode.Item1.val;
                    }

                    if (tempNode.Item1.left != null)
                        queue.Enqueue(new Tuple<TreeNode, int>(tempNode.Item1.left, tempNode.Item2 + 1));
                    if(tempNode.Item1.right != null)
                        queue.Enqueue(new Tuple<TreeNode, int>(tempNode.Item1.right, tempNode.Item2 + 1));

                    //enqueue the left and then the right
                    //in theory, this should do the right most one?
                }

                return tracker.ToArray();

            }

            public int KthSmallest(TreeNode root, int k)
            {
                //go as far down left as I can go
                //add along the way
                //then add the right of the node
                //take that list and return the k
                List<int> ints = new List<int>();
                KthSmallestHelper(root, k, ints);
                return ints[k-1];

            }

            public void KthSmallestHelper(TreeNode root, int k, List<int> ints)
            {
                if (root == null)
                    return;
                KthSmallestHelper(root.left, k, ints);
                ints.Add(root.val);
                KthSmallestHelper(root.right, k, ints);  
            }

            public bool IsBalanced(TreeNode root)
            {

            }

            public int IsBalancedHelper(TreeNode root)
            {
                //find depth of each subtree
                //if the depth differs by > 1, its no good
            }


            #endregion

            #region Queues
            //https://leetcode.com/problems/implement-stack-using-queues/
            public class MyStack
            {

                public MyStack()
                {

                }

                public void Push(int x)
                {

                }

                public int Pop()
                {

                }

                public int Top()
                {

                }

                public bool Empty()
                {

                }
            }
            #endregion

            #region Heap/Priority Queue
            public class KthLargest
            {
                private class Item
                {
                    int val;
                    int priority;
                }
                public KthLargest(int k, int[] nums)
                {

                }

                public int Add(int val)
                {

                }
            }
            #endregion

            #region Amazon Spring 23 High Frequency
            public int KthFactor(int n, int k)
            {
                List<int> factors = new List<int>();
                int indexInsert = 0;
                for(int i = 1; i <= n; i++)
                {
                    if(n % i == 0 && n/i >= i)
                    {
                        //add i to factors at index
                        factors.Insert(indexInsert, i);
                        if(n/i != i)
                            //add n/i to factors at index + 1
                            factors.Insert(indexInsert + 1, n / i);
                        //add 1 to index
                        indexInsert++;
                    }
                }

                foreach (var factor in factors)
                    Console.WriteLine(factor);

                if (k > factors.Count)
                    return -1;
                else
                    return factors[k-1];
            }

            public int PartitionString(string s)
            {
                //have a hash set
                //work through the string, adding to the hash set when you see character NOT in there
                //when see one in there, add to the partition count and empty the hash set
                HashSet<int> helper = new HashSet<int>();
                int partitionCounter = 0;
                foreach(var character in s)
                {
                    if(helper.Contains(character))
                    {
                        helper = new HashSet<int>();
                        helper.Add(character);
                        partitionCounter++;
                    }
                    else
                    {
                        helper.Add(character);
                    }
                }
                return partitionCounter+1;
            }
            #endregion

            #region Programming Skills Basic Implementation
            public string MergeAlternately(string word1, string word2)
            {
                int word1Pointer = 0;
                int word2Pointer = 0;
                StringBuilder stringBuilder = new StringBuilder();

                while(word1Pointer != word1.Length && word2Pointer != word2.Length)
                {
                    stringBuilder.Append(word1[word1Pointer]);
                    stringBuilder.Append(word2[word2Pointer]);
                    word2Pointer++;
                    word1Pointer++;
                }

                while(word1Pointer != word1.Length)
                {
                    stringBuilder.Append(word1[word1Pointer]);
                    word1Pointer++;
                }

                while (word2Pointer != word2.Length)
                {
                    stringBuilder.Append(word2[word2Pointer]);
                    word2Pointer++;
                }

                return stringBuilder.ToString();
            }

            public int StrStr(string haystack, string needle)
            {
                int needlePointer = 0;

                for(int i = 0; i < haystack.Length; i++)
                {
                    if (haystack[i] == needle[needlePointer])
                    {
                        needlePointer++;
                        if (needlePointer == needle.Length)
                            return i - needlePointer + 1;
                    }
                    else
                    {
                        i = i - needlePointer;
                        needlePointer = 0;
                    }
                }
                return -1;
            }

            public char FindTheDifference(string s, string t)
            {
                int[] sHash = new int[26];
                int[] tHash = new int[26];

                foreach (var character in s)
                {
                    sHash[character - 97]++;
                }

                foreach (var character in t)
                {
                    tHash[character - 97]++;
                }

                for (int i = 0; i < 26; i++)
                {
                    if (sHash[i] != tHash[i])
                        return ((char)(i + 97));
                }

                return 'a';
            }

            public void MoveZeroes(int[] nums)
            {
                int zeroPointer = -1;
                int regularPointer = 0;

                while(regularPointer < nums.Length)
                {
                    if(nums[regularPointer] != 0 && zeroPointer != -1)
                    {
                        //swap them here
                        nums[zeroPointer] = nums[regularPointer];
                        nums[regularPointer] = 0;
                        while (nums[zeroPointer] != 0)
                            zeroPointer++;
                        //set zero pointer = -1 again
                    }

                    if (nums[regularPointer] == 0 && zeroPointer == -1)
                    {
                        zeroPointer = regularPointer;
                    }

                    regularPointer++;
                }

            }

            public int[] PlusOne(int[] digits)
            {
                bool carryOver = false;
                digits[digits.Length - 1]++;
                for (int i = digits.Length - 1; i >= 0; i--)
                {
                    if(carryOver)
                    {
                        digits[i] = digits[i] + 1;
                    }
                    if (digits[i] >= 10)
                    {
                        digits[i] = digits[i] % 10;
                        carryOver = true;
                    }
                    else
                        carryOver = false;
                }

                if(carryOver)
                {
                    int[] newDigits = new int[digits.Length + 1];
                    newDigits[0] = 1;
                    for(int i = 1; i < newDigits.Length; i++)
                    {
                        newDigits[i] = digits[i - 1];
                    }
                    return newDigits;
                }

                return digits;
            }

            public int ArraySign(int[] nums)
            {
                bool positive = true;
                for(int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] < 0)
                        positive = !positive;

                    if (nums[i] == 0)
                    {
                        return 0;
                    }
                }

                if (positive)
                    return 1;
                return -1;
            }

            public bool IsMonotonic(int[] nums)
            {
                bool decreasing = true;
                bool increasing = true;

                for(int i = 1; i < nums.Length; i++)
                {
                    if(nums[i] < nums[i-1])
                        increasing = false;
                    if(nums[i] > nums[i-1])
                        decreasing = false;
                }

                return decreasing || increasing;

            }

            public bool CanMakeArithmeticProgression(int[] arr)
            {
                var newArr = arr.ToList();
                newArr.Sort();
                int diff = newArr[1] - newArr[0];
                for (int i = 2; i < newArr.Count; i++)
                {
                    if (newArr[i] - newArr[i - 1] != diff)
                        return false;
                }
                return true;
            }

            public int LengthOfLastWord(string s)
            {
                int length = 0;
                for(int i = s.Length - 1; i >= 0; i--)
                {
                    if (s[i] != ' ')
                    {
                        //start counting here
                        while(s[i] != ' ' && i >= 0)
                        {
                            i--;
                            length++;
                            if (i < 0)
                                break;
                        }
                        break;
                    }
                }
                return length;
            }

            public bool JudgeCircle(string moves)
            {
                int vertical = 0;
                int horizontal = 0;
                foreach(char move in moves)
                {
                    switch (move)
                    {
                        case 'U':
                            vertical++;
                            break;
                        case 'D':
                            vertical--;
                            break;
                        case 'L':
                            horizontal--;
                            break;
                        case 'R':
                            horizontal++;
                            break;
                        default:
                            break;
                    }
                }
                if (vertical == 0 && horizontal == 0)
                    return true;
                return false;
            }

            public bool IsRobotBounded(string instructions)
            {
                int frontPointer = 0;
                int backPointer = 0;
            }

            public string Tictactoe(int[][] moves)
            {
                //create 3x3 matrix
                char[][] tictactoeboard = new char[3][];
                for(int i = 0; i < 3;i++)
                {
                    tictactoeboard[i] = new char[3];
                }

                //add the moves to the tictac board

                bool xMove = true;
                foreach(var move in moves)
                {
                    if (xMove)
                        tictactoeboard[move[0]][move[1]] = 'A';
                    else
                        tictactoeboard[move[0]][move[1]] = 'B';
                    xMove = !xMove;
                }
                //need to write way to validate the board
                
                //check all rows
                for(int i = 0; i < 3; i++)
                {
                    if (tictactoeboard[i][0] != 0 && tictactoeboard[i][0] == tictactoeboard[i][1] && tictactoeboard[i][1] == tictactoeboard[i][2])
                    {
                        return tictactoeboard[i][0].ToString();
                    }
                }



                //check all columns
                for (int i = 0; i < 3; i++)
                {
                    if (tictactoeboard[0][i] != 0 && tictactoeboard[0][i] == tictactoeboard[1][i] && tictactoeboard[1][i] == tictactoeboard[2][i])
                    {
                        return tictactoeboard[0][i].ToString();
                    }
                }


                //check diagonals
                if (tictactoeboard[0][0] != 0 && tictactoeboard[0][0] == tictactoeboard[1][1] && tictactoeboard[1][1] == tictactoeboard[2][2])
                {
                    return tictactoeboard[0][0].ToString();
                }

                if (tictactoeboard[2][0] != 0 && tictactoeboard[2][0] == tictactoeboard[1][1] && tictactoeboard[1][1] == tictactoeboard[0][2])
                {
                    return tictactoeboard[1][1].ToString();
                }


                //if all full, return draw
                if (moves.Length == 9)
                    return "Draw";

                //else return pending
                return "Pending";
            }

            public int MaximumWealth(int[][] accounts)
            {
                int max = int.MinValue;
                foreach(var account in accounts)
                {
                    int sum = 0;
                    foreach(var value in account)
                    {
                        sum += value;
                    }
                    if(sum > max)
                        max = sum;
                }
                return max;
            }

            public void Rotate(int[] nums, int k)
            {
                
            }

            #endregion

            #region Misc
            public bool CheckRecord(string s)
            {
                bool recordCheck = false;
                int Acounter = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 'A')
                    {
                        Acounter++;
                        if(Acounter == 2)
                        {
                            return false;
                        }
                    }

                    if(s[i] == 'L')
                    {
                        i++;
                        int Lcounter = 1;
                        while(i < s.Length)
                        {
                            if(s[i] == 'L')
                            {
                                Lcounter++;
                                i++;
                                if(Lcounter >= 3)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                i--;
                                break;
                            }
                        }
                    }
                }
                return true;
            }
            #endregion

            public ListNode ReverseKGroup(ListNode head, int k)
            {
                Stack<ListNode> stack = new Stack<ListNode>();
                ListNode prev = null;
                var tempIterator = head;
                while (tempIterator != null)
                {
                    stack.Push(tempIterator);
                    if(stack.Count == k)
                    {
                        tempIterator = tempIterator.next;
                        while(stack.Count != 0)
                        {
                            if (prev == null)
                            {
                                head = stack.Peek();
                            }
                            else
                                prev.next = stack.Peek();
                            prev = stack.Pop();
                        }
                    }
                    else
                        tempIterator = tempIterator.next;      
                }
                return head;
                //have a stack
                //hae one thing as the iterator?
                //when the stack hits k amount in it, empty it, link them up
                    //if the first one, make the head the first popped
                    //if not the first loop, link up the previous to the first popped

            }
        }
    }
    }

