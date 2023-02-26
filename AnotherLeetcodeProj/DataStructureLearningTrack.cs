using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class DataStructureLearningTrack
    {
        #region Day1
        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> duplicateCheck = new HashSet<int>();
            foreach(int num in nums)
            {
                if (duplicateCheck.Contains(num))
                    return true;
                else
                   duplicateCheck.Add(num);
            }
            return false;
        }
        #endregion
    }
}
