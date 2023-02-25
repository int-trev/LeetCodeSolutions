using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class PascalsTriangle
    {
        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> bigList = new List<IList<int>>();
            for (int i = 0; i < numRows; i++)
            {
                int pointer = 0;
                int[] row = new int[i + 1];
                row[0] = 1;
                row[row.Length - 1] = 1;
                for(int fill = 1; fill < row.Length - 1; fill++)
                {
                    row[fill] = (int)bigList[i - 1][pointer] + (int)bigList[i - 1][pointer+1];
                    pointer++;
                }
                bigList.Add(row.ToList());
            }
            return bigList;
        }

        public IList<int> GetRow(int rowIndex)
        {
            IList<int> ints = new List<int>();
            ints.Add(1);
            for (int i = 0; i < rowIndex; i++)
            {
                int pointer = 0;
                while(pointer + 1 != ints.Count)
                {
                    ints[pointer] = ints[pointer] + ints[pointer + 1];
                    pointer++;
                }
                ints.Insert(0, 1);
            }
            return ints;
        }
    }
}
