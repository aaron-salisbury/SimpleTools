using System;
using static SimpleTools.Core.Domains.Budget.Types;

namespace SimpleTools.Core.Domains.Budget
{
    [Serializable]
    public class Expense
    {
        public SchedulePeriods SchedulePeriod { get; set; }
        public string Account { get; set; }
        public byte DueDateDay { get; set; }
        public decimal Amount { get; set; }
    }
}
