using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class MinecraftSeedMapPage : Page
    {
        public MinecraftSeedMapPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.MinecraftSeedMapViewModel;
        }

        private void WebViewControl_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //TODO: Block ads somehow.

            // https://stackoverflow.com/questions/50271689/uwp-webview-adblocker
            //string functionString =
            //    "let link = document.createElement('link'); " +
            //    "link.rel = 'stylesheet'; " +
            //    "link.type = 'text/css'; " +
            //    "link.href = 'ms-appx-web:///MyCss.css'; document.getElementsByTagName('head')[0].appendChild(link);";

            //WebViewControl.InvokeScriptAsync("eval", new string[] { functionString }).GetResults();
        }
    }
}
