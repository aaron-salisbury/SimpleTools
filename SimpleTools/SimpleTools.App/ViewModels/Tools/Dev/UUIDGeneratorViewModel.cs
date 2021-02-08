using GalaSoft.MvvmLight.Command;
using SimpleTools.App.Base.Helpers;
using SimpleTools.Core.Tools.Dev;

namespace SimpleTools.App.ViewModels
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public RelayCommand CopyUUIDCommand { get; }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator(AppLogger);
            CopyUUIDCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(UUIDGenerator.UUID));

            bool uuidGenerateFunction() => UUIDGenerator.Initiate();
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(uuidGenerateFunction, ExecuteTaskCommand), () => !IsBusy);
        }
    }
}
