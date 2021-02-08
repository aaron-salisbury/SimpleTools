using System;
using static SimpleTools.Core.Domains.Budget.Types;

namespace SimpleTools.Core.Domains.Budget
{
    [Serializable]
    public class Income
    {
        public SchedulePeriods SchedulePeriod { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
    }
}
