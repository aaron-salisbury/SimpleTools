using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Domains.Budget;
using System.Collections.ObjectModel;

namespace SimpleTools.Core.Tools.Finance
{
    public class BudgetManager : ValidatableModel
    {
        private ILogger _logger { get; set; }

        public ObservableCollection<Income> Incomes { get; set; } = new ObservableCollection<Income>();
        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();
        public ObservableCollection<Investment> Investments { get; set; } = new ObservableCollection<Investment>();
        public ObservableCollection<Saving> Savings { get; set; } = new ObservableCollection<Saving>();
        public ObservableCollection<Debt> Debts { get; set; } = new ObservableCollection<Debt>();

        public BudgetManager(AppLogger appLogger)
        {
            _logger = appLogger.Logger;

            LoadTestData();
        }

        private void LoadTestData()
        {
            Incomes.Add(new Income
            {
                SchedulePeriod = Types.SchedulePeriods.Yearly,
                Source = "Salary",
                Amount = 80000.00M
            });

            Investments.Add(new Investment
            {
                SchedulePeriod = Types.SchedulePeriods.Monthly,
                InvestmentType = "401(k)",
                Account = "Associated Bank",
                SalaryContributionRate = 6.0D
            });

            Savings.Add(new Saving
            {
                SchedulePeriod = Types.SchedulePeriods.Monthly,
                Account = "General Savings",
                Amount = 200.00M
            });

            Debts = new ObservableCollection<Debt>
            { 
                new Debt
                {
                    Account = "Student Loans",
                    Interest = 4.7D,
                    OriginalAmount = 30000.00M,
                    Payoff = 23000.00M,
                    Payment = 325.00M
                },
                new Debt
                {
                    Account = "Mortgage",
                    Interest = 3.2D,
                    OriginalAmount = 120000.00M,
                    Payoff = 76000.00M,
                    Payment = 800.00M
                }
            };

            Expenses = new ObservableCollection<Expense>
            {
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Internet",
                    DueDateDay = 1,
                    Amount = 40.00M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Water",
                    DueDateDay = 15,
                    Amount = 50.00M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Netflix",
                    DueDateDay = 12,
                    Amount = 11.59M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Gas",
                    DueDateDay = 30,
                    Amount = 200.00M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "General Spending",
                    DueDateDay = 30,
                    Amount = 250.00M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Mortgage",
                    DueDateDay = 1,
                    Amount = 800.00M
                },
                new Expense
                {
                    SchedulePeriod = Types.SchedulePeriods.Monthly,
                    Account = "Student Loans",
                    DueDateDay = 3,
                    Amount = 325.00M
                }
            };
        }
    }
}
