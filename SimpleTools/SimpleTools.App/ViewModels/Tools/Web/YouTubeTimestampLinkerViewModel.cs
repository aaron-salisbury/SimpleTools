using GalaSoft.MvvmLight.Command;
using SimpleTools.App.Base.Helpers;
using SimpleTools.Core.Tools.Web;
using System;

namespace SimpleTools.App.ViewModels
{
    public class YouTubeTimestampLinkerViewModel : BaseViewModel
    {
        public YTTimestampLinker YTTimestampLinker { get; set; }

        public RelayCommand GetLinkCommand { get; }
        public RelayCommand PreviewCommand { get; }
        public RelayCommand CopyCommand { get; }

        public YouTubeTimestampLinkerViewModel()
        {
            YTTimestampLinker = new YTTimestampLinker(AppLogger);

            GetLinkCommand = new RelayCommand(() => YTTimestampLinker.GetLink());
            PreviewCommand = new RelayCommand(async () => await PreviewAsync());
            CopyCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(YTTimestampLinker.NewURL));
        }

        private async System.Threading.Tasks.Task<bool> PreviewAsync()
        {
            string previewURL = YTTimestampLinker.Preview();

            if (!string.IsNullOrEmpty(previewURL))
            {
                Uri previewUri = new Uri(previewURL);

                return await Windows.System.Launcher.LaunchUriAsync(previewUri);
            }

            return false;
        }
    }
}
