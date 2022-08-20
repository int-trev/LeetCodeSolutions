using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class LongestCommonPrefixProblem
    {
        public string LongestCommonPrefix(string[] strs)
        {
            string comparer = strs[0];
            bool breaker = false;
            int i = 0;
            for(i = 0; i < comparer.Length; i++)
            {
                for(int j = 1; j < strs.Length; j++)
                {
                    if (i >= strs[j].Length)
                    {
                        breaker = true;
                        break;
                    } 
                    if (strs[j][i] != comparer[i])
                    {
                        breaker = true;
                        break;
                    }     
                }
                if (breaker)
                    break;
            }
            return comparer.Substring(0, i);
        }

        //public IList<int> disapp(int[] nums)
        //{
        //    IList<int> list = new List<int>();
        //    char[] apple = new char[8];
        //    apple.Cast<String>();
        //    string ban = "apple";
        //    IList<IList<int>> anotherTemp = new List<IList<int>>();
            
        //}
    }
}
