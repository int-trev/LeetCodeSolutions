using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryTreeInorderTraversal bin = new BinaryTreeInorderTraversal();
            var num2 = new BinaryTreeInorderTraversal.TreeNode(2, null, null);
            var num1 = new BinaryTreeInorderTraversal.TreeNode(1, null, num2);
            var num3 = new BinaryTreeInorderTraversal.TreeNode(3, num1, null);
            bin.InorderTraversal(num3);
            
            
        }
    }
}
