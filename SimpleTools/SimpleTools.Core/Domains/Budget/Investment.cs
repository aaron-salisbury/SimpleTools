using System;
using static SimpleTools.Core.Domains.Budget.Types;

namespace SimpleTools.Core.Domains.Budget
{
    [Serializable]
    public class Investment
    {
        public SchedulePeriods SchedulePeriod { get; set; }
        public string InvestmentType { get; set; }
        public string Account { get; set; }
        public double SalaryContributionRate { get; set; }
    }
}
