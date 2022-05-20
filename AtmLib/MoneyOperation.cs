using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmLib
{
    public class MoneyOperation
    {
        public Dictionary<int, int> AtmOutput { get; set; }

        public long TimeForCalculateLastIssueMoney { get; set; }

        public bool IsSuccessOperation { get; set; }

        public MoneyOperation()
        {
            AtmOutput = new();
        }
    }
}
