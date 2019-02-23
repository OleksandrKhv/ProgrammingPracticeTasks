using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Other.NotRanked
{
    public class FindAllNumberSums
    {
        public static void App()
        {

        }

        //public int Calc(int targetSum, params int[] nums)
        //{
        //    if (nums == null || nums.Length == 0) throw new ArgumentException("");

        //    int iterationsNum = 0;

        //    List<List<int>> solutions = new List<List<int>>();

        //    nums = Sort(nums);

        //    if (targetSum < nums[0]) return 0;

        //    int[] currentNums = new int[targetSum / nums[0]];
        //    int[] currentPositions = new int[currentNums.Length];
        //    int currentSum = nums[0], currentPos = 0;
        //    bool movingForward = true;

        //    currentNums[0] = 0;
        //    currentPositions[0] = 0;


        //    while (currentPos >= 0)
        //    {
        //        if (currentSum >= targetSum)
        //        {
        //            if (currentSum == targetSum)
        //            {
        //                solutions.Add(new List<int>(GetSubArray(currentNums, currentPos+1)));
        //            }
        //            currentPos--;
        //            movingForward = false;
        //        }
        //        else if(movingForward)
        //        {

        //        }


        //        while (true)
        //        {

        //        }
        //    }
        //}

        public static int[] Sort(int[] values)
        {
            List<int> list = new List<int>(values);
            list.Sort();
            return list.ToArray();
        }

        public static List<int> GetSubArray(int[] arr, int end)
        {
            int[] arrNew = new int[end];
            for(int i = 0; i < arrNew.Length; i++)
            {
                arrNew[i] = arr[i];
            }
            return new List<int>(arrNew);
        }
    }
}
