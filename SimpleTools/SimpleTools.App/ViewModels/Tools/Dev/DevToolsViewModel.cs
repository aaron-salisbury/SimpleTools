using GalaSoft.MvvmLight;
using SimpleTools.App.Views;
using System;
using System.Collections.Generic;

namespace SimpleTools.App.ViewModels
{
    public class DevToolsViewModel : ViewModelBase
    {
        public readonly List<(string Content, Type Page)> ToolPages = new List<(string Content, Type Page)>
        {
            ("UUID Generator", typeof(UUIDGeneratorPage)),
            ("Flat UI Color Picker", typeof(FlatUIColorPickerPage)),
            ("Line Sorter", typeof(LineSorterPage))
        };
    }
}
