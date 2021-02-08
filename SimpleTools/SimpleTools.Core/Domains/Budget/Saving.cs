using System;
using static SimpleTools.Core.Domains.Budget.Types;

namespace SimpleTools.Core.Domains.Budget
{
    [Serializable]
    public class Saving
    {
        public SchedulePeriods SchedulePeriod { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
    }
}
