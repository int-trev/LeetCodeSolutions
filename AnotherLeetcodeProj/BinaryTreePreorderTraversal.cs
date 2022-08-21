using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class BinaryTreePreorderTraversal
    {
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
        public IList<int> PreorderTraversal(TreeNode root)
        {
            List<int> ints = new List<int>();
            if (root != null)
                IterativeHelperMethodPreorder(root, ref ints);
            return ints;
        }

        private void RecursiveHelperMethodPreorder(TreeNode root, ref List<int> ints)
        {
            ints.Add(root.val);
            if(root.left != null)
                RecursiveHelperMethodPreorder(root.left, ref ints);
            if(root.right != null)
                RecursiveHelperMethodPreorder(root.right, ref ints);
        }

        //non recursive solution
        private void IterativeHelperMethodPreorder(TreeNode root, ref List<int> ints)
        {
            //Add the root to the list
            List<TreeNode> treeNodes = new List<TreeNode>();
            treeNodes.Add(root);
            ints.Add(root.val);
            //while(the list is not empty)
            //if the node at the end of the list has a left child that has not been visited make the left child null, add it to the end and continue on
            //if the node at the end of the list has a right child that has not been visited, make the right child null, it to the end and continue on
            //if the node has no children, remove it
            while (treeNodes.Count != 0)
            {
                if (treeNodes[treeNodes.Count - 1].left != null)
                {
                    var node = treeNodes[treeNodes.Count - 1].left;
                    ints.Add(node.val);
                    treeNodes[treeNodes.Count - 1].left = null;
                    treeNodes.Add(node);
                }
                else if (treeNodes[treeNodes.Count - 1].right != null)
                {
                    var node = treeNodes[treeNodes.Count - 1].right;
                    ints.Add(node.val);
                    treeNodes[treeNodes.Count - 1].right = null;
                    treeNodes.Add(node);
                }
                else
                {
                    treeNodes.RemoveAt(treeNodes.Count - 1);
                }

            }
        }
    }
}
