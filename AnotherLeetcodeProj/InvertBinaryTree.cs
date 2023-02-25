using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class InvertBinaryTree
    {
        public TreeNode InvertTree(TreeNode root)
        {
            return InvertTreeHelper(root);
        }
        
        public TreeNode InvertTreeHelper(TreeNode root)
        {
            if(root == null)
            {
                return null;
            }
            else if(root.left == null && root.right == null)
            {
                return root;
            }
            else
            {
                var left = root.left;
                var right = root.right;
                root.left = InvertTreeHelper(right);
                root.right = InvertTreeHelper(left);
                return root;
            } 
        }

        public TreeNode OptimizedTree(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while(stack.Count > 0)
            {
                TreeNode node = stack.Peek();
                if(node == null || (node.left == null && node.right == null))
                {
                    stack.Pop();
                }
                else
                {
                    var left = node.left;
                    var right = node.right;
                    node.left = right;
                    node.right = left;
                    stack.Pop();
                    stack.Push(node.left);
                    stack.Push(node.right);
                }
            }
            return root;
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
