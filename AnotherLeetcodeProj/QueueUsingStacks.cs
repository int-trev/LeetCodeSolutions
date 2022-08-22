using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class QueueUsingStacks
    {
        public class MyQueue
        {
            Stack<int> pushStack;
            Stack<int> popStack;

            public MyQueue()
            {
                pushStack = new Stack<int>();
                popStack = new Stack<int>();
            }

            public void Push(int x)
            {
                //take everything from pop, put it  on top of the push stack
                //push to the pop stack
                //push everything from push stack to pop stack
                while (popStack.Count > 0)
                {
                    pushStack.Push(popStack.Pop());
                }
                popStack.Push(x);
                while(pushStack.Count > 0)
                {
                    popStack.Push(pushStack.Pop());
                }
            }

            //In problem description, it states that all calls to pop and peek will be valid
            //not doing error checking because of this
            public int Pop()
            {
                return popStack.Pop();
            }

            public int Peek()
            {
                return popStack.Peek();
            }

            public bool Empty()
            {
                return popStack.Count == 0;
            }
        }
        
    }
}
