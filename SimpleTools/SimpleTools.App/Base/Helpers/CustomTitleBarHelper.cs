using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SimpleTools.App.Base.Helpers
{
    // https://docs.microsoft.com/en-us/windows/uwp/design/shell/title-bar

    /// <summary>
    /// Worker to configure as much of what goes with a custom title bar as possible.
    /// Unfortunately it can't all be done in one place, in XAML or otherwise.
    /// These techniques are usefull if you want custom content in the title bar, or even just a different title font. 
    /// If you just want to change the color of the title bar though, calling UI.ApplyColorToTitleBar() during activation
    /// and leaving out all the XAML controls would be better.
    /// Also, if you were to want a transparent custom title bar, more would have to be done to define a draggable area.
    /// </summary>
    internal class CustomTitleBarHelper
    {
        // XAML Controls that make up the title bar.
        private Grid _appTitleBar { get; set; }
        private TextBlock _appTitleTextBlock { get; set; }

        // Colors for different states of the title bar. The initial background and foreground are set in XAML as well.
        private SolidColorBrush _barBackgroundBrush { get; } = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _barBackgroundBrushInactive { get; } = new SolidColorBrush(Colors.LightGray);
        private SolidColorBrush _barTextBrush { get; } = new SolidColorBrush(Colors.White);
        private SolidColorBrush _barTextBrushInactive { get; } = new SolidColorBrush(Colors.Gray);
        private Color _buttonHoverBackgroundColor { get; } = PlatformShim.BrushConverterConvertFrom("#4d4d4d").Color;

        private CoreApplicationViewTitleBar _coreTitleBar { get; } = CoreApplication.GetCurrentView().TitleBar;

        internal CustomTitleBarHelper(Grid appTitleBar, TextBlock appTitleTextBlock)
        {
            _appTitleBar = appTitleBar;
            _appTitleTextBlock = appTitleTextBlock;

            UpdateTitleBarLayout(_coreTitleBar);

            _coreTitleBar.ExtendViewIntoTitleBar = true;
            _coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            _coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            UI.ApplyColorToTitleBarButtons(_barBackgroundBrush.Color, buttonHoverBackgroundColor: _buttonHoverBackgroundColor);
            Window.Current.Activated += Current_Activated;

            Window.Current.SetTitleBar(_appTitleBar);
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            _appTitleBar.Height = coreTitleBar.Height;
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                _appTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                _appTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
            {
                _appTitleBar.Background = _barBackgroundBrushInactive;
                _appTitleTextBlock.Foreground = _barTextBrushInactive;
            }
            else
            {
                _appTitleBar.Background = _barBackgroundBrush;
                _appTitleTextBlock.Foreground = _barTextBrush;
            }
        }
    }
}
