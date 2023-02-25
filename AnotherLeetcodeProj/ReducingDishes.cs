using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLeetcodeProj
{
    internal class ReducingDishes
    {
        public int MaxSatisfaction(int[] satisfaction)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            int bestSoFar = 0;
            //Looping through the satisfaction array to see how many items to choose. At most N, at least 0
            for (int i = 0; i < satisfaction.Length; i++)
            {
                int max = int.MinValue;
                int index = -1;
                for (int j = 0; j < satisfaction.Length; j++)
                {
                    //Finding the next best choice for dish
                    if (satisfaction[j] > max && !result.ContainsKey(j))
                    {
                        max = satisfaction[j];
                        index = j;
                    }
                }

                //Go through the previous values and add them all up
                int sum = max;
                foreach (var keyval in result)
                {
                    sum += keyval.Value;
                    result[keyval.Key] = keyval.Value + satisfaction[keyval.Key]; //iterating itself
                }
                result[index] = max * 2;

                //check if the resultant is better
                if (bestSoFar > sum)
                {
                    break;
                }
                bestSoFar = sum;
            }

            return bestSoFar;
        }
    }
}
