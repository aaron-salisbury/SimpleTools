using System;

namespace SimpleTools.Core.Domains.Budget
{
    [Serializable]
    public class Debt
    {
        public string Account { get; set; }
        public double Interest { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal Payoff { get; set; }
        public decimal Payment { get; set; }
        public decimal ExtraPayment { get; set; }
        public decimal NewPayment { get; set; }
        public int RemainingPayments { get; set; }
    }
}
