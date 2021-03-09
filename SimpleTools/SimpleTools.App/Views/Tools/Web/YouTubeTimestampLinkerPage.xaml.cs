using SimpleTools.App.ViewModels;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class YouTubeTimestampLinkerPage : Page
    {
        public YouTubeTimestampLinkerPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.YouTubeTimestampLinkerViewModel;
        }

        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = false;
            }
            else
            {
                bool shouldCancelForNonNums = args.NewText.Any(c => !char.IsDigit(c));

                args.Cancel = shouldCancelForNonNums;
            }
        }
    }
}
