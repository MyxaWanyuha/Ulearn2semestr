using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var opt = CreateOptimizationTable(first, second);
            return RestoreAnswer(opt, first, second);
        }
        /*
         * создать таблицу
         * если один из индексов равен нулю ставим туда ноль, если i элемент == j, то ставим 1
         * если i != j ставим максимум совпадающих символов
         */
        private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
        {
            throw new NotImplementedException();
        }

        private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
        {
            throw new NotImplementedException();
        }
    }
}