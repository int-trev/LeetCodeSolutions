using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AnotherLeetcodeProj
{
    internal class SubtreeOfAnotherTree
    {
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            return NormalTraversal(root, subRoot);
        }

        public bool NormalTraversal(TreeNode root, TreeNode subRoot)
        {
            if (root == null)
                return false;
            if(root.val == subRoot.val)
                return ValidateNode(root, subRoot) || NormalTraversal(root.right, subRoot) || NormalTraversal(root.left, subRoot);
            return NormalTraversal(root.right, subRoot) || NormalTraversal(root.left, subRoot);
        }

        public bool ValidateNode(TreeNode mainTree, TreeNode compareTree)
        {
            if (mainTree == null && compareTree == null)
                return true;
            if ((mainTree == null && compareTree != null) || (mainTree != null && compareTree == null) || (mainTree.val != compareTree.val))
                return false;
            return ValidateNode(mainTree.left, compareTree.left) && ValidateNode(mainTree.right, compareTree.right);
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
