using SimpleTools.App.ViewModels;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class MinecraftNetherCalculatorPage : Page
    {
        public MinecraftNetherCalculatorPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.MinecraftNetherCalculatorViewModel;
        }

        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            // https://blog.mzikmund.com/2019/01/number-only-textbox-in-uwp/

            if (string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = false;
            }
            else
            {
                bool shouldCancelForNonNumsOrMinus = args.NewText.Any(c => !char.IsDigit(c) && c != '-');
                bool shouldCancelForMinusInMiddle = !string.IsNullOrEmpty(sender.Text) && args.NewText.Last().Equals('-');
                bool shouldCancelForNegativeY = sender.Name.EndsWith("Y") && args.NewText.Contains('-');

                args.Cancel = shouldCancelForNonNumsOrMinus || shouldCancelForMinusInMiddle || shouldCancelForNegativeY;
            }
        }
    }
}
