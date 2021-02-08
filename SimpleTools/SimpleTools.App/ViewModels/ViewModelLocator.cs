using GalaSoft.MvvmLight.Ioc;
using SimpleTools.App.Base.Services;
using SimpleTools.App.Views;

namespace SimpleTools.App.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;
        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<SettingsViewModel, SettingsPage>();
            Register<IntroductionViewModel, IntroductionPage>();
            Register<LogViewModel, LogPage>();

            // *** Dev Tools ***
            Register<DevToolsViewModel, DevToolsPage>();
            Register<UUIDGeneratorViewModel, UUIDGeneratorPage>();
            Register<FlatUIColorPickerViewModel, FlatUIColorPickerPage>();
            Register<LineSorterViewModel, LineSorterPage>();

            // *** Game Tools ***
            Register<GameToolsViewModel, GameToolsPage>();
            Register<DiceRollerViewModel, DiceRollerPage>();
            Register<MinecraftNetherCalculatorViewModel, MinecraftNetherCalculatorPage>();
            Register<MinecraftSeedMapViewModel, MinecraftSeedMapPage>();

            // *** Finance Tools ***
            Register<FinanceToolsViewModel, FinanceToolsPage>();

            // *** Other Tools ***
            Register<ToDoListViewModel, ToDoListPage>();
        }

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();
        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
        public IntroductionViewModel IntroductionViewModel => SimpleIoc.Default.GetInstance<IntroductionViewModel>();
        public LogViewModel LogViewModel => SimpleIoc.Default.GetInstance<LogViewModel>();

        // *** Dev Tools ***
        public DevToolsViewModel DevToolsViewModel => SimpleIoc.Default.GetInstance<DevToolsViewModel>();
        public UUIDGeneratorViewModel UUIDGeneratorViewModel => SimpleIoc.Default.GetInstance<UUIDGeneratorViewModel>();
        public FlatUIColorPickerViewModel FlatUIColorPickerViewModel => SimpleIoc.Default.GetInstance<FlatUIColorPickerViewModel>();
        public LineSorterViewModel LineSorterViewModel => SimpleIoc.Default.GetInstance<LineSorterViewModel>();

        // *** Game Tools ***
        public GameToolsViewModel GameToolsViewModel => SimpleIoc.Default.GetInstance<GameToolsViewModel>();
        public DiceRollerViewModel DiceRollerViewModel => SimpleIoc.Default.GetInstance<DiceRollerViewModel>();
        public MinecraftNetherCalculatorViewModel MinecraftNetherCalculatorViewModel => SimpleIoc.Default.GetInstance<MinecraftNetherCalculatorViewModel>();
        public MinecraftSeedMapViewModel MinecraftSeedMapViewModel => SimpleIoc.Default.GetInstance<MinecraftSeedMapViewModel>();

        // *** Finance Tools ***
        public FinanceToolsViewModel FinanceToolsViewModel => SimpleIoc.Default.GetInstance<FinanceToolsViewModel>();

        // *** Other Tools ***
        public ToDoListViewModel ToDoListViewModel => SimpleIoc.Default.GetInstance<ToDoListViewModel>();

        public void Register<VM, V>() where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
