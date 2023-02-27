using GalaSoft.MvvmLight;
using SimpleTools.App.Views;
using System;
using System.Collections.Generic;

namespace SimpleTools.App.ViewModels
{
    public class WebToolsViewModel : ViewModelBase
    {
        public readonly List<(string Content, Type Page)> ToolPages = new List<(string Content, Type Page)>
        {
            ("YT Timestamp Linker", typeof(YouTubeTimestampLinkerPage)),
            ("Dummy Translator", typeof(DummyTranslatorPage))
        };
    }
}
