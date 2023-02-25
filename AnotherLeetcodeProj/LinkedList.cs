using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class LinkedList
    {
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
            return HelperRecursiveFunction(head);
        }

        private ListNode HelperRecursiveFunction(ListNode head)
        {
            if(head.next == null)
            {
                return head;
            }
            ListNode node = HelperRecursiveFunction(head.next);
            node.next = head;
            return node;
        }
        

    }


}
