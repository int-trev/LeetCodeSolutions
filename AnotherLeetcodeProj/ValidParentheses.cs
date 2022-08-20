using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class ValidParentheses
    {
        public bool IsValid(string s)
        {
            //going to use a stack for this problem
            Stack<char> stack = new Stack<char>();
            //go through string, push when open one, pop when close one. If the popped does not correlate to the one we encountered, false
            //if we reach the end without breaking, return true
            foreach (char inputCharacter in s)
            {
                if(inputCharacter == '(' || inputCharacter == '{' || inputCharacter == '[')
                {
                    stack.Push(inputCharacter);
                }
                else if(inputCharacter == ')' && (stack.Count == 0 || stack.Pop() != '('))
                {
                    return false;
                }
                else if (inputCharacter == '}' && (stack.Count == 0 || stack.Pop() != '{'))
                {
                    return false;
                }
                else if (inputCharacter == ']' && (stack.Count == 0 || stack.Pop() != '['))
                {
                    return false;
                }
            }
            if (stack.Count != 0)
                return false;
            return true;
        }
    }
}
