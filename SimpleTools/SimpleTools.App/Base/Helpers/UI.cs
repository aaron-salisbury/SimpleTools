using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SimpleTools.App.Base.Helpers
{
    public static class UI
    {
        private static SolidColorBrush _systemAccentBrush => (SolidColorBrush)Application.Current.Resources["SystemControlHighlightAccentBrush"];

        internal static ApplicationViewTitleBar TitleBar => ApplicationView.GetForCurrentView().TitleBar;

        public static void ApplyColorToTitleBar(Color? backgroundColor = null, Color? foregroundColor = null, Color? inactiveBackgroundColor = null, Color? inactiveForegroundColor = null, Color? buttonPressedForegroundColor = null, Color? buttonHoverBackgroundColor = null, Color? buttonHoverForegroundColor = null)
        {
            if (backgroundColor is null)
            {
                backgroundColor = _systemAccentBrush.Color;
            }

            ApplicationViewTitleBar titleBar = TitleBar;

            titleBar.BackgroundColor = backgroundColor;
            titleBar.ForegroundColor = foregroundColor ?? Colors.White;
            titleBar.InactiveBackgroundColor = inactiveBackgroundColor ?? Colors.LightGray;
            titleBar.InactiveForegroundColor = inactiveForegroundColor ?? Colors.Gray;

            ApplyColorToTitleBarButtons(backgroundColor, foregroundColor, inactiveBackgroundColor, inactiveForegroundColor,  buttonPressedForegroundColor, buttonHoverBackgroundColor, buttonHoverForegroundColor);
        }

        public static void ApplyColorToTitleBarButtons(Color? backgroundColor = null, Color? foregroundColor = null, Color? inactiveBackgroundColor = null, Color? inactiveForegroundColor = null, Color? buttonPressedForegroundColor = null, Color? buttonHoverBackgroundColor = null, Color? buttonHoverForegroundColor = null)
        {
            if (backgroundColor is null)
            {
                backgroundColor = _systemAccentBrush.Color;
            }

            ApplicationViewTitleBar titleBar = TitleBar;

            titleBar.ButtonBackgroundColor = backgroundColor;
            titleBar.ButtonForegroundColor = foregroundColor ?? Colors.White;
            titleBar.ButtonInactiveBackgroundColor = inactiveBackgroundColor ?? Colors.LightGray;
            titleBar.ButtonInactiveForegroundColor = inactiveForegroundColor ?? Colors.Gray;
            titleBar.ButtonPressedBackgroundColor = backgroundColor;
            titleBar.ButtonPressedForegroundColor = buttonPressedForegroundColor ?? Colors.White;
            titleBar.ButtonHoverBackgroundColor = buttonHoverBackgroundColor ?? ExtrapolateModestButtonHoverBackground(backgroundColor.Value);
            titleBar.ButtonHoverForegroundColor = buttonHoverForegroundColor ?? Colors.White;
        }

        private static Color ExtrapolateModestButtonHoverBackground(Color baseColor)
        {
            return Color.FromArgb(
                128,
                (byte)(baseColor.R + 30),
                (byte)(baseColor.G + 30),
                (byte)(baseColor.B + 30));
        }
    }
}
