using AtmLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Class.ATM
{
    public class ATM
    {
        public IEnumerable<Cassette> Cassettes { get; set; }

        public List<MoneyOperation> MoneyOperations { get; set; }

        public ATM(IEnumerable<Cassette> сassettes)
        {
            Cassettes = сassettes.OrderByDescending(c => c.NominalValue);
            MoneyOperations = new();
        }

        public MoneyOperation GetMoney(int needSum)
        {
            var watch = Stopwatch.StartNew();

            MoneyOperation operation = new();
            if (needSum > 0 && isCanIssueMoney(needSum, operation))
            {

                operation.IsSuccessOperation = true;
                foreach (var cassett in Cassettes)
                {
                    if(cassett.IsWork)
                    {
                        if (operation.AtmOutput.ContainsKey(cassett.NominalValue))
                            cassett.BanknoteCount -= operation.AtmOutput[cassett.NominalValue];
                    }
                }
                watch.Stop();
                operation.TimeForCalculateLastIssueMoney = watch.ElapsedMilliseconds;
                MoneyOperations.Add(operation);
                return operation;
            }
            else
            {
                watch.Stop();
                operation.TimeForCalculateLastIssueMoney = watch.ElapsedMilliseconds;
                operation.IsSuccessOperation = false;
                MoneyOperations.Add(operation);
                return operation;
            }
        }

        private bool isCanIssueMoney(int needSum, MoneyOperation operation)
        {
            int remain = needSum;
            var summInAllCassettes = Cassettes.Select(x => x).Where(c => c.IsWork).Sum(c => c.NominalValue * c.BanknoteCount);
            if (summInAllCassettes < needSum)
                return false;
            foreach (var cassette in Cassettes)
            {
                if (cassette.IsWork)
                {
                    if (cassette.NominalValue <= remain && cassette.BanknoteCount > 0)
                    {
                        remain -= cassette.NominalValue;
                        if (!operation.AtmOutput.TryAdd(cassette.NominalValue, 1))
                        {
                            operation.AtmOutput[cassette.NominalValue] += 1;
                        }

                        if (!isCanIssueMoney(remain, operation))
                            return false;
                        else
                            return true;
                    }
                }
            }
            if (remain > 0)
                return false;
            return true;
        }
    }
}
