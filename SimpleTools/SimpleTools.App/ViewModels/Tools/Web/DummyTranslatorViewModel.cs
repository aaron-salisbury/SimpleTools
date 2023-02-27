using GalaSoft.MvvmLight.Command;
using SimpleTools.App.Base.Helpers;
using SimpleTools.Core.Tools.Web;

namespace SimpleTools.App.ViewModels
{
    public class DummyTranslatorViewModel : BaseViewModel
    {
        public DummyTranslator DummyTranslator { get; set; }

        public RelayCommand TranslateCommand { get; }
        public RelayCommand CopyCommand { get; }

        public DummyTranslatorViewModel()
        {
            DummyTranslator = new DummyTranslator();

            TranslateCommand = new RelayCommand(() => DummyTranslator.Translate());
            CopyCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(DummyTranslator.TranslatedPhrase));
        }
    }
}
