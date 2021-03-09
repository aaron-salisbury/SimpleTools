using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Base.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Tools.Web
{
    public class YTTimestampLinker : ValidatableModel
    {
        private ILogger _logger { get; set; }

        private string _youTubeURL;
        [Required]
        public string YouTubeURL
        {
            get => _youTubeURL;
            set { _youTubeURL = value; RaisePropertyChanged(); }
        }

        private string _minutes;
        public string Minutes
        {
            get => _minutes;
            set { _minutes = value; RaisePropertyChanged(); }
        }

        private string _seconds;
        public string Seconds
        {
            get => _seconds;
            set { _seconds = value; RaisePropertyChanged(); }
        }

        private string _newURL;
        public string NewURL
        {
            get => _newURL;
            set { _newURL = value; RaisePropertyChanged(); }
        }

        public YTTimestampLinker(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
        }

        public bool GetLink()
        {
            string newURL = Preview();

            if (string.IsNullOrEmpty(newURL))
            {
                NewURL = null;
                return false;
            }

            NewURL = newURL;
            return true;
        }

        public string Preview()
        {
            string previewURL = null;

            try
            {
                if (!string.IsNullOrEmpty(YouTubeURL))
                {
                    _logger.Information("Beginning to stamp YouTube URL with time.");

                    string urlToConvert = YouTubeURL;
                    if (urlToConvert.Contains("www.youtube.com/watch?v="))
                    {
                        urlToConvert = urlToConvert.Replace("www.youtube.com/watch?v=", "youtu.be/");
                    }

                    Uri youTubeUri = urlToConvert.AsURL();

                    if (youTubeUri != null)
                    {
                        string minutesString = string.IsNullOrEmpty(Minutes) ? "0" : Minutes;
                        bool minutesAreGood = int.TryParse(minutesString, out int minutes);

                        string secondsString = string.IsNullOrEmpty(Seconds) ? "0" : Seconds;
                        bool secondsAreGood = int.TryParse(secondsString, out int seconds);

                        if (minutesAreGood && secondsAreGood)
                        {
                            seconds += (minutes * 60);

                            previewURL = $"{youTubeUri.AbsoluteUri}&t={Math.Abs(seconds)}s";
                        }
                        else
                        {
                            _logger.Error("Failed to stamp YouTube URL with time: Both minutes and seconds must be valid integers.");
                        }
                    }
                    else
                    {
                        _logger.Error("Failed to stamp YouTube URL with time: YouTube URL is not valid.");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.Error($"Failed to stamp YouTube URL with time: {e.Message}");
            }

            return previewURL;
        }
    }
}
