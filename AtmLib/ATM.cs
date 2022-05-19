using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Class.ATM
{
    public class ATM
    {
        public IEnumerable<Cassette> Cassettes { get; set; }

        private Dictionary<int, int> _аtmOutput { get; set; }

        public long TimeForCalculateLastIssueMoney { get; set; }

        public ATM(IEnumerable<Cassette> сassettes)
        {
            Cassettes = сassettes.OrderByDescending(c => c.NominalValue);
            _аtmOutput = new Dictionary<int, int>();
        }

        public Dictionary<int, int> GetMoney(int needSum)
        {
            var watch = Stopwatch.StartNew();
            
            _аtmOutput.Clear();
            if (isCanIssueMoney(needSum))
            {
                watch.Stop();
                TimeForCalculateLastIssueMoney = watch.ElapsedMilliseconds;
                return _аtmOutput;
            }
            else
            {
                watch.Stop();
                TimeForCalculateLastIssueMoney = watch.ElapsedMilliseconds;
                return null;
            }
        }

        private bool isCanIssueMoney(int needSum)
        {
            int remain = needSum;
            Console.WriteLine("Надо выдать: " + remain);
            var summInAllCassettes = Cassettes.Sum(c => c.NominalValue * c.BanknoteCount);
            if (summInAllCassettes < needSum)
                return false;
            foreach (var cassette in Cassettes)
            {
                if (cassette.IsWork)
                {
                    if (cassette.NominalValue <= remain && cassette.BanknoteCount > 0)
                    {
                        remain -= cassette.NominalValue;
                        cassette.BanknoteCount--;
                        if (!_аtmOutput.TryAdd(cassette.NominalValue, 1))
                        {
                            _аtmOutput[cassette.NominalValue] += 1;
                        }
                        Console.WriteLine("Выдана банкнота номиналом: {0}", cassette.NominalValue);

                        if (!isCanIssueMoney(remain))
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
